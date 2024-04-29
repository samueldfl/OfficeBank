using Domain.Shared.Exceptions;

namespace Domain.Account.Exceptions;

public class AccountNotFoundException : RootException
{
    public AccountNotFoundException()
        : base("message") { }
}
