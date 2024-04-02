namespace Domain.User.Notifications.Auth;

public record InvalidEmailNotification : AuthNotification
{
    public InvalidEmailNotification() : base("Email", "The provided email is invalid!") { }
}
