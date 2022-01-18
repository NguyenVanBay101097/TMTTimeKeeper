using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TMTTimeKeeper.Models
{
    public class RefreshResponseViewModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }

    public class RefreshViewModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
