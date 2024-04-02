namespace Domain.User.Notifications.Auth;

public record InvalidPasswordNotification : AuthNotification
{
    public InvalidPasswordNotification() : base("Password", "The provided password is invalid!") { }
}
