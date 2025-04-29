using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstracion;
using Shared.DataTransferObjects.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class AuthenticationController(IServiceManager _serviceManager) : ApiControllerBase
    {
        // Login
        [HttpPost("login")] // Post BaseUrl/Api/Authentication/login
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var User = await _serviceManager.AuthenticationService
                .LoginAsync(loginDto);
            return Ok(User);
        }

        // Register
        [HttpPost("register")] // Post BaseUrl/Api/Authentication/register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var User = await _serviceManager.AuthenticationService
                .RegisterAsync(registerDto);
            return Ok(User);
        }  
    }
}
