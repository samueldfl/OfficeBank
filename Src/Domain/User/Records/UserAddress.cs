namespace Domain.User.Records;

public record UserAddress(
    string Street,
    int Number,
    string ZipCode,
    string Neighborhood,
    string City,
    string State,
    string Country
);
