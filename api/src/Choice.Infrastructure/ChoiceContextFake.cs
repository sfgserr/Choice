using Choice.Domain.Models;
using System.Collections.ObjectModel;

namespace Choice.Infrastructure
{
    public class ChoiceContextFake
    {
        public ChoiceContextFake()
        {
            Setup();
        }

        public Collection<Category> Categories { get; } = new Collection<Category>();
        public Collection<ChatMessage> ChatMessages { get; } = new Collection<ChatMessage>();
        public Collection<Client> Clients { get; } = new Collection<Client>();
        public Collection<Company> Companies { get; } = new Collection<Company>();
        public Collection<Order> Orders { get; } = new Collection<Order>();
        public Collection<OrderMessage> OrderMessages { get; } = new Collection<OrderMessage>();
        public Collection<Review> Reviews { get; } = new Collection<Review>();
        public Collection<Room> Rooms { get; } = new Collection<Room>();
        public Collection<SocialMedia> SocialMedias { get; } = new Collection<SocialMedia>();

        private void Setup()
        {
            Category category = new Category()
            {
                Id = 1,
                Title = "Category",
                IconUri = "uri"
            };

            Client client = new Client()
            {
                Id = 1,
                Email = "client@mail.ru",
                Name = "Name",
                Password = "Password",
                PhotoUri = "uri",
                ShowReviews = true,
                Surname = "Surname"
            };

            Room room = new Room()
            {
                Id = 1,
                Name = "Room",
            };

            ChatMessage chatMessage = new ChatMessage()
            {
                Id = 1,
                Sender = client,
                Status = MessageStatus.Unread,
                Text = "Hi",
                Room = room,
                UploadDate = DateTime.Now
            };

            Company company = new Company()
            {
                Id = 1,
                Address = "Sydney, Australia",
                SiteUri = "uri",
                ShowOnMap = true,
                Email = "some@email.com",
                Password = "Password",
                Title = "Title",
                PrepaymentAvailability = PrepaymentAvailability.With,
                PhoneNumber = "+123123123",
                PhotoUris = new List<string>() { "uri" }
            };

            SocialMedia socialMedia = new SocialMedia()
            {
                Id = 1,
                Title = "Telegram",
                Uri = "uri",
            };

            company.SocialMedias.Add(socialMedia);
            company.Categories.Add(category);
            category.Companies.Add(company);

            Companies.Add(company);
            SocialMedias.Add(socialMedia);
            Categories.Add(category);
            Clients.Add(client);
            ChatMessages.Add(chatMessage);
            Rooms.Add(room);
        }
    }
}
