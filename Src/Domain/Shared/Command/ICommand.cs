using Domain.Shared.Identities;

namespace Domain.Shared.Command;

public interface ICommand
{
    Notifier Validate();
}
