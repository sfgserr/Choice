using Choice.ClientService.Infrastructure.Data;
using Choice.ClientService.Infrastructure.Data.Repositories;
using Choice.Infrastructure.Data;
using Infrastructure.Authentication;

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
