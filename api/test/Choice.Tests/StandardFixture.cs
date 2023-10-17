using Choice.Infrastructure;
using Choice.Infrastructure.Repositories.Fakes;

namespace Choice.UnitTests
{
    public sealed class StandardFixture
    {
        public StandardFixture()
        {
            Context = new ChoiceContextFake();
            UnitOfWorkFake = new UnitOfWorkFake();

            CategoryRepositoryFake = new CategoryRepositoryFake(Context);
            ChatMessageRepositoryFake = new ChatMessageRepositoryFake(Context);
            CompanyRepositoryFake = new CompanyRepositoryFake(Context);
            OrderMessageRepositoryFake = new OrderMessageRepositoryFake(Context);
            ClientRepositoryFake = new ClientRepositoryFake(Context);
            OrderRepositoryFake = new OrderRepositoryFake(Context);
        }

        public CategoryRepositoryFake CategoryRepositoryFake { get; }
        public ChatMessageRepositoryFake ChatMessageRepositoryFake { get; }
        public CompanyRepositoryFake CompanyRepositoryFake { get; }
        public ClientRepositoryFake ClientRepositoryFake { get; }
        public OrderMessageRepositoryFake OrderMessageRepositoryFake { get; }
        public OrderRepositoryFake OrderRepositoryFake { get; }
        public ChoiceContextFake Context { get; }
        public UnitOfWorkFake UnitOfWorkFake { get; }
    }
}
