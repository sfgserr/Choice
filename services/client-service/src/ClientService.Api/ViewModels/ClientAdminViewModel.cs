﻿using Choice.ClientService.Domain.ClientAggregate;

namespace Choice.ClientService.Api.ViewModels
{
    public class ClientAdminViewModel
    {
        public ClientAdminViewModel(Client client)
        {
            Id = client.Id;
            Guid = client.Guid;
            Name = client.Name;
            Surname = client.Surname;
            Email = client.Email;
            AverageGrade = client.AverageGrade;
            IconUri = client.IconUri;
            PhoneNumber = client.PhoneNumber;
            Street = client.Address.Street;
            City = client.Address.City;
            Coords = client.Coordinates;
        }

        public int Id { get; }
        public string Guid { get; }
        public string Name { get; }
        public string Surname { get; }
        public string Email { get; }
        public double AverageGrade { get; }
        public string IconUri { get; }
        public string PhoneNumber { get; }
        public string Street { get; }
        public string City { get; }
        public string Coords { get; }
    }
}
