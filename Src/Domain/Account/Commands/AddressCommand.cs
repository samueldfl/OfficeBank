namespace Domain.Account.Records;

public record AddressCommand(
    string Street,
    int Number,
    string ZipCode,
    string Neighborhood,
    string City,
    string State,
    string Country
);
