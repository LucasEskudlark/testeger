namespace Testeger.Shared.Exceptions;

public class TestCaseNotFoundException : Exception
{
    public TestCaseNotFoundException()
    {
    }

    public TestCaseNotFoundException(string message)
        : base(message)
    {
    }

    public TestCaseNotFoundException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
