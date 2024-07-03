
namespace ClientService.Application.UseCases.DeleteClientAdmin
{
    public sealed class DeleteClientAdminPresenter : IOutputPort
    {
        public string? Id { get; set; }

        public void Ok(string id)
        {
            Id = id;
        }
    }
}
