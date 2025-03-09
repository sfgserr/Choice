using BuildingBlocks.Domain;
using Users.Domain.OrderRequests.OrderResponses.Events;
using Users.Domain.OrderRequests.OrderResponses.Rules;
using Users.Domain.Users;
using Users.Domain.Users.Clients;
using Users.Domain.Users.Companies;

namespace Users.Domain.OrderRequests.OrderResponses
{
    public class OrderResponse : Entity, IAggregateRoot
    {
        private OrderRequestId _requestId;

        private ClientId _clientId;
        
        private CompanyId _companyId;

        private double _price;

        private int _deadline;
        
        private DateTime? _enrollmentDate;

        private double _prepayment;
        
        private OrderStatus _status = OrderStatus.Active;

        private bool _isEnrolled;

        private UserId? _userChangedEnrollmentDate;
        
        private bool _isEnrollmentDateConfirmed = true;

        private bool _isActive = true;
        
        private readonly List<Review> _reviews = [];

        private OrderResponse()
        {

        }

        private OrderResponse(
            OrderResponseId id,
            OrderRequestId requestId,
            ClientId clientId,
            CompanyId companyId,
            double price,
            int deadline,
            DateTime? enrollmentDate,
            bool toKnowPrice,
            bool toKnowDeadline,
            bool toKnowEnrollmentDate,
            double prepayment,
            OrderStatus status)
        {
            CheckRule(new PrepaymentShouldBeInRangeBetweenTenAndTwentyFivePercentOfPriceRule(prepayment, price));
            CheckRule(new RequiredInformationMustBeProvidedRule(
                price, 
                deadline, 
                enrollmentDate, 
                toKnowPrice,
                toKnowDeadline,
                toKnowEnrollmentDate));

            Id = id;
            
            _requestId = requestId;
            _clientId = clientId;
            _companyId = companyId;
            _price = price;
            _deadline = deadline;
            _enrollmentDate = enrollmentDate;
            _prepayment = prepayment;
            _status = status;

            AddDomainEvent(new OrderResponseCreatedDomainEvent(
                Id,
                _companyId,
                _clientId));
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
                new(Guid.NewGuid()),
                request.Id,
                request.ClientCreatedId,
                company.Id,
                price,
                deadline,
                enrollmentDate,
                request.ToKnowPrice,
                request.ToKnowDeadline,
                request.ToKnowEnrollmentDate,
                prepayment,
                OrderStatus.Active);
        }

        public OrderResponseId Id { get; }

        public void Enroll(ClientId enrollingClientId)
        {
            CheckRule(new CannotMakeOperationsWithNotActiveOrderRule(_status, _isActive));
            CheckRule(new CannotEnrollMoreThanOnceRule(_isEnrolled));
            CheckRule(new OnlyClientCreatedOrCompanyResponsedCanMakeOperationsRule(enrollingClientId.Value, _companyId, _clientId));
            CheckRule(new CannotEnrollIfEnrollmentDateIsNotConfirmedRule(_isEnrollmentDateConfirmed));

            _isEnrolled = true;

            AddDomainEvent(new EnrolledDomainEvent(Id, _requestId, _companyId));
        }

        public void Finish(UserId cancellingUserId)
        {
            CheckRule(new OnlyClientCreatedOrCompanyResponsedCanMakeOperationsRule(cancellingUserId.Value, _companyId, _clientId));
            CheckRule(new CannotMakeOperationsWithNotActiveOrderRule(_status, _isActive));
            CheckRule(new CannotFinishOrCancelIfUserIsNotEnrolledRule(_isEnrolled));

            _status = OrderStatus.Finished;

            AddDomainEvent(new OrderStatusChangedDomainEvent(
                _requestId, 
                Id, 
                _status.Value, 
                !cancellingUserId.Equals(new UserId(_clientId.Value)) ? new(_clientId.Value) : new(_companyId.Value)));
        }

        public void Cancel(UserId cancellingUserId)
        {
            CheckRule(new OnlyClientCreatedOrCompanyResponsedCanMakeOperationsRule(cancellingUserId.Value, _companyId, _clientId));
            CheckRule(new CannotMakeOperationsWithNotActiveOrderRule(_status, _isActive));
            CheckRule(new CannotFinishOrCancelIfUserIsNotEnrolledRule(_isEnrolled));

            _status = OrderStatus.Cancelled;

            AddDomainEvent(new OrderStatusChangedDomainEvent(
                _requestId, 
                Id, 
                _status.Value, 
                !cancellingUserId.Equals(new UserId(_clientId.Value)) ? new(_clientId.Value) : new(_companyId.Value)));
        }

