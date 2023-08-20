using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using UserService.Domain.Aggregates.UserAggregate;

namespace UserService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;

    public AuthController(
        IConfiguration configuration, 
        IUserRepository userRepository)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    [HttpPost("Token")]
    public async Task<IActionResult> GetToken([FromForm] string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user is null)
        {
            return BadRequest();
        }

        var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!);
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Claims = new Dictionary<string, object>
            {
                { "userId", user.Id },
                { "name", user.Name },
                { "email", user.Email }
            },
            Issuer = "me",
            Audience = "you",
            IssuedAt = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey), 
                SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return Ok(new
        {
            AccessToken = tokenHandler.WriteToken(securityToken)
        });
    }
}
