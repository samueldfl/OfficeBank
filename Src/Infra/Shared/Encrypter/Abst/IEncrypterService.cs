namespace Infra.Shared.Encrypter.Abst;

public interface IEncrypterService
{
    public string Hash(string value);
}
