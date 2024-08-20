using BuildingBlocks.Domain;
using Users.Domain.OrderRequests;
using Users.Domain.OrderResponses.Events;
using Users.Domain.OrderResponses.Rules;
using Users.Domain.Users;
using Users.Domain.Users.Clients;
using Users.Domain.Users.Companies;

namespace Users.Domain.OrderResponses
{
    public class OrderResponse : Entity, IAggregateRoot
    {
        private readonly List<Review> _reviews = [];

        private OrderResponse(
            OrderResponseId id,
            ClientId clientId,
            CompanyId companyId,
            double price,
            int deadline,
            DateTime? enrollmentDate,
            bool toKnowPrice,
            bool toKnowDeadline,
            bool toKnowEnrollmentDate,
            double prepayment,
            OrderStatus status,
            bool isPaid)
        {
            CheckRule(new ResponseCannotMarkedAsNotpaidIfPrepaymentIsZeroRule(isPaid, prepayment));
            CheckRule(new PrepaymentShouldBeInRangeBetweenTenAndTwentyFivePercentOfPriceRule(prepayment, price));
            CheckRule(new RequiredInformationMustBeProvidedRule(
                price, 
                deadline, 
                enrollmentDate, 
                toKnowPrice,
                toKnowDeadline,
                toKnowEnrollmentDate));

            Id = id;
            ClientId = clientId;
            CompanyId = companyId;
            Price = price;
            Deadline = deadline;
            EnrollmentDate = enrollmentDate;
            Prepayment = prepayment;
            Status = status;
            IsPaid = isPaid;

            AddDomainEvent(new OrderResponseCreatedDomainEvent(Id));
        }

        internal static OrderResponse Create(
            OrderRequest request,
            Company company,
            double price,
            int deadline,
            DateTime? enrollmentDate,
            double prepayment)
        {
            return new OrderResponse(
                new(request.Id.Value),
                request.ClientCreatedId,
                company.Id,
                price,
                deadline,
                enrollmentDate,
                request.ToKnowPrice,
                request.ToKnowDeadline,
                request.ToKnowEnrollmentDate,
                prepayment,
                OrderStatus.Active,
                !company.IsPrepaymentAvailable);
        }

        public OrderResponseId Id { get; }

        public ClientId ClientId { get; }
        
        public CompanyId CompanyId { get; }

        public double Price { get; }

        public int Deadline { get; }

        public DateTime? EnrollmentDate { get; private set; }

        public double Prepayment { get; }

        public OrderStatus Status { get; private set; } = OrderStatus.Active;

        public IReadOnlyCollection<Review> Reviews => _reviews.AsReadOnly();

        public bool IsEnrolled { get; private set; } = false;

        public bool IsPaid { get; private set; }

        public UserId? UserChangedEnrollmentDate { get; private set; }

        public bool IsEnrollmentDateConfirmed { get; private set; } = true;

        public bool IsActive { get; private set; } = true;

        public void EnrollWithPrepayment(ClientId enrollingClientId)
        {
            CheckRule(new CannotEnrollMoreThanOnceRule(IsEnrolled));
            CheckRule(new CannotMakeOperationsWithNotActiveOrderRule(Status, IsActive));
            CheckRule(new OnlyClientCreatedOrCompanyResponsedCanMakeOperationsRule(enrollingClientId.Value, CompanyId, ClientId));
            CheckRule(new CannotEnrollIfEnrollmentDateIsNotConfirmedRule(IsEnrollmentDateConfirmed));

            IsEnrolled = true;

            AddDomainEvent(new EnrolledWithPrepaymentDomainEvent(Id));
        }

        public void Enroll(ClientId enrollingClientId)
        {
            CheckRule(new CannotEnrollMoreThanOnceRule(IsEnrolled));
            CheckRule(new CannotEnrollIfOrderIsNotPaidRule(IsPaid));
            CheckRule(new CannotMakeOperationsWithNotActiveOrderRule(Status, IsActive));
            CheckRule(new OnlyClientCreatedOrCompanyResponsedCanMakeOperationsRule(enrollingClientId.Value, CompanyId, ClientId));
            CheckRule(new CannotEnrollIfEnrollmentDateIsNotConfirmedRule(IsEnrollmentDateConfirmed));

            IsEnrolled = true;

            AddDomainEvent(new EnrolledDomainEvent(Id));
        }

        public void Finish(UserId cancellingUserId)
        {
            CheckRule(new OnlyClientCreatedOrCompanyResponsedCanMakeOperationsRule(cancellingUserId.Value, CompanyId, ClientId));
            CheckRule(new CannotMakeOperationsWithNotActiveOrderRule(Status, IsActive));
            CheckRule(new CannotFinishOrCancelIfUserIsNotEnrolledRule(IsEnrolled, IsPaid));

            Status = OrderStatus.Finished;

            AddDomainEvent(new OrderStatusChangedDomainEvent(Id, Status));
        }

