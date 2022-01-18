using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TMTTimeKeeper.Interface;
using TMTTimeKeeper.Models;

namespace TMTTimeKeeper.Services
{
    public class AccountService : BaseService, IAccountService
    {
        private readonly ITendalRequestService _tdentalRequestService;
        private readonly IXmlService _xmlService;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public AccountService(ITendalRequestService tdentalRequestService, IXmlService xmlService, IWebHostEnvironment webHostEnvironment, IServiceProvider provider) : base(provider)
        {
            _tdentalRequestService = tdentalRequestService;
            _xmlService = xmlService;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<LoggedInViewModel> Login(LoginViewModel val)
        {
            var result = await _tdentalRequestService.PostRequest<LoggedInViewModel>(val.DomainName + "/api/Account/Login", val, true);
            if (result == null)
            {
                throw new Exception("Tài khoản không tồn tại");
            }
            else 
            {
                if (result.Succeeded)
                {
                    var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, @"ThirdParty\Tdental.xml");
                    var data = _xmlService.GetObject<TdentalRequestInfo>(filePath);
                    data.Domain = val.DomainName;
                    data.UserName = result.User.UserName;
                    data.PassWord = val.Password;
                    data.Token = result.Token;
                    data.RefreshToken = result.RefreshToken;
                    _xmlService.WriteXMLFile(filePath, data);
                }
            }
            return result;
        }
    }
}
