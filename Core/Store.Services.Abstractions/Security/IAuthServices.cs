using Store.Shared.Dtos.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Abstractions.Security
{
    
    public interface IAuthServices
    {
        // Define methods related to authentication and authorization here
        Task<LoginResult> LoginAsync(LoginResponse response); // method for user login
        Task<LoginResult> RegisterAsync(RegisterResponse response); // method for user register
        Task<bool> CheckEmailExsistAsync(string email); // method for checking email existence
        Task<LoginResult?> GetCurrentUserAsync(string email); // method for getting current user by email
        Task<AddressResponse?> GetCurrentUserAddressAsync(string email); // method for getting current user address by email
        Task<AddressResponse?> UpdateCurrentUserAddressAsync(AddressResponse address,string email); // method for updating current user address by email
    }
}
