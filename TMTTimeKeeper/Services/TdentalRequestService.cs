using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;
using TMTTimeKeeper.Interface;
using TMTTimeKeeper.Models;
using TMTTimeKeeper.Models.ApiRequestModels;

namespace TMTTimeKeeper.Services
{
    public class TdentalRequestService : BaseService, ITendalRequestService
    {
        private readonly string _hostWebApiUrl = "";
        private readonly string _token;
        private HttpClient client;
        private IConfiguration _config; 
        private IXmlService _xmlService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public TdentalRequestService(IServiceProvider provider,IConfiguration config, IXmlService xmlService, IWebHostEnvironment webHostEnvironment)
        : base(provider)
        {
            _config = config;
            _xmlService = xmlService;
            _hostingEnvironment = webHostEnvironment;

        }


        private async Task SetRequest()
        {
            var path = Path.Combine(_hostingEnvironment.ContentRootPath, @"ThirdParty\Tdental.xml");
            var data = _xmlService.GetObject<TdentalRequestInfo>(path);

            client = new HttpClient
            {
                BaseAddress = new Uri(data.Domain),
            };
            //check token hết hạn chưa? rồi thì lấy token mới => lưu vào xml. (tìm cách tìm cái node token và lưu token)
            if (isInvalidToken(data.Token))
            {
                var val = new RefreshViewModel()
                {
                    AccessToken = data.Token,
                    RefreshToken = data.RefreshToken
                };
                var result = await PostRequest<RefreshResponseViewModel>(data.Domain + "/api/Account/Refresh", val, true);
                if (result != null)
                {
                    data.Token = result.AccessToken;
                    data.RefreshToken = result.RefreshToken;
                    _xmlService.ChangeTextInNode(path, "Token", result.AccessToken);
                    _xmlService.ChangeTextInNode(path, "RefreshToken", result.AccessToken);
                }
            }

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {data.Token}");
        }

        private bool isInvalidToken(string token)
        {
            var jwtToken = new JwtSecurityToken(token);
            return (jwtToken == null) || (jwtToken.ValidFrom > DateTime.UtcNow) || (jwtToken.ValidTo < DateTime.UtcNow);
        }
        //public TdentalRequestService(string hostWebApiUrl, string token)
        //{
        //    _hostWebApiUrl = hostWebApiUrl;
        //    _apiAuthKey = token;
        //    client = new HttpClient
        //    {
        //        BaseAddress = new Uri(hostWebApiUrl)
        //    };
        //}
        public HttpClient GetClient()
        {
            string _baseAddress = _hostWebApiUrl;
            var client = new HttpClient
            {
                BaseAddress = new Uri(_baseAddress)
            };

            client.DefaultRequestHeaders.Add("AuthApiKey", _token);
            return client;
        }

        public async Task<TResult> GetAsync<TResult>(string url)
        {
            await SetRequest();

            client.Timeout = TimeSpan.FromSeconds(30);
            var result = default(TResult);
            try
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                //var jsonString = await response.Content.ReadAsStringAsync();
                //var result = JsonConvert.DeserializeObject<TResult>(jsonString);


                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;

                    result = JsonConvert.DeserializeObject<TResult>(x.Result);
                });



            }

            catch (Exception ex)
            {

            }
            return result;
        }

        public async Task<TResult> GetAsync<TResult>(string url, object obj = null)
        {
            await SetRequest();

            if (obj != null)
                url += "?" +obj.ToQueryString();
            client.Timeout = TimeSpan.FromSeconds(30);
            var result = default(TResult);
            try
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                //var jsonString = await response.Content.ReadAsStringAsync();
                //var result = JsonConvert.DeserializeObject<TResult>(jsonString);


                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;

                    result = JsonConvert.DeserializeObject<TResult>(x.Result);
                });

            }

            catch (Exception ex)
            {

            }
            return result;
        }

        public async Task<string> GetStringAsync(string url)
        {
            await SetRequest();

            var httpRequest = new HttpRequestMessage(new HttpMethod("GET"), url);
            var response = client.SendAsync(httpRequest).Result;
            var jsonString = await response.Content.ReadAsStringAsync();
            return jsonString;
        }

        public async Task<TResult> PostRequest<TResult>(string apiUrl, object postObject, bool isAnonymous = false)
        {
            if(!isAnonymous)
                await SetRequest();

            TResult result = default(TResult);

                var response = await client.PostAsync(apiUrl, postObject, new JsonMediaTypeFormatter()).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;

                    result = JsonConvert.DeserializeObject<TResult>(x.Result);

                });

            return result;
        }

        public async Task<TResult> Login<TResult>(string apiUrl, object postObject)
        {
            TResult result = default(TResult);

            var response = await client.PostAsync(apiUrl, postObject, new JsonMediaTypeFormatter()).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
            {
                if (x.IsFaulted)
                    throw x.Exception;

                result = JsonConvert.DeserializeObject<TResult>(x.Result);

            });

            return result;
        }

        public async Task PutRequest<T>(string apiUrl, object putObject)
        {
            SetRequest();

            var response = await client.PutAsync(apiUrl, putObject, new JsonMediaTypeFormatter()).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
        }

        public async Task DeleteRequest(string apiUrl)
        {
            SetRequest();

            var response = await client.DeleteAsync(apiUrl).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                response.Content?.Dispose();
                throw new HttpRequestException($"{response.StatusCode}:{content}");
            }
        }
        private async Task<string> ParamsToStringAsync(Dictionary<string, string> urlParams)
        {
            using (HttpContent content = new FormUrlEncodedContent(urlParams))
                return await content.ReadAsStringAsync();
        }

        public Task PutRequest<T>(string apiUrl, T putObject)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TimeAttendanceSyncLogDisplay>> GetAttendanceSyncLog(GetAttendanceSyncReq val)
        {
            return await PostRequest<IEnumerable<TimeAttendanceSyncLogDisplay>>("api/TimeAttendanceMachines/GetAttendanceSyncLog", val);
        }
    }

    public static class ObjectExtensions
    {
        public static string ToQueryString(this object model)
        {
            var serialized = JsonConvert.SerializeObject(model);
            var deserialized = JsonConvert.DeserializeObject<Dictionary<string, string>>(serialized);
            var result = deserialized.Where(x=> x.Value != null).Select((kvp) => kvp.Key.ToString() + "=" + Uri.EscapeDataString(kvp.Value)).Aggregate((p1, p2) => p1 + "&" + p2);
            return result;
        }
    }


    [XmlRoot(ElementName = "Data")]
    public class TdentalRequestInfo
    {
        [XmlElement]
        public string Token { get; set; }
        [XmlElement]
        public string RefreshToken { get; set; }
        [XmlElement]
        public string Domain { get; set; }
        [XmlElement]
        public string UserName { get; set; }
        [XmlElement]
        public string PassWord { get; set; }
    }
}
