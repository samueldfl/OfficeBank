namespace Domain.Shared.Exceptions;

public abstract class RootException : Exception
{
    public override string Message { get; }

    public RootException(string message)
        : base(message)
    {
        Message = message;
    }
}
