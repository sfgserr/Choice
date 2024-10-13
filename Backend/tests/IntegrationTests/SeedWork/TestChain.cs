
namespace IntegrationTests.SeedWork
{
    public class TestChain
    {
        private readonly Func<Task<bool>> _test;

        private TestChain? _next;

        public TestChain(Func<Task<bool>> test)
        {
            _test = test;
        }

        public void SetNext(TestChain next)
        {
            _next = next;
        }

        public bool Execute()
        {
            var result = _test().GetAwaiter().GetResult();

            if (result)
            {
                return _next is null || _next.Execute();
            }

            return false;
        }
    }
}