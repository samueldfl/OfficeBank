namespace Domain.Shared.Encrypter;

public interface IEncrypterService
{
    public string Hash(string value);
}
