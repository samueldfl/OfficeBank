using System.Text;
using Domain.Shared.Models;
using Domain.Account.Models;
using Domain.User.Commands;

namespace Domain.User.Models;

public class UserModel : BaseModel
{
    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string CPF { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
    
    public DateTime? DeleteAt { get; set; }

    public UserAddressModel Address { get; set; } = null!;

    public AccountModel Account { get; } = new();

    public static implicit operator UserModel(CreateUserCommand command) 
    {
        return new()
        {
            Name = command.Name.ToUpperInvariant(),
            Email = command.Email.ToLowerInvariant().Replace(" ", ""),
            Password = Convert.ToBase64String(Encoding.UTF8.GetBytes(command.Password)),
            CPF = Convert.ToBase64String(Encoding.UTF8.GetBytes(command.CPF)).Replace(".", "").Replace("-", ""),
            Address = command.Address,
        };
    }
}
