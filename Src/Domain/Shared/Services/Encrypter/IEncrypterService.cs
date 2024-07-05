namespace Domain.Shared.Services.Encrypter;

public interface IEncrypterService
{
    public string Hash(string value);

    public bool VerifyHash(string hash, string compareHash);
}
