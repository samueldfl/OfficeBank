namespace Domain.Shared.Identities;

public record Notification
{
    public string Key { get; private set; }

    public string Value { get; private set; }

    public Notification(string key, string value)
    {
        Key = key;
        Value = value;
    }
}
