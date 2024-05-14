using Domain.Services.Encrypter;

namespace Infra.Shared.Encrypter;

internal sealed class EncrypterService : IEncrypterService
{
    public string Hash(string value) => BCrypt.Net.BCrypt.HashPassword(value);
}
