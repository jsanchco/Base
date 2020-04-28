namespace SGI.API.Controllers
{
    #region Using

    using Microsoft.AspNetCore.Mvc;
    using System;
    using Microsoft.AspNetCore.Authorization;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Domain.Supervisor;
    using Models;
    using System.IdentityModel.Tokens.Jwt;
    using System.Text;
    using Microsoft.IdentityModel.Tokens;
    using System.Security.Claims;
    using Microsoft.Extensions.Options;
    using SGI.Domain.Helpers;

    #endregion

    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly ISupervisor _supervisor;
        private readonly ILogger<AuthenticateController> _logger;
        private readonly IOptions<JwtAppSettings> _config;

        public AuthenticateController(ILogger<AuthenticateController> logger, ISupervisor supervisor, IOptions<JwtAppSettings> config)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));

            _supervisor = supervisor ??
                throw new ArgumentNullException(nameof(supervisor));

            _config = config ??
                throw new ArgumentNullException(nameof(config));
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            try
            {
                var userAuthenticate = await _supervisor.Login(login.username, login.password);

                if (userAuthenticate == null)
                {
                    _logger.LogWarning("Error in Authenticate: username [{Username}] not registered or incorrect password", login.username);
                    return BadRequest(new { message = "Username or password is incorrect" });
                }

                return new ObjectResult(new Session
                {
                    user = userAuthenticate,
                    token = getToken(userAuthenticate.id.ToString())
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(500, ex.Message);
            }
        }

        private string getToken(string id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.Value.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, id)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                //Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return (tokenHandler.WriteToken(token));
        }
    }
}