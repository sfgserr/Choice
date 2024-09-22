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
            Distance = distance;
            Description = description;
            Status = status;
            CategoryId = categoryId;
            CreationDate = creationDate;

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

        public ClientId ClientCreatedId { get; }

        public bool ToKnowPrice { get; private set; }

        public bool ToKnowDeadline { get; private set; }

        public bool ToKnowEnrollmentDate { get; private set; }

        public int Distance { get; private set; }

        public string Description { get; private set; }

        public bool IsEnrolled { get; private set; } = false;

        public OrderStatus Status { get; private set; }

        public CategoryId CategoryId { get; private set; }

        public DateTime CreationDate { get; }

        public OrderResponse Response(
            Company company,
            double price,
            int deadline,
            DateTime? enrollmentDate,
            double prepayment)
        { 
            CheckRule(new CannotChangeInactiveRequestRule(Status));
            CheckRule(new CannotResponseIfUserEnrolledRule(IsEnrolled));
            
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
            CheckRule(new CannotChangeInactiveRequestRule(Status));
            CheckRule(new AtLeastOneRequirementMustBeTrueRule([toKnowDeadline, toKnowEnrollmentDate, toKnowPrice]));
            CheckRule(new DescriptionMustBeProvidedRule(description));
            
            ToKnowPrice = toKnowPrice;
            ToKnowDeadline = toKnowDeadline;
            ToKnowEnrollmentDate = toKnowEnrollmentDate;
            Distance = distance > 25 ? 25 : distance < 5 ? 5 : distance;
            Description = description;
            CategoryId = categoryId;

            _photoUris.Clear();
            _photoUris.AddRange(photoUris);
        }

        public void Enroll()
        {
            IsEnrolled = true;
        }
        
        public void SetStatus(OrderStatus status)
        {
            Status = status;
        }
    }
}
