using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWTProject.Core.Entities;
using JWTProject.Core.Services;
using Microsoft.Extensions.Logging;

namespace JWTProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthenticationService authenticationService, ILogger<AuthController> logger)
        {
            _authenticationService = authenticationService;
            _logger = logger;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest login)
        {
            var controlLogin = Validator.Validator.LoginValidator(login);
            if (!string.IsNullOrWhiteSpace(controlLogin))
            {
                return BadRequest(new ResponseEntity(controlLogin));
            }
            var result = await _authenticationService.CreateTokenAsync(login);
            if (!result.isSuccess)
                return BadRequest(result);
            _logger.LogInformation($"{login.UserName} kullanıcısı giriş yaptı.");
            return Ok(result);
        }

        [HttpPost("CreateTokenByRefreshToken")]
        public async Task<IActionResult> CreateTokenByRefreshToken(string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                return BadRequest(new ResponseEntity("Refresh Token boş gönderilemez."));
            }
            var result = await _authenticationService.CreateTokenByRefreshTokenAsync(refreshToken);
            if(!result.isSuccess)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
