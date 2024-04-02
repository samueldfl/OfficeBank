namespace Domain.User.Notifications.Auth;

public record InvalidNameNotification : AuthNotification
{
    public InvalidNameNotification() : base("key", "value") { }
}
