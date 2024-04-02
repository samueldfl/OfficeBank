using Domain.User.Models;
using Domain.Shared.Models;
using Domain.Shared.Relationships.AccountTransfer.Models;

namespace Domain.Account.Models;

public class AccountModel : BaseModel
{
    public decimal Amount { get; private set; }

    public Guid UserId { get; set; }

    public UserModel User { get; set; } = null!;

    public IEnumerable<AccountTransferModel> Transfers { get; set; } = [];
}
