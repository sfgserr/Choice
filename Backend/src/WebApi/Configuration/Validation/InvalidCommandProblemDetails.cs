using BuildingBlocks.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Configuration.Validation
{
    public class InvalidCommandProblemDetails : ProblemDetails
    {
        public InvalidCommandProblemDetails(InvalidCommandException exception)
        {
            Title = "Command validation error";
            Status = StatusCodes.Status400BadRequest;
            Errors = exception.Errors;
        }

        public List<string> Errors { get; }
    }
}
