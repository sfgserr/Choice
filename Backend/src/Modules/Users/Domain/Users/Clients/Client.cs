using BuildingBlocks.Domain;
using Users.Domain.Categories;
using Users.Domain.OrderRequests;

namespace Users.Domain.Users.Clients
{
    public class Client : Entity, IAggregateRoot
    {
        private User _user;

        private UserId _userId;
        
        private Client()
        {

        }

        private Client(User user)
        {
            Id = new(user.Id.Value);
            
            _userId = user.Id;
            _user = user;
        }

        public static Client Create(
            string name,
            string email,
            string phoneNumber,
            string password,
            string deviceName,
            string deviceToken,
            Address address,
            IUsersCounter counter)
        {
            User user = User.Create(
                name,
                email,
                password,
                phoneNumber,
                deviceName,
                deviceToken,
                address,
                UserRole.Client,
                counter);

            return new Client(user);
        }

        public OrderRequest CreateRequest(
            bool toKnowPrice,
            bool toKnowDeadline,
            bool toKnowEnrollmentDate,
            int distance,
            List<string> photoUris,
            string description,
            CategoryId categoryId)
        {
            return OrderRequest.Create(
                Id,
                toKnowPrice,
                toKnowDeadline,
                toKnowEnrollmentDate,
                distance,
                photoUris,
                description,
                categoryId);
        }

        public void ChangeIconUri(string iconUri)
        {
            _user.ChangeIconUri(iconUri);
        }

        public void ChangeData(
            string name,
            string email,
            string phoneNumber,
            Address address,
            IUsersCounter counter)
        {
            _user.ChangeData(
                name,
                email,
                phoneNumber,
                address,
                counter);
        }

        public ClientId Id { get; }
    }
}
