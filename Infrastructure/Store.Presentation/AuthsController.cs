using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Services.Abstractions;
using Store.Shared.Dtos.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Store.Presentation
{
    // API controller for authentication-related endpoints
    // Route is set to "api/auths"
    // Inherits from ControllerBase to provide API functionalities
    [ApiController]
    [Route("api/[controller]")]
    public class AuthsController(IServiceManager _serviceManager) : ControllerBase
    {
        #region Login == SignIn
        [HttpPost("login")] // POST api/auths/login
        public async Task<IActionResult> Login(LoginResponse login)
        {
            var result = await _serviceManager.AuthServices.LoginAsync(login);
            return Ok(result);
        }
        #endregion

        #region Register == SignUp 
        [HttpPost("register")] // POST api/auths/register
        public async Task<IActionResult> Register(RegisterResponse register)
        {
            var result = await _serviceManager.AuthServices.RegisterAsync(register);
            return Ok(result);
        }
        #endregion

        #region CheckEmailExsist
        [HttpGet("EmailExists")]
        public async Task<IActionResult> CheckEmailExsist(string email)
        {
            var result = await _serviceManager.AuthServices.CheckEmailExsistAsync(email);
            return Ok(result);
        }
        #endregion

        #region GetCurrentUser
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var email = User.FindFirst(ClaimTypes.Email).Value;
            var result = await _serviceManager.AuthServices.GetCurrentUserAsync(email);
            return Ok(result);
        }
        #endregion

        #region GetCurrentUserAddress
        [HttpGet("Address")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUserAddress()
        {
            var email = User.FindFirst(ClaimTypes.Email).Value;
            var result = await _serviceManager.AuthServices.GetCurrentUserAddressAsync(email);
            return Ok(result);
        }
        #endregion

        #region UpdateCurrentUserAddress
        [HttpPut("Address")]
        [Authorize]
        public async Task<IActionResult> UpdateCurrentUserAddress(AddressResponse address)
        {
            var email = User.FindFirst(ClaimTypes.Email).Value;
            var result = await _serviceManager.AuthServices.UpdateCurrentUserAddressAsync(address,email);
            return Ok(result);
        }
        #endregion
    }
}
