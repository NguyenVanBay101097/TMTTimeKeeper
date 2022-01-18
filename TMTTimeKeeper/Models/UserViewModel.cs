using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TMTTimeKeeper.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Avatar { get; set; }

        public Guid CompanyId { get; set; }
    }
}
