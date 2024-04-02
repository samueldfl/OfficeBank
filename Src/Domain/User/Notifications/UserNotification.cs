using Domain.Shared.Identities;

namespace Domain.User.Notifications;

public record UserNotification : Notification
{
    public UserNotification(string key, string value) : base(key, value) { }
}
