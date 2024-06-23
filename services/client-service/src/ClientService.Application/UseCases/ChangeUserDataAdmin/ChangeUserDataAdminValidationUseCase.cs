using Choice.Application.Services;
using Choice.ClientService.Domain.ClientAggregate;

namespace Choice.ClientService.Application.UseCases.ChangeUserDataAdmin
{
    public sealed class ChangeUserDataAdminValidationUseCase : IChangeUserDataAdminUseCase
    {
        private readonly IChangeUserDataAdminUseCase _useCase;
        private readonly IUserService _userService;
        private readonly IClientRepository _repository;
        private readonly Notification _notification;

        private IOutputPort _outputPort;

        public ChangeUserDataAdminValidationUseCase(IChangeUserDataAdminUseCase useCase, Notification notification, IClientRepository repository, IUserService userService)
        {
            _useCase = useCase;
            _notification = notification;
            _userService = userService;

            _outputPort = new ChangeUserDataAdminPresenter();
            _repository = repository;
        }

        public async Task Execute(string id, string name, string surname, string email, string phoneNumber, string city, 
            string street)
        {
            if (name == string.Empty || surname == string.Empty || email == string.Empty || phoneNumber == string.Empty 
                || city == string.Empty || street == string.Empty)
            {
                _notification.Add(nameof(name), "All fields should not be empty");
            }

            IList<Client> clients = await _repository.GetAll();

            if (clients.Any(c => c.Email == email && c.Guid != id))
            {
                _notification.Add(nameof(email), "Email already in use");
            }

            if (clients.Any(c => c.PhoneNumber == phoneNumber && c.Guid != id))
            {
                _notification.Add(nameof(phoneNumber), "Phone number already in use");
            }

            if (_notification.IsInvalid)
            {
                _outputPort.Invalid();
                return;
            }

            await _useCase.Execute(id, name, surname, email, phoneNumber, city, street);
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
            _useCase.SetOutputPort(outputPort);
        }
    }
}
