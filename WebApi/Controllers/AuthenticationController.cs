using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApi.Dto;

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
        var user = await _userManager.FindByEmailAsync(requestDto.Email);

        if (user is not null)
        {
            return BadRequest("Email already exists");
        }

        var newUser = new IdentityUser()
        {
            Email = requestDto.Email,
            UserName = requestDto.Email
        };

        var identityResult = await _userManager.CreateAsync(newUser, requestDto.Password);

        if (!identityResult.Succeeded)
        {
            return BadRequest("Identity result creation does not indicate success");
        }

        try
        {
            var token = GenerateJwtToken(newUser);
            return Ok(token);
        }
        catch (Exception e)
        {
            return BadRequest("GenerateJwtToken does not indicate success");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Login(UserLoginRequestDto loginRequest)
    {
        var user = await _userManager.FindByEmailAsync(loginRequest.Email);

        if (user is null)
        {
            return BadRequest("Unable to find user");
        }

        var isCorrect = await _userManager.CheckPasswordAsync(user, loginRequest.Password);

        if (!isCorrect)
        {
            return BadRequest("Password does not match following user");
        }

        try
        {
            var jwtToken = GenerateJwtToken(user);
            return Ok(jwtToken);
        }
        catch (Exception e)
        {
            return BadRequest("GenerateJwtToken does not indicate success");
        }
    }

    private string GenerateJwtToken(IdentityUser user)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JwtConfig:Secret").Value);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, value: user.Email),
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