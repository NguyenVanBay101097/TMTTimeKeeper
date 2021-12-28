using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TMTTimeKeeper.Interface;

namespace TMTTimeKeeper.Services
{
    public class TdentalRequestService : ITendalRequestService
    {
        private readonly string _hostWebApiUrl = "";
        private readonly string _token;
        private HttpClient client;
        private IConfiguration _config; 
        public TdentalRequestService(IConfiguration config)
        {
            _config = config;
            this._hostWebApiUrl = "https://localhost:44377/";

            client = new HttpClient
            {
                BaseAddress = new Uri(_hostWebApiUrl),
            };
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIzYTcyOGZjYi04NGY4LTRjYzItODlmNS1mNTU0NTMyNjdiNjAiLCJ1bmlxdWVfbmFtZSI6ImFkbWluIiwiY29tcGFueV9pZCI6IjZjZTA0ZThjLTVmNjctNDVjYy1mZjU4LTA4ZDkyY2MwMjlhZCIsInVzZXJfcm9vdCI6IlRydWUiLCJuYmYiOjE2NDA2ODAyMTAsImV4cCI6MTY0MTI4NTAxMCwiaWF0IjoxNjQwNjgwMjEwfQ.usRojkInvfjqIyUJ9iLHeh2wk14uS8IwroI4BECbVvs");

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
            var httpRequest = new HttpRequestMessage(new HttpMethod("GET"), url);
            var response = client.SendAsync(httpRequest).Result;
            var jsonString = await response.Content.ReadAsStringAsync();
            return jsonString;
        }

        public async Task<TResult> PostRequest<TResult>(string apiUrl, object postObject)
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
                var response = await client.PutAsync(apiUrl, putObject, new JsonMediaTypeFormatter()).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
        }

        public async Task DeleteRequest(string apiUrl)
        {
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
}
