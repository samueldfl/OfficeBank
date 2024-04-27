namespace Application.Shared.ResultStates;

public class RootFailureResult : RootResult
{
    public override int Code => 500;

    public override dynamic? Body { get; set; }
}
