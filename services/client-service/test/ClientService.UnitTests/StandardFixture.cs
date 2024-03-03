using Choice.ClientService.Infrastructure.Authentication;
using Choice.ClientService.Infrastructure.Data;
using Choice.ClientService.Infrastructure.Data.Repositories;

namespace Choice.ClientService.UnitTests
{
    public class StandardFixture
    {
        public StandardFixture()
        {
            Context = new ClientContextFake();
            Repository = new ClientRepositoryFake(Context);
            UnitOfWork = new UnitOfWorkFake();
            UserService = new TestUserService();
        }

        public ClientContextFake Context { get; }
        public ClientRepositoryFake Repository { get; }
        public UnitOfWorkFake UnitOfWork { get; }
        public TestUserService UserService { get; }
    }
}
