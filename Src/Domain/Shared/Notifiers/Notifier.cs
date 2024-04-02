namespace Domain.Shared.Notifiers;

public class Notifier
{
    public string Key { get; }

    public string Value { get; }

    public Notifier(string key, string value)
    {
        Key = key;
        Value = value;
    }
}