        public void ChangeEnrollmentDateByClient(ClientId changingClientId, DateTime newEnrollmentDate)
        {
            CheckRule(new OnlyClientCreatedOrCompanyResponsedCanMakeOperationsRule(changingClientId.Value, _companyId, _clientId));
            CheckRule(new CannotMakeOperationsWithNotActiveOrderRule(_status, _isActive));
            CheckRule(new CannotChangeEnrollmentDateIfItIsNotProvidedRule(_enrollmentDate));
            CheckRule(new CannotChangeEnrollmentDateIfItIsNotProvidedRule(newEnrollmentDate));
            CheckRule(new CannotChangeEnrollmentDateIfUserEnrolledRule(_isEnrolled));
            CheckRule(new CannotChangeEnrollmentDateTwiceInARowRule(
                _userChangedEnrollmentDate,
                new UserId(changingClientId.Value)));

            var previousEnrollmentDate = _enrollmentDate!.Value;

            _enrollmentDate = newEnrollmentDate;
            _isEnrollmentDateConfirmed = false;
            _userChangedEnrollmentDate = new UserId(changingClientId.Value);

            AddDomainEvent(new EnrollmentDateChangedDomainEvent(
                Id, 
                previousEnrollmentDate,
                new UserId(_companyId.Value)));
        }

        public void ChangeEnrollmentDateByCompany(CompanyId changingCompanyId, DateTime newEnrollmentDate)
        {
            CheckRule(new OnlyClientCreatedOrCompanyResponsedCanMakeOperationsRule(changingCompanyId.Value, _companyId, _clientId));
            CheckRule(new CannotMakeOperationsWithNotActiveOrderRule(_status, _isActive));
            CheckRule(new CannotChangeEnrollmentDateIfItIsNotProvidedRule(_enrollmentDate));
            CheckRule(new CannotChangeEnrollmentDateIfItIsNotProvidedRule(newEnrollmentDate));
            CheckRule(new CannotChangeEnrollmentDateIfUserEnrolledRule(_isEnrolled));
            CheckRule(new CannotChangeEnrollmentDateTwiceInARowRule(
                _userChangedEnrollmentDate, 
                new UserId(changingCompanyId.Value)));

            var previousEnrollmentDate = _enrollmentDate!.Value;

            _enrollmentDate = newEnrollmentDate;
            _isEnrollmentDateConfirmed = true;
            _userChangedEnrollmentDate = new UserId(changingCompanyId.Value);

            AddDomainEvent(new EnrollmentDateChangedDomainEvent(
                Id,
                previousEnrollmentDate,
                new UserId(_clientId.Value)));
        }

        public void ConfirmEnrollmentDate(CompanyId confirmingCompanyId)
        {
            CheckRule(new OnlyClientCreatedOrCompanyResponsedCanMakeOperationsRule(confirmingCompanyId.Value, _companyId, _clientId));
            CheckRule(new CannotMakeOperationsWithNotActiveOrderRule(_status, _isActive));
            CheckRule(new CannotConfirmEnrollmentDateMoreThanOnceRule(_isEnrollmentDateConfirmed));

            _isEnrollmentDateConfirmed = true;
            _isEnrolled = true;
            
            AddDomainEvent(new EnrollmentDateConfirmedDomainEvent(Id, _clientId));
        }

        public void AddReview(
            UserId reviewingUserId, 
            UserId toUserId, 
            int grade, 
            string text)
        {
            CheckRule(new CannotReviewMoreThanOnceRule(_reviews, reviewingUserId));
            CheckRule(new CannotReviewYourselfRule(reviewingUserId, toUserId));
            CheckRule(new CannotReviewWhileOrderActive(_status));

            var review = Review.Create(
                Id,
                reviewingUserId,
                toUserId,
                text,
                grade);
            
            _reviews.Add(review);
            
            AddDomainEvent(new ReviewCreatedDomainEvent(grade, toUserId));
        }
    }
}
