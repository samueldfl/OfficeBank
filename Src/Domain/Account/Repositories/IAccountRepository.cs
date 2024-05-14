using Domain.Account.Models;
using Domain.Shared.Repositories.Create;
using Domain.Shared.Repositories.Read;

namespace Domain.Account.Repositories;

public interface IAccountRepository
    : ICreateRepository<AccountModel>,
        IRead<AccountModel>,
        IReadAsNoTracking<AccountModel> { }
