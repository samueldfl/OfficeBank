namespace Application.Shared.ResultStates;

public class RootOkResult : RootResult
{
    public override int Code => 200;

    public override dynamic? Body { get; set; }
}
