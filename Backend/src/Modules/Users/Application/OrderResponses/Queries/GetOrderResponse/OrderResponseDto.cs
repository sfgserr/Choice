namespace Users.Application.OrderResponses.Queries.GetOrderResponse
{
    public class OrderResponseDto
    {
        public Guid Id { get; }

        public Guid RequestId { get; }

        public Guid ClientId { get; }
        
        public Guid CompanyId { get; }

        public double Price { get; }

        public int Deadline { get; }

        public DateTime? EnrollmentDate { get; }

        public double Prepayment { get; }

        public string Status { get; }
        
        public bool IsEnrolled { get; }

        public bool IsPaid { get; }
        
        public bool IsEnrollmentDateConfirmed { get; }
        
        public Guid? UserChangedEnrollmentDate { get; }

        public bool IsActive { get; }
    }
}