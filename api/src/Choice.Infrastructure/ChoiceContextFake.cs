using Choice.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Choice.Infrastructure
{
    public class ChoiceContextFake
    {
        public ChoiceContextFake()
        {

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
    }
}
