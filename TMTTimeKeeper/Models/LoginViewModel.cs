using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TMTTimeKeeper.Models
{
    public class LoginViewModel
    {
        [Required]
        public string DomainName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; }

    }

    public class LoggedInViewModel
    {
        public bool Succeeded { get; set; }

        public string Message { get; set; }

        public string Token { get; set; }

        public string RefreshToken { get; set; }

        public object Configs { get; set; }

        public UserViewModel User { get; set; }
    }
}
