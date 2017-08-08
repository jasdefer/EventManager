using DataLayer.DataModel;
using EventApi.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ValidationRules.Dto;

namespace EventApi.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> Logger;
        private readonly UserManager<VisitorDto> UserManager;
        private readonly IPasswordHasher<VisitorDto> Hasher;
        private readonly IConfigurationRoot Config;

        public AuthController(ILogger<AuthController> logger,
            UserManager<VisitorDto> userManager,
            IPasswordHasher<VisitorDto> hasher,
            IConfigurationRoot config)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            UserManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            Hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
            Config = config ?? throw new ArgumentNullException(nameof(config));
        }

        [HttpPost("api/auth/token")]
        public async Task<IActionResult> CreateToken([FromBody] CrendentialModel model)
        {
            try
            {
                VisitorDto user = await UserManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    if (Hasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) == PasswordVerificationResult.Success)
                    {
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["Token:JwtKey"]));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                            issuer: Config["Token:Issuer"],
                            audience: Config["Token:Audience"],
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

                Logger.LogWarning(3, e, "Cannot login user");
            }

            return BadRequest();
        }

        [HttpPost("api/auth/register")]
        public async Task<IActionResult> Register([FromBody]VisitorDto dto)
        {
            try
            {
                var result = await UserManager.CreateAsync(dto);
                if(result == IdentityResult.Success)
                {
                    return Ok();
                }
            }
            catch (Exception e)
            {
                Logger.LogWarning(3, e, "Cannot register user.");
            }

            return BadRequest();
        }
    }
}
