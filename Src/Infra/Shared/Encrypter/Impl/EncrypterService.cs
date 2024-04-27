using Infra.Shared.Encrypter.Abst;

namespace Infra.Shared.Encrypter.Impl;

internal sealed class EncrypterService : IEncrypterService
{
    public string Hash(string value)
    {
        return BCrypt.Net.BCrypt.HashPassword(value);
    }
}
