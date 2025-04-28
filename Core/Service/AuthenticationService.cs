using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using ServicesAbstracion;
using Shared.DataTransferObjects.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager) : IAuthenticationService
    {
        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            // Check if the Email exists
            var User = await _userManager.FindByEmailAsync(loginDto.Email)
                ?? throw new UserNotFoundException(loginDto.Email);

            // Check Password
            var IsPasswordValid = await _userManager.CheckPasswordAsync(User, loginDto.Password);
            if (IsPasswordValid)
            {
                return new UserDto
                {
                    DisplayName = User.DisplayName,
                    Email = User.Email,
                    Token = CreateTokenAsync(User),
                };
                // return UserDto
            }
            else
            {
                throw new UnauthorizedException();
            }

        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            // Mapping RegisterDto to ApplicationUser
            var User = new ApplicationUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.Email,
            };

            // Create a new user
            var result = await _userManager.CreateAsync(User, registerDto.Password);

            // Return UserDto
            if (result.Succeeded)
                return new UserDto
                { DisplayName = User.DisplayName, Email = User.Email, Token = CreateTokenAsync(User) };

            else
            {
                // Handle errors
                var errors = result.Errors.Select(e => e.Description).ToList();

                //Throw BadRequestException
                throw new BadRequestException(errors);
            }
        }

        private static string CreateTokenAsync(ApplicationUser user)
        {
            return "Token - TODO";
        }
    }
}