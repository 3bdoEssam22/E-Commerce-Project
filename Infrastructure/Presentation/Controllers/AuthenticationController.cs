using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstracion;
using Shared.DataTransferObjects.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        // Check Email
        [HttpGet("CheckEmail")] // Get BaseUrl/Api/Authentication/CheckEmail
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            return Ok(await _serviceManager.AuthenticationService.CheckEmailAsync(email));
        }

        // Get Current User
        [Authorize]
        [HttpGet("CurrentUser")] // Get BaseUrl/Api/Authentication/CurrentUser
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var AppUser = await _serviceManager.AuthenticationService.GetCurrentUserAsync(email!);
            return Ok(AppUser);
        }

        // Get Current User Address
        [Authorize]
        [HttpGet("Address")] // Get BaseUrl/Api/Authentication/Address
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var Address = await _serviceManager.AuthenticationService.GetCurrentUserAddressAsync(email!);
            return Ok(Address);
        }

        // Update Current User Address
        [Authorize]
        [HttpPut("Address")] // Put BaseUrl/Api/Authentication/Address
        public async Task<ActionResult<AddressDto>> UpdateCurrentUserAddress(AddressDto addressDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var UpdatedAddress = await _serviceManager.AuthenticationService.UpdateCurrentUserAddressAsync(addressDto, email!);
            return Ok(UpdatedAddress);
        }

    }
}
