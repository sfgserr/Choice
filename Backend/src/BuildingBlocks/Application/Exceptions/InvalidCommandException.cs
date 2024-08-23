
namespace BuildingBlocks.Application.Exceptions
{
    public class InvalidCommandException : Exception
    {
        public InvalidCommandException(List<string> errors)
        {
            Errors = errors;
        }

        public List<string> Errors { get; }
    }
}
