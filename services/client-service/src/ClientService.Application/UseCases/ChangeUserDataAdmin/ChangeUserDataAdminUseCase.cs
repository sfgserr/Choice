using Choice.Application.Services;
using Choice.ClientService.Domain.ClientAggregate;

namespace Choice.ClientService.Application.UseCases.ChangeUserDataAdmin
{
    public sealed class ChangeUserDataAdminUseCase : IChangeUserDataAdminUseCase
    {
        private readonly IClientRepository _repository;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAddressService _addressService;

        private IOutputPort _outputPort;

        public ChangeUserDataAdminUseCase(IClientRepository repository, IUnitOfWork unitOfWork, IUserService userService, 
            IAddressService addressService)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _userService = userService;
            _addressService = addressService;

            _outputPort = new ChangeUserDataAdminPresenter();
        }

        public async Task Execute(string id, string name, string surname, string email, string phoneNumber, string city,
            string street)
        {
            Client client = await _repository.Get(id);

            if (client is null)
            {
                _outputPort.NotFound();
                return;
            }

            await ChangeData(client, name, surname, email, phoneNumber, city, street);

            _outputPort.Ok(client);
        }

        public async Task ChangeData(Client client, string name, string surname, string email, string phoneNumber, 
            string city, string street)
        {
            string coords = await _addressService.Geocode(new(street,city));

            client.ChangeData(name, surname, email, phoneNumber, city, street, coords);

            _repository.Update(client);

            await _unitOfWork.SaveChanges();
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}
