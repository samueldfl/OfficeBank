using Domain.Shared.Encrypter;

namespace Infra.Shared.Encrypter;

internal sealed class EncrypterService : IEncrypterService
{
    public string Hash(string value) => BCrypt.Net.BCrypt.HashPassword(value);
}
