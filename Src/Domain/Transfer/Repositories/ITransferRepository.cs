using Domain.Transfer.Models;

namespace Domain.Transfer.Repositories;

public interface ITransferRepository
{
    Task CreateAsync(TransferModel model, CancellationToken cancellationToken = default);
}
