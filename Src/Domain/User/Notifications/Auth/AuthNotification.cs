namespace Domain.User.Notifications.Auth;

public record AuthNotification : UserNotification
{
    public AuthNotification(string key, string value) : base(key, value) { }
}
