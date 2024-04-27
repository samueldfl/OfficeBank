using Domain.Payment.Models;
using Domain.Shared.Models;
using Domain.User.Models;

namespace Domain.Account.Models;

public class AccountModel : BaseModel
{
    public Guid UserId { get; set; }

    public UserModel User { get; set; } = null!;

    public ICollection<TransactionModel> Transactions { get; set; } = [];
}
