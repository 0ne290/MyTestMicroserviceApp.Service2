namespace Domain.Constants;

public static class StatusesOfAnyModel
{
    public static bool StatusIsValid(string status) => ValidStatuses.Contains(status);

    public const string AwaitingCallOfMethod1 = "Awaiting call of method1";

    public const string AwaitingCallOfMethod2 = "Awaiting call of method2";

    public const string AllMethodsAreCalled = "All methods are called";

    private static readonly HashSet<string> ValidStatuses = new(new[]
        { AwaitingCallOfMethod1, AwaitingCallOfMethod2, AllMethodsAreCalled });
}