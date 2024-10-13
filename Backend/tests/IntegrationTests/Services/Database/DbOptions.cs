namespace IntegrationTests.Services.Database
{
    public class DbOptions
    {
        public DbOptions(string connectionString)
        {
            ConnectionString = connectionString;
        }
        
        public string ConnectionString { get; }
    }
}
