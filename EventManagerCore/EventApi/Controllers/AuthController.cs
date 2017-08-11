using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DataTransfer;
using EventApi.Services.Filters;

namespace EventApi.Controllers
{
    [ValidateModelAttribute]
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly UserManager<VisitorDto> _userManager;
        private readonly IPasswordHasher<VisitorDto> _hasher;
        private readonly IConfigurationRoot _config;

        public AuthController(ILogger<AuthController> logger,
            UserManager<VisitorDto> userManager,
            IPasswordHasher<VisitorDto> hasher,
            IConfigurationRoot config)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        [HttpPost("api/auth/token")]
        public async Task<IActionResult> CreateToken([FromBody]CredentialModel model)
        {
            try
            {
                VisitorDto user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    if (_hasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) == PasswordVerificationResult.Success)
                    {
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:JwtKey"]));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                            issuer: _config["Token:Issuer"],
                            audience: _config["Token:Audience"],
                            claims: claims, 
                            expires: DateTime.UtcNow.AddMinutes(20),
                            signingCredentials: creds
                            );

                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token) ,
                            expiration  = token.ValidTo
                        });
                    }
                }
            }
            catch (Exception e)
            {

                _logger.LogWarning(3, e, "Cannot login user");
            }

            return BadRequest();
        }

        [HttpPost("api/auth/register")]
        public async Task<IActionResult> Register([FromBody]VisitorDto dto)
        {
            try
            {
                var result = await _userManager.CreateAsync(dto);
                if(result == IdentityResult.Success)
                {
                    return Ok();
                }
            }
            catch (Exception e)
            {
                _logger.LogWarning(3, e, "Cannot register user.");
            }

            return BadRequest();
        }
    }
}
