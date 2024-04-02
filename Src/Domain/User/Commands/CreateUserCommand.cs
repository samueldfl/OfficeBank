using System.Text;

using Application.Shared.RootRegex;
using Domain.Shared.Command;
using Domain.Shared.Identities;
using Domain.User.Notifications.Auth;
using Domain.User.Records;

namespace Domain.User.Commands;

public record CreateUserCommand(
    string Name,
    string Email,
    string Password,
    string CPF,
    UserAddress Address
) : ICommand
{
    public Notifier Validate()
    {
        Notifier notifier = new();

        List<Notification?> result =
        [
            ValidateName,
            ValidateEmail,
            ValidateCpf,
            ValidatePassword,
        ];

        foreach (var notification in result)
        {
            if (notification is not null)
            {
                notifier.Notifications.Add(notification);
            }
        }

        return notifier;
    }

    private Notification? ValidateName
    {
        get
        {
            if (RootRegex.SpecialChar().IsMatch(Name) || RootRegex.Number().IsMatch(Name))
                return new InvalidNameNotification();
            return null;
        }
    }

    private Notification? ValidateEmail
    {
        get
        {
            if (!RootRegex.Email().IsMatch(Email))
                return new InvalidEmailNotification();
            return null;
        }
    }

    private Notification? ValidateCpf
    {
        get
        {
            if (!RootRegex.Base64().IsMatch(CPF))
                return new Notification("", "");

            string cpf = Convert.ToBase64String(Encoding.UTF8.GetBytes(CPF));
            return null;
        }
    }

    private Notification? ValidatePassword
    {
        get
        {
            if (!RootRegex.Base64().IsMatch(Password))
                return new InvalidPasswordNotification();

            string password = Convert.ToBase64String(
                Encoding.UTF8.GetBytes(Password)
            );

            if (
                !RootRegex.SpecialChar().IsMatch(password)
                || !RootRegex.UpperCase().IsMatch(password)
                || !RootRegex.Number().IsMatch(password)
                || password.Length < 8
                || password.Length > 32
            )
                return new InvalidPasswordNotification();

            return null;
        }
    }
};
