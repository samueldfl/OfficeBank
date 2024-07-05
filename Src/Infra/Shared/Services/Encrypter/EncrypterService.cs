using Domain.Shared.Services.Encrypter;

namespace Infra.Shared.Services.Encrypter;

internal sealed class EncrypterService : IEncrypterService
{
    public string Hash(string value) => BCrypt.Net.BCrypt.HashPassword(value);

    public bool VerifyHash(string hash, string compareHash) =>
        BCrypt.Net.BCrypt.Verify(hash, compareHash);
}
