using BuildingBlocks.Application.Cqrs.Commands;

namespace Users.Application.OrderResponses.Commands.Response
{
    public class ResponseCommand : ICommand
    {
        public ResponseCommand(
            Guid requestId, 
            int price, 
            int deadline, 
            DateTime enrollmentDate, double prepayment)
        {
            RequestId = requestId;
            Price = price;
            Deadline = deadline;
            EnrollmentDate = enrollmentDate;
            Prepayment = prepayment;
        }

        public Guid RequestId { get; }

        public int Price { get; }

        public int Deadline { get; }

        public DateTime EnrollmentDate { get; }

        public double Prepayment { get; }
    }
}