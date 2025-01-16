
namespace BuildingBlocks.Application.Exceptions
{
    public class InvalidCommandException : Exception
    {
        public InvalidCommandException(List<string> errors)
        {
            Errors = errors;
        }

        public List<string> Errors { get; }

        public static void ThrowIfNull<T>(T? obj) where T : class
        {
            if (obj is null) throw new InvalidCommandException([$"Entity of type {typeof(T).Name} is not found"]);
        }
    }
}
