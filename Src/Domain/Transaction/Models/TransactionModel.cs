using Domain.Account.Models;
using Domain.Shared.Models;

namespace Domain.Transaction.Models;

public sealed class TransactionModel : BaseModel
{
    public decimal Balance { get; set; }

    public decimal LastBalance { get; set; }

    public decimal BalanceDiff { get; set; }

    public Guid AccountId { get; set; }

    public AccountModel Account { get; set; } = null!;
}
