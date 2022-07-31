namespace DDDTest.Domain.Framework.SeedWork;
public interface IBusinessRule {
    bool IsBroken();
    string Message { get; }
}