        public void Cancel(UserId cancellingUserId)
        {
            CheckRule(new OnlyClientCreatedOrCompanyResponsedCanMakeOperationsRule(cancellingUserId.Value, CompanyId, ClientId));
            CheckRule(new CannotMakeOperationsWithNotActiveOrderRule(Status, IsActive));
            CheckRule(new CannotFinishOrCancelIfUserIsNotEnrolledRule(IsEnrolled, IsPaid));

            Status = OrderStatus.Cancelled;

            AddDomainEvent(new OrderStatusChangedDomainEvent(Id, Status));
        }

        public void ChangeEnrollmentDateByClient(ClientId changingClientId, DateTime newEnrollmentDate)
        {
            CheckRule(new OnlyClientCreatedOrCompanyResponsedCanMakeOperationsRule(changingClientId.Value, CompanyId, ClientId));
            CheckRule(new CannotMakeOperationsWithNotActiveOrderRule(Status, IsActive));
            CheckRule(new CannotChangeEnrollmentDateIfItIsNotProvidedRule(EnrollmentDate));
            CheckRule(new CannotChangeEnrollmentDateIfItIsNotProvidedRule(newEnrollmentDate));
            CheckRule(new CannotChangeEnrollmentDateIfUserEnrolledRule(IsEnrolled));
            CheckRule(new CannotChangeEnrollmentDateTwiceInARowRule(
                UserChangedEnrollmentDate,
                new UserId(changingClientId.Value)));

            EnrollmentDate = newEnrollmentDate;
            IsEnrollmentDateConfirmed = false;
            UserChangedEnrollmentDate = new UserId(changingClientId.Value);

            AddDomainEvent(new EnrollmentDateChangedDomainEvent(
                Id, 
                EnrollmentDate, 
                UserChangedEnrollmentDate, 
                IsEnrollmentDateConfirmed));
        }

        public void ChangeEnrollmentDateByCompany(CompanyId changingCompanyId, DateTime newEnrollmentDate)
        {
            CheckRule(new OnlyClientCreatedOrCompanyResponsedCanMakeOperationsRule(changingCompanyId.Value, CompanyId, ClientId));
            CheckRule(new CannotMakeOperationsWithNotActiveOrderRule(Status, IsActive));
            CheckRule(new CannotChangeEnrollmentDateIfItIsNotProvidedRule(EnrollmentDate));
            CheckRule(new CannotChangeEnrollmentDateIfItIsNotProvidedRule(newEnrollmentDate));
            CheckRule(new CannotChangeEnrollmentDateIfUserEnrolledRule(IsEnrolled));
            CheckRule(new CannotChangeEnrollmentDateTwiceInARowRule(
                UserChangedEnrollmentDate, 
                new UserId(changingCompanyId.Value)));

            EnrollmentDate = newEnrollmentDate;
            IsEnrollmentDateConfirmed = true;
            UserChangedEnrollmentDate = new UserId(changingCompanyId.Value);

            AddDomainEvent(new EnrollmentDateChangedDomainEvent(
                Id,
                EnrollmentDate,
                UserChangedEnrollmentDate,
                IsEnrollmentDateConfirmed));
        }

        public void ConfirmEnrollmentDate(CompanyId confirmingCompanyId)
        {
            CheckRule(new OnlyClientCreatedOrCompanyResponsedCanMakeOperationsRule(confirmingCompanyId.Value, CompanyId, ClientId));
            CheckRule(new CannotMakeOperationsWithNotActiveOrderRule(Status, IsActive));
            CheckRule(new CannotConfirmEnrollmentDateMoreThanOnceRule(IsEnrollmentDateConfirmed));

            IsEnrollmentDateConfirmed = true;
            
            AddDomainEvent(new EnrollmentDateConfirmedDomainEvent(Id));
        }

        public void MarkAsPaid()
        {
            IsPaid = true;

            AddDomainEvent(new OrderPaidDomainEvent(Id));
        }

        public void AddReview(
            UserId reviewingUserId, 
            UserId toUserId, 
            int grade, 
            string text)
        {
            CheckRule(new CannotReviewMoreThanOnceRule(_reviews, reviewingUserId));
            CheckRule(new CannotReviewYourselftRule(reviewingUserId, toUserId));
            CheckRule(new CannotReviewWhileOrderActive(Status));

            _reviews.Add(Review.Create(
                Id,
                reviewingUserId,
                toUserId,
                text,
                grade));
        }
    }
}
