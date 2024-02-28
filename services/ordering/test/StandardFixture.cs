using Choice.Ordering.Infrastructure.Authentication;
using Choice.Ordering.Infrastructure.Data;
using Choice.Ordering.Infrastructure.Data.Repositories;

namespace Choice.Ordering.UnitTests
{
    public sealed class StandardFixture
    {
        public StandardFixture()
        {
            Context = new OrderingContextFake();
            OrderRepository = new OrderRepositoryFake(Context);
            UnitOfWork = new UnitOfWorkFake();
            UserService = new TestUserService();
        }

        public OrderRepositoryFake OrderRepository { get; }
        public OrderingContextFake Context { get; }
        public UnitOfWorkFake UnitOfWork { get; }
        public TestUserService UserService { get; }
    }
}
