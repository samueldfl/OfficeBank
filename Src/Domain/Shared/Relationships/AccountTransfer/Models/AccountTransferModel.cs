using Domain.Shared.Models;
using Domain.Account.Models;
using Domain.Transfer.Models;

namespace Domain.Shared.Relationships.AccountTransfer.Models;

public class AccountTransferModel : BaseModel
{
    public Guid TransferId { get; set; }

    public TransferModel Transfer { get; set; } = null!;

    public Guid AccountId { get; set; }

    public AccountModel Account { get; set; } = null!;
}
