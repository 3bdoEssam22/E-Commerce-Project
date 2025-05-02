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

        // Check Email.
        // Take string Email and return bool Boolean.
        Task<bool> CheckEmailAsync(string email);

        // Get Current User Address
        // Take string Email then return AddressDto.
        Task<AddressDto> GetCurrentUserAddressAsync(string email);

        // Update Current User Address
        // Take AddressDto Updated Address and string email then Return AddressDto Address After Update.
        public Task<AddressDto> UpdateCurrentUserAddressAsync(AddressDto addressDto, string email);

        // Get Current User
        // Take string Email Then Return UserDto Token, Email and DisplayName.
        Task<UserDto> GetCurrentUserAsync(string email);
    }
}
