using Domain.Transfer.Models;

namespace Domain.Transfer.Repositories;

public interface ITransferRepository
{
    void Create(TransferModel model);
}
