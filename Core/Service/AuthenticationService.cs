using AutoMapper;
using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServicesAbstracion;
using Shared.DataTransferObjects.IdentityDtos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager
        , IConfiguration _configuration, IMapper _mapper) : IAuthenticationService
    {
        public async Task<bool> CheckEmailAsync(string email)
        {
            // Check if the Email exists
            var User = await _userManager.FindByEmailAsync(email);
            return User is not null;
        }

        public async Task<UserDto> GetCurrentUserAsync(string email)
        {
            // Check if the Email exists
            var User = await _userManager.FindByEmailAsync(email) ?? throw new UserNotFoundException(email);
            return new UserDto
            {
                DisplayName = User.DisplayName,
                Email = User.Email,
                Token = await CreateTokenAsync(User),
            };
        }

        public async Task<AddressDto> GetCurrentUserAddressAsync(string email)
        {
            var User = await _userManager.Users.Include(x => x.Address)
                .FirstOrDefaultAsync(x => x.Email == email) ?? throw new UserNotFoundException(email);
            if (User.Address is not null)
                return _mapper.Map<AddressDto>(User.Address);
            else
                throw new AddressNotFoundException(User.UserName);

        }

        public async Task<AddressDto> UpdateCurrentUserAddressAsync(AddressDto addressDto, string email)
        {
            var User = await _userManager.Users.Include(x => x.Address)
                .FirstOrDefaultAsync(x => x.Email == email) ?? throw new UserNotFoundException(email);

            if (User.Address is not null)
            {
                User.Address.FirstName = addressDto.FirstName;
                User.Address.LastName = addressDto.LastName;
                User.Address.City = addressDto.City;
                User.Address.Street = addressDto.Street;
                User.Address.Country = addressDto.Country;
            }
            else
            {
                User.Address = _mapper.Map<Address>(addressDto);
            }

            await _userManager.UpdateAsync(User);
            return _mapper.Map<AddressDto>(User.Address);

        }

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
                    Token = await CreateTokenAsync(User),
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
                { DisplayName = User.DisplayName, Email = User.Email, Token = await CreateTokenAsync(User) };

            else
            {
                // Handle errors
                var errors = result.Errors.Select(e => e.Description).ToList();

                //Throw BadRequestException
                throw new BadRequestException(errors);
            }
        }

        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Name, user.DisplayName)
            };
            var Roles = await _userManager.GetRolesAsync(user);

            foreach (var role in Roles)
                Claims.Add(new Claim(ClaimTypes.Role, role));

            var SecretKey = _configuration.GetSection("JWTOptions")["SecretKey"];
            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

            var Credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken
            (
                issuer: _configuration["JWTOptions:Issuer"],
                audience: _configuration["JWTOptions:Audience"],
                claims: Claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: Credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}