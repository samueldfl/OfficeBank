namespace Application.Shared.ResultStates;

public class RootCreatedResult : RootResult
{
    public override int Code => 201;

    public override dynamic? Body { get; set; }
}
