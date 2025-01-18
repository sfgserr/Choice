namespace Users.Application.OrderRequests.Queries.GetOrderRequestAsCompany
{
    public class OrderRequestDto
    {
        public Guid Id { get; }

        public bool ToKnowPrice { get; }

        public bool ToKnowDeadline { get; }

        public bool ToKnowEnrollmentDate { get; }
    }
}