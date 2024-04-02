namespace Infra.Shared.Encrypter.Services.Abstractions;

public interface IEncrypterService
{
    public string Hash(string value);
}
