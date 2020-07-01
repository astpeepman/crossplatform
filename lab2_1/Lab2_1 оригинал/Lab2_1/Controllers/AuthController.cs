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
        [HttpGet("token")]
        public string GetToken()
        {
            return AuthOptions.GenerateToken();
        }
        [HttpGet("token/secret")]
        public string GetAdminToken()
        {
            return AuthOptions.GenerateToken(true);
        }
    }
}