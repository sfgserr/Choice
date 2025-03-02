using BuildingBlocks.Domain;
using Users.Domain.Categories;
using Users.Domain.OrderRequests.OrderResponses;
using Users.Domain.OrderRequests.Rules;
using Users.Domain.Users.Clients;
using Users.Domain.Users.Companies;

namespace Users.Domain.OrderRequests
{
    public class OrderRequest : Entity, IAggregateRoot
    {
        private int _distance;
        
        private string _description;

        private bool _isEnrolled;

        private OrderStatus _status;

        private CategoryId _categoryId;

        private DateTime _creationDate;
        
        private readonly List<string> _photoUris = [];
        
        private OrderRequest()
        {

        }

        private OrderRequest(
            OrderRequestId id,
            ClientId clientCreatedId,
            bool toKnowPrice,
            bool toKnowDeadline,
            bool toKnowEnrollmentDate,
            int distance,
            List<string> photoUris,
            string description,
            OrderStatus status,
            CategoryId categoryId,
            DateTime creationDate)
        {
            CheckRule(new AtLeastOneRequirementMustBeTrueRule([toKnowPrice, toKnowDeadline, toKnowEnrollmentDate]));
            CheckRule(new DescriptionMustBeProvidedRule(description));

            Id = id;
            ClientCreatedId = clientCreatedId;
            ToKnowPrice = toKnowPrice;
            ToKnowDeadline = toKnowDeadline;
            ToKnowEnrollmentDate = toKnowEnrollmentDate;
            
            _distance = distance;
            _description = description;
            _status = status;
            _categoryId = categoryId;
            _creationDate = creationDate;
            _photoUris = photoUris;
        }

        internal static OrderRequest Create(
            ClientId clientCreatedId,
            bool toKnowPrice,
            bool toKnowDeadline,
            bool toKnowEnrollmentDate,
            int distance,
            List<string> photoUris,
            string description,
            CategoryId categoryId)
        {
            return new OrderRequest(
                new(Guid.NewGuid()),
                clientCreatedId,
                toKnowPrice,
                toKnowDeadline,
                toKnowEnrollmentDate,
                distance > 25 ? 25 : distance < 5 ? 5 : distance,
                photoUris,
                description,
                OrderStatus.Active,
                categoryId,
                DateTime.UtcNow);
        }

        public OrderRequestId Id { get; }

        internal ClientId ClientCreatedId { get; }

        internal bool ToKnowPrice { get; private set; }

        internal bool ToKnowDeadline { get; private set; }

        internal bool ToKnowEnrollmentDate { get; private set; }

        public OrderResponse Response(
            Company company,
            double price,
            int deadline,
            DateTime? enrollmentDate,
            double prepayment,
            IOrderResponsesCounter counter)
        { 
            CheckRule(new CannotChangeInactiveRequestRule(_status));
            CheckRule(new CannotResponseTwiceRule(counter, Id, company.Id));
            CheckRule(new CannotResponseIfUserEnrolledRule(_isEnrolled));
            
            return OrderResponse.Create(
                this, 
                company, 
                price, 
                deadline, 
                enrollmentDate, 
                prepayment);
        }

        public void Change(
            bool toKnowPrice,
            bool toKnowDeadline,
            bool toKnowEnrollmentDate,
            int distance,
            List<string> photoUris,
            string description,
            CategoryId categoryId,
            ClientId changingClientId)
        {
            CheckRule(new OnlyCreatorCanChangeRequestRule(changingClientId, ClientCreatedId));
            CheckRule(new CannotChangeInactiveRequestRule(_status));
            CheckRule(new AtLeastOneRequirementMustBeTrueRule([toKnowDeadline, toKnowEnrollmentDate, toKnowPrice]));
            CheckRule(new DescriptionMustBeProvidedRule(description));
            
            ToKnowPrice = toKnowPrice;
            ToKnowDeadline = toKnowDeadline;
            ToKnowEnrollmentDate = toKnowEnrollmentDate;
            _distance = distance > 25 ? 25 : distance < 5 ? 5 : distance;
            _description = description;
            _categoryId = categoryId;

            _photoUris.Clear();
            _photoUris.AddRange(photoUris);
        }

        public void Enroll()
        {
            _isEnrolled = true;
        }
        
        public void SetStatus(OrderStatus status)
        {
            _status = status;
        }
    }
}
