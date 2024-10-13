namespace IntegrationTests.SeedWork.Probes
{
    public class CheckSuccessStatusCodeProbe : IProbe
    {
        private readonly bool _isSuccessStatusCode;

        public CheckSuccessStatusCodeProbe(bool isSuccessStatusCode)
        {
            _isSuccessStatusCode = isSuccessStatusCode;
        }

        public bool Test() => _isSuccessStatusCode;
    }
}
