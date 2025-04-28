using Shared.DataTransferObjects.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstracion
{
    public interface IAuthenticationService
    {
        // Login
        // Take Email and Password then return token, Email and DisplayName.
        Task<UserDto> LoginAsync(LoginDto loginDto);

        // Register
        // Take Email, Password, Username, DisplayName, and phone number then return token, Email and DisplayName.
        Task<UserDto> RegisterAsync(RegisterDto registerDto);
    }
}
