using Domain.Account.Records;
using Domain.Shared.Models;

namespace Domain.Account.Models;

public class AddressModel : BaseModel
{
    public string Street { get; set; } = string.Empty;

    public int Number { get; set; }

    public string ZipCode { get; set; } = string.Empty;

    public string Neighborhood { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string State { get; set; } = string.Empty;

    public string Country { get; set; } = string.Empty;

    public Guid AccountId { get; private set; }

    public AccountModel Account { get; private set; } = null!;

    public static implicit operator AddressModel(AddressCommand address)
    {
        return new()
        {
            Street = address.Street,
            Number = address.Number,
            ZipCode = address.ZipCode,
            Neighborhood = address.Neighborhood,
            City = address.City,
            State = address.State,
            Country = address.Country,
        };
    }
}
