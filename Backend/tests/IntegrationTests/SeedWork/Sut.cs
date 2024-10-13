using IntegrationTests.SeedWork.Probes;
using IntegrationTests.Services;
using IntegrationTests.Services.Database;
using Microsoft.AspNetCore.Mvc.Testing;
using WebApi;
using Xunit.Abstractions;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace IntegrationTests.SeedWork
{
    public class Sut : TestBed<Fixture>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly AuthService _authService;
        private readonly DbService _dbService;

        public Sut(ITestOutputHelper outputHelper, Fixture testBed) : base(outputHelper, testBed)
        {
            _factory = testBed.GetService<WebApplicationFactory<Program>>(outputHelper)!;
            _dbService = testBed.GetService<DbService>(outputHelper)!;

            _authService = testBed.GetService<AuthService>(outputHelper)!;
        }

        protected async Task<bool> ExecuteAuthorizedTest(
            Func<WebApplicationFactory<Program>,string,Task<IProbe>> testExecution,
            int delayAfterProbe = 0,
            bool reset = false,
            bool companyToken = false)
        {
            var token = companyToken ? await _authService.GetCompanyToken() : await _authService.GetClientToken();
            
            if (token is null) 
            {
                _dbService.ClearDatabase();
                return false;
            }

            try
            {
                var probe = await testExecution(_factory, token);

                var result = await ProbeTest(probe, delayAfterProbe);

                if (reset || !result) Reset();

                return result;
            }
            catch
            {
                Reset();
                throw;
            }
        }

        protected async Task<bool> ExecuteTest(
            Func<WebApplicationFactory<Program>,Task<IProbe>> testExecution,
            int delayAfterProbe = 0,
            bool reset = false)
        {
            try
            {
                var probe = await testExecution(_factory);

                var result = await ProbeTest(probe, delayAfterProbe);

                if (reset || !result) Reset();

                return result;
            }
            catch
            {
                Reset();
                throw;
            }
        }

        private async Task<bool> ProbeTest(IProbe probe, int delay)
        {
            var result = probe.Test();

            await Task.Delay(delay);

            return result;
        }

        private void Reset()
        {
            _authService.ClearTokens();
            _dbService.ClearDatabase();
        }
    }
}