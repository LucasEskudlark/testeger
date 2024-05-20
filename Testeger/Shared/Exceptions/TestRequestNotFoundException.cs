namespace Testeger.Shared.Exceptions;

public class TestRequestNotFoundException : Exception
{
    public TestRequestNotFoundException()
    {
    }

    public TestRequestNotFoundException(string message)
        : base(message)
    {
    }

    public TestRequestNotFoundException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
