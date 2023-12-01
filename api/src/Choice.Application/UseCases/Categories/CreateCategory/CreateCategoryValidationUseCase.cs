
namespace Choice.Application.UseCases.Categories.CreateCategory
{
    public class CreateCategoryValidationUseCase : ICreateCategoryUseCase
    {
        private readonly ICreateCategoryUseCase _useCase;

        private IOutputPort _outputPort;

        public CreateCategoryValidationUseCase(ICreateCategoryUseCase useCase)
        {
            _useCase = useCase;

            _outputPort = new CreateCategoryPresenter();
        }

        public async Task Execute(string title, string iconUri)
        {
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(iconUri))
            {
                _outputPort.Invalid();
                return;
            }

            await _useCase.Execute(title, iconUri);
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
            _useCase.SetOutputPort(outputPort);
        }
    }
}
