namespace TaskFocus.WebApi.Core.Configs;

public class JwtSettings
{
    public string Issuer { get; set; }

    public string Audience { get; set; }

    public int Lifetime { get; set; }

    public int RefreshTokenLifetime { get; set; }

    public string SecretKey { get; set; }

    public bool ValidateLifetime { get; set; }

    public bool ValidateIssuer { get; set; }

    public bool ValidateAudience { get; set; }

    public bool ValidateIssuerSigningKey { get; set; }
}