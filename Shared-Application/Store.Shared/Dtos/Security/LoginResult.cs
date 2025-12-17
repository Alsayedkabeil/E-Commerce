using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Shared.Dtos.Security
{
    public class LoginResult
    {
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Token { get; set; }
    }
}
