namespace Application.Shared.ResultStates;

public class RootBadRequestResult : RootResult
{
    public override int Code => 400;

    public override dynamic? Body { get; set; }
}
