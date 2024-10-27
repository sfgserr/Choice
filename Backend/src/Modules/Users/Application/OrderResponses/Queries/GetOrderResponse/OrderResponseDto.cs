namespace Users.Application.OrderResponses.Queries.GetOrderResponse
{
    public class OrderResponseDto
    {
        public OrderResponseDto(
            Guid id, 
            Guid requestId, 
            Guid clientId, 
            Guid companyId, 
            double price, 
            int deadline, 
            DateTime? enrollmentDate, 
            double prepayment, 
            string status, 
            bool isEnrolled, 
            bool isPaid, 
            bool isEnrollmentDateConfirmed, 
            bool isActive)
        {
            Id = id;
            RequestId = requestId;
            ClientId = clientId;
            CompanyId = companyId;
            Price = price;
            Deadline = deadline;
            EnrollmentDate = enrollmentDate;
            Prepayment = prepayment;
            Status = status;
            IsEnrolled = isEnrolled;
            IsPaid = isPaid;
            IsEnrollmentDateConfirmed = isEnrollmentDateConfirmed;
            IsActive = isActive;
        }

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

        public bool IsActive { get; }
    }
}