namespace WebApi.Modules.Users.OrderResponses
{
    public class ResponseRequest
    {
        public ResponseRequest(
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