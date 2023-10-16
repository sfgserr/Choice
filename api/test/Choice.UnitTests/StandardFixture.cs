using Choice.Infrastructure;
using Choice.Infrastructure.Repositories.Fakes;

namespace Choice.UnitTests
{
    public sealed class StandardFixture
    {
        public StandardFixture() 
        {
            Context = new ChoiceContextFake();

            CategoryRepositoryFake = new CategoryRepositoryFake(Context);

            UnitOfWorkFake = new UnitOfWorkFake();
        }

        public CategoryRepositoryFake CategoryRepositoryFake { get; }
        public ChoiceContextFake Context { get; }
        public UnitOfWorkFake UnitOfWorkFake { get; }
    }
}
