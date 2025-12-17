
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Store.Domain.Entities.Identity;
using Store.Domain.Exceptions;
using Store.Services.Abstractions.Security;
using Store.Shared.Dtos.OptionsPattern;
using Store.Shared.Dtos.Security;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Security
{
    public class AuthServices(UserManager<AppUser> _userManager,IOptions <JwtOptions> options,IMapper _mapper) : IAuthServices
    {
        public async Task<bool> CheckEmailExsistAsync(string email)
        {
            var flag = await _userManager.FindByEmailAsync(email);
            if (flag is null)
            {
                throw new EmailNotFoundException(email);
            }
            return true;
        }

        public async Task<LoginResult?> GetCurrentUserAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                throw new UserNotFoundException(email);
            }
            return new LoginResult()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = await GenerateJwtToken(user)
            };
        }

        public async Task<AddressResponse?> GetCurrentUserAddressAsync(string email)
        {
            var user = await _userManager.Users.Include(U => U.Address).FirstOrDefaultAsync(U => U.Email.ToLower() == email.ToLower()); // Include Address navigation property
            if (user is null)
            {
                throw new UserNotFoundException(email);
            }
            return _mapper.Map<AddressResponse>(user.Address); // Map Address entity to AddressResponse DTO
        }


        public async Task<AddressResponse?> UpdateCurrentUserAddressAsync(AddressResponse address, string email)
        {
            var user = await _userManager.Users.Include(U => U.Address).FirstOrDefaultAsync(U => U.Email.ToLower() == email.ToLower()); // Include Address navigation property
            if (user is null)
            {
                throw new UserNotFoundException(email);
            }
            if (user.Address is null)
            {
                // If user has no address, create a new one:
                user.Address = _mapper.Map<Address>(address);
            }
            else
            {
                // Update existing address:
                user.Address.FirstName = address.FirstName;
                user.Address.LastName = address.LastName;
                user.Address.Street = address.Street;
                user.Address.City = address.City;
                user.Address.Country = address.Country;

            }
            await _userManager.UpdateAsync(user); // Update user with new address in database
            return _mapper.Map<AddressResponse>(user.Address); // Map Address entity to AddressResponse DTO
        }

        public async Task<LoginResult> LoginAsync(LoginResponse response)
        {
            // Implementation for user login:
            // 1. Check if the user exists using _userManager
            var user = await _userManager.FindByEmailAsync(response.Email); // Assuming LoginResponse has an Email property
            if (user is null)
            {
                throw new UnAuthorizedException();
            }
            var flag = await _userManager.CheckPasswordAsync(user, response.Password); // Assuming LoginResponse has a Password property
            if (!flag)
            {
                throw new UnAuthorizedException(); // Invalid password
            }
            return new LoginResult()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = await GenerateJwtToken(user)
            };
        }

        public async Task<LoginResult> RegisterAsync(RegisterResponse response)
        {
            var user = new AppUser()
            {
                DisplayName = response.DisplayName,
                Email = response.Email,
                UserName = response.UserName,
                PhoneNumber = response.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, response.Password); // Create user with password
            if (!result.Succeeded) // check if creation succeeded
            {
                var errors = result.Errors.Select(e => e.Description); // collect error descriptions
                throw new ValidationException(errors);
            }
            return new LoginResult()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = await GenerateJwtToken(user)
            };
        }


        private async Task<string> GenerateJwtToken(AppUser user)
        {
            // Implementation for generating JWT token for the authenticated user
            // 1. Header
            // 2. Payload
            // 3. Signature

            var JwtOptions = options.Value;

            var authClaims = new List<Claim>() // Create claims for the token
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email,user.Email)
            };
            var roles = await _userManager.GetRolesAsync(user); // Get user roles
            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role)); // Add role claims
            }
            // STRONGSECURITYKEYFORAUTHNTICATIONSTRONGSECURITYKEYFORAUTHNTICATION

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.SecretKey));
            var token = new JwtSecurityToken
                (
                   issuer: JwtOptions.Issuer,
                   audience: JwtOptions.Audience,
                   claims: authClaims,
                   expires: DateTime.Now.AddDays(JwtOptions.DurationInDays),
                   signingCredentials : new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(token); // To Generate Token
        }
    }
}
