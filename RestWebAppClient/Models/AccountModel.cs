using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestWebAppClient.Models
{
    public class LoginModel
    {
        public string Username { get; set; }

        [PasswordPropertyText(true)]
        public string Password { get; set; }
    }
}
