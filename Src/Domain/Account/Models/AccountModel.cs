using System.Text;
using Domain.Account.Commands;
using Domain.Shared.Models;
using Domain.Transaction.Models;

namespace Domain.Account.Models;

public class AccountModel : BaseModel
{
    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string CPF { get; set; } = string.Empty;

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeleteAt { get; set; }

    public AddressModel Address { get; set; } = null!;

    public ICollection<TransactionModel> Transactions { get; set; } = [];

    public static implicit operator AccountModel(RegisterAccountCommand command) =>
        new()
        {
            Name = command.Name.ToUpperInvariant(),
            Email = command.Email.ToLowerInvariant().Replace(" ", ""),
            Password = Convert.ToBase64String(Encoding.UTF8.GetBytes(command.Password)),
            CPF = Convert
                .ToBase64String(Encoding.UTF8.GetBytes(command.CPF))
                .Replace(".", "")
                .Replace("-", ""),
            Address = command.Address,
        };
}
