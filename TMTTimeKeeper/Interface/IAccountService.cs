using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMTTimeKeeper.Models;

namespace TMTTimeKeeper.Interface
{
    public interface IAccountService
    {
        Task<LoggedInViewModel> Login(LoginViewModel val);

    }
}
