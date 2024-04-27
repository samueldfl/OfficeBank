namespace Domain.Shared.ValidationStates;

public abstract class ValidationState { 
    public virtual Dictionary<string, string> Body { get; set; } = [];
}
