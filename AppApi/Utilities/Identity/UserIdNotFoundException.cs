namespace Utilities.Identity;

[Serializable]
public class UserIdNotFoundException : Exception
{
    public UserIdNotFoundException() : base("user id not found")
    {
    }

    public UserIdNotFoundException(string? message) : base(message)
    {
    }

    public UserIdNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}