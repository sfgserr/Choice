using BuildingBlocks.Domain;
using Users.Domain.Categories;
using Users.Domain.OrderRequests;

namespace Users.Domain.Users.Clients
{
    public class Client : Entity, IAggregateRoot
    {
        private Client()
        {

        }

        private Client(User user)
        {
            Id = new(user.Id.Value);
            User = user;
        }

        public static Client Create(
            string name,
            string email,
            string phoneNumber,
            string hashedPassword,
            Address address,
            IUsersCounter counter)
        {
            User user = User.Create(
                name,
                email,
                phoneNumber,
                hashedPassword,
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
            User.ChangeIconUri(iconUri);
        }

        public void ChangeData(
            string name,
            string email,
            string phoneNumber,
            Address address,
            IUsersCounter counter)
        {
            User.ChangeData(
                name,
                email,
                phoneNumber,
                address,
                counter);
        }

        public ClientId Id { get; }

        public User User { get; }
    }
}
