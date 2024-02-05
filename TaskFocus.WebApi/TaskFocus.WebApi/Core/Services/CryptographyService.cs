using System.Security.Cryptography;
using System.Text;
using TaskFocus.Data.Interfaces;

namespace TaskFocus.WebApi.Core.Services;

public class CryptographyService : ICryptographyService
{
    public string GenerateHash(string password, string salt, HashAlgorithm hashAlgorithm)
    {
        var passwordAsBytes = Encoding.UTF8.GetBytes(password);

        var passwordWithSaltBytes = new List<byte>();

        passwordWithSaltBytes.AddRange(passwordAsBytes);

        var saltBytes = Encoding.UTF8.GetBytes(salt);

        passwordWithSaltBytes.AddRange(saltBytes);
            
        var hashBytes = hashAlgorithm.ComputeHash(passwordWithSaltBytes.ToArray());

        return Convert.ToBase64String(hashBytes);
    }
}