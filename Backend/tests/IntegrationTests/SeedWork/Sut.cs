using IntegrationTests.Services.Auth;
using IntegrationTests.Services.Database;
using Xunit.Abstractions;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace IntegrationTests.SeedWork
{
    public class Sut : TestBed<Fixture>
    {
        private readonly IHttpClientFactory _factory;
        private readonly AuthService _authService;
        private readonly DbService _dbService;

        protected Sut(ITestOutputHelper outputHelper, Fixture testBed) : base(outputHelper, testBed)
        {
            _factory = testBed.GetService<IHttpClientFactory>(outputHelper)!;
            _dbService = testBed.GetService<DbService>(outputHelper)!;
            _authService = testBed.GetService<AuthService>(outputHelper)!;        
        }

        protected async Task<TestResult> ExecuteAuthorizedTest(
            Func<IHttpClientFactory,string,Task<TestResult>> testExecution,
            int delayAfterTestExecution = 0,
            bool reset = false,
            TokenType tokenType = TokenType.Client)
        {
            try
            {
                var token = await _authService.GetToken(tokenType);
            
                if (token is null) 
                {
                    _dbService.ClearDatabase();
                    return new TestResult(false, []);
                }
                
                var result = await testExecution(_factory, token);
                
                if (reset || !result.IsSuccessful) Reset();

                await Task.Delay(delayAfterTestExecution);

                return result;
            }
            catch
            {
                Reset();
                return new TestResult(false, []);
            }
        }
        
        protected async Task<TestResult> ExecuteTest(
            Func<IHttpClientFactory,Task<TestResult>> testExecution,
            int delayAfterExecution = 0,
            bool reset = false)
        {
            try
            {
                var result = await testExecution(_factory);
                
                if (reset || !result.IsSuccessful) Reset();

                await Task.Delay(delayAfterExecution);
                
                return result;
            }
            catch
            {
                Reset();
                throw;
            }
        }

        private void Reset()
        {
            _authService.ClearTokens();
            _dbService.ClearDatabase();
        }
    }
}