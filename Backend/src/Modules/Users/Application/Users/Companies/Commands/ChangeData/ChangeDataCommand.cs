using BuildingBlocks.Application.Cqrs.Commands;
using Users.Domain.Categories;
using Users.Domain.Users;

namespace Users.Application.Users.Companies.Commands.ChangeData
{
    public class ChangeDataCommand : ICommand
    {
        public ChangeDataCommand(
            string name,
            string email,
            string phoneNumber,
            Address address,
            string description,
            List<CategoryId> categories,
            List<string> photoUris,
            bool isPrepaymentAvailable)
        {
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            Description = description;
            Categories = categories;
            PhotoUris = photoUris;
            IsPrepaymentAvailable = isPrepaymentAvailable;
        }
        
        public string Name { get; }
        
        public string Email { get; }
        
        public string PhoneNumber { get; }
        
        public Address Address { get; }
        
        public string Description { get; }
        
        public List<CategoryId> Categories { get; }
        
        public List<string> PhotoUris { get; }
        
        public bool IsPrepaymentAvailable { get; }
    }
}
