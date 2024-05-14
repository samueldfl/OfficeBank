using Domain.Shared.Models;

namespace Domain.Transfer.Models;

public class TransferModel : BaseModel
{
    public decimal Amount { get; set; }

    public Guid FromAccountId { get; set; }

    public Guid ToAccountId { get; set; }
}
