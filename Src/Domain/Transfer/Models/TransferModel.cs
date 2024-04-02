using Domain.Shared.Models;
using Domain.Account.Models;
using Domain.Shared.Relationships.AccountTransfer.Models;

namespace Domain.Transfer.Models;

public class TransferModel : BaseModel
{
    public decimal Value { get; set; }

    public Guid FromAccountId { get; set; }

    public AccountModel FromAccount { get; set; } = null!;

    public Guid ToAccountId { get; set; }

    public AccountModel ToAccount { get; set; } = null!;

    public IEnumerable<AccountTransferModel> Accounts { get; set; } = [];
}
