
namespace IntegrationTests.SeedWork
{
    public class TestChain
    {
        private readonly Func<object?, Task<TestResult>> _test;

        private TestChain? _next;

        public TestChain(Func<object?, Task<TestResult>> test)
        {
            _test = test;
        }

        public void SetNext(TestChain next)
        {
            _next = next;
        }

        public bool Execute(object? args)
        {
            var result = _test(args).GetAwaiter().GetResult();

            if (result.IsSuccessful)
            {
                return _next is null || _next.Execute(result.Result);
            }

            return false;
        }
    }
}