using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TaskFocus.WebApi.Core.Configs;

namespace TaskFocus.WebApi.Startup;

public static class ConfigureJwtOptionsExtension
{
    public static void AddTokenAuthentication(this IServiceCollection services, JwtSettings authenticationOptions)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authenticationOptions.Issuer,
                    ValidateIssuer = authenticationOptions.ValidateIssuer,
                    ValidAudience = authenticationOptions.Audience,
                    ValidateAudience = authenticationOptions.ValidateAudience,
                    ValidateLifetime = authenticationOptions.ValidateLifetime,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationOptions.SecretKey)),
                    ValidateIssuerSigningKey = authenticationOptions.ValidateIssuerSigningKey,
                    ClockSkew = TimeSpan.Zero
                };
            });

        services.AddAuthorization(
            //     options =>
            // {
            //     options.AddPolicy("Api", policy =>
            //     {
            //         policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
            //         policy.RequireAuthenticatedUser();
            //     });
            // }
        );
    }
}