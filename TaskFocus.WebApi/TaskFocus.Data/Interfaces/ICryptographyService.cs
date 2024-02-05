using System.Security.Cryptography;

namespace TaskFocus.Data.Interfaces;

public interface ICryptographyService
{
    string GenerateHash(string password, string salt, HashAlgorithm hashAlgorithm);
}