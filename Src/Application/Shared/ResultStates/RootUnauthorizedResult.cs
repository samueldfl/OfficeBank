namespace Application.Shared.ResultStates;

public class RootUnauthorizedResult : RootResult
{
    public override int Code => 401;

    public override dynamic? Body { get; set; }
}
