namespace Domain.Services.Encrypter;

public interface IEncrypterService
{
    public string Hash(string value);
}
