using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Lab2_1.Models;

namespace Lab2_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        public struct LoginData
        {
            public string login { get; set; }
            public string password { get; set; }

            public LoginData(string login, string password)
            {
                this.login = login;
                this.password = password;
            }
        }

        public List<LoginData> allUsers = new List<LoginData> { new LoginData("admin", "admin"), new LoginData("user", "user") };

        [HttpPost("token")]
        public string GetToken([FromBody] LoginData logPas)
        {
            logPas.password = new string(logPas.password.ToCharArray().ToArray());
            var currentUser = allUsers.FirstOrDefault(u => u.login == logPas.login && u.password == logPas.password);

            if (currentUser.login == null)
            {
                return null;
            }

            if (currentUser.login == "admin")
                return AuthOptions.GenerateToken(true);
            else
                return AuthOptions.GenerateToken(false);
        }
    }
}