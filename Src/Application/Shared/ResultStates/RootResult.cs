namespace Application.Shared.ResultStates;

public abstract class RootResult
{
    public abstract int Code { get; }

    public abstract dynamic? Body { get; set; }
}
