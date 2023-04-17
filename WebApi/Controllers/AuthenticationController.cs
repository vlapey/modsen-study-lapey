using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApi.Dto;
using WebApi.Jwt;

namespace WebApi.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _configuration;

    public AuthenticationController(UserManager<IdentityUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    [HttpPost]
    public async Task<IActionResult> Register(UserRegistrationRequestDto requestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        
        var userExist = await _userManager.FindByEmailAsync(requestDto.Email);
            
            if (userExist is not null)
            {
                return BadRequest(new AuthResult()
                {
                    Result = false,
                    Errors = new List<string>()
                    {
                        "Email already exists"
                    }
                });
            }

            var newUser = new IdentityUser()
            {
                Email = requestDto.Email,
                UserName = requestDto.Email
            };
            
            var isCreated = await _userManager.CreateAsync(newUser, requestDto.Password);

            if (isCreated.Succeeded)
            {
                var token = GenerateJwtToken(newUser);
                return Ok(new AuthResult()
                {
                    Result = true,
                    Token = token
                });
            }

            return BadRequest(new AuthResult()
            {
                Errors = new List<string>()
                {
                    "Server error"
                },
                Result = false
            });
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(UserLoginRequestDto loginRequest) 
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new AuthResult()
            {
                Errors = new List<string>()
                {
                    "Invalid payload"
                },
                Result = false
            });
        }

        var existingUser = await _userManager.FindByEmailAsync(loginRequest.Email);

        if (existingUser is null)
        {
            return BadRequest(new AuthResult()
            {
                Errors = new List<string>()
                {
                    "Invalid payload"
                },
                Result = false
            });
        }

        var isCorrect = await _userManager.CheckPasswordAsync(existingUser ,loginRequest.Password);

        if (!isCorrect)
        {
            return BadRequest(new AuthResult()
            {
                Errors = new List<string>()
                {
                    "Invalid credentials"
                },
                Result = false
            });
        }

        var jwtToken = GenerateJwtToken(existingUser);

        return Ok(new AuthResult()
        {
            Token = jwtToken,
            Result = true
        });
    }

    private string GenerateJwtToken(IdentityUser user)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();
        
        var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JwtConfig:Secret").Value);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new []
            {
                new Claim("Id", user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, value:user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString())
            }),
            
            Expires = DateTime.Now.AddHours(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var token = jwtTokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = jwtTokenHandler.WriteToken(token);
        
        return jwtToken;
    }
}