using Infra.Shared.Encrypter.Services.Abstractions;

namespace Infra.Shared.Encrypter.Services.Implementations;

public class EncrypterService : IEncrypterService
{
    public string Hash(string value)
    {
        return BCrypt.Net.BCrypt.HashPassword(value);
    }
}
