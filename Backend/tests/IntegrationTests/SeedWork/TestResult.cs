namespace IntegrationTests.SeedWork
{
    public class TestResult
    {
        public TestResult(bool isSuccessful, object?[] result)
        {
            IsSuccessful = isSuccessful;
            Result = result;
        }
        
        public bool IsSuccessful { get; }
        
        public object?[] Result { get; }
    }
}
