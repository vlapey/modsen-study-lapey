using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using WebApi.Configurations;

namespace WebApi.Startup;

public static class JwtExtension
{
    public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtConfig>(configuration.GetSection("JwtConfig"));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(jwt =>
        {
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("JwtConfig:Secret").Value);

            jwt.SaveToken = true;
            jwt.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false, //when on prod it should be true
                ValidateAudience = false, //when on prod it should be true
                RequireExpirationTime = false, //for dev -- needs to be updated when refresh token is added
                ValidateLifetime = true
            };
        });
        services.AddDefaultIdentity<IdentityUser>(options =>
            options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<AppDbContext>();
    }
}