using Choice.Common.SeedWork;

namespace Choice.Ordering.Domain.OrderEntity
{
    public class Order : Entity
    {
        public Order(int orderRequestId, string companyId, string clientId, int price, int prepayment,
            int deadline, DateTime? enrollmentDate)
        {
            OrderRequestId = orderRequestId;
            CompanyId = companyId;
            ClientId = clientId;
            Price = price;
            Prepayment = prepayment;
            Deadline = deadline;
            EnrollmentDate = enrollmentDate;
        }

        public int OrderRequestId { get; private set; }
        public string CompanyId { get; private set; }
        public string ClientId { get; private set; }
        public int Price { get; private set; }
        public int Prepayment { get; private set; }
        public int Deadline { get; private set; }
        public bool IsEnrolled { get; private set; } = false;
        public DateTime? EnrollmentDate { get; private set; }
        public bool IsDateConfirmed { get; private set; } = true;
        public List<string> Reviews { get; private set; } = [];
        public OrderStatus Status { get; private set; } = OrderStatus.Active;
        public string? UserChangedEnrollmentDateGuid { get; private set; }

        public void SetEnrollmentDate(DateTime? newDate, string userGuid)
        {
            EnrollmentDate = newDate;
            IsDateConfirmed = !(userGuid == ClientId);
            UserChangedEnrollmentDateGuid = userGuid;
        }

        public void ConfirmDate()
        {
            IsDateConfirmed = true;
            IsEnrolled = true;
        }

        public void FinishOrder() =>
            Status = OrderStatus.Finished;

        public void Enroll() =>
            IsEnrolled = true;

        public void AddReview(string guid) =>
            Reviews.Add(guid);

        public void CancelEnrollment()
        {
            IsEnrolled = false;
            Status = OrderStatus.Canceled;
        }
    }
}
