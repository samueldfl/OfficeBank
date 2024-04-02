namespace Domain.Shared.Identities;

public class Notifier
{
    public bool HasFailure => Notifications.Count() is not 0;

    public ICollection<Notification> Notifications { get; } = [];
}
