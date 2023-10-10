namespace kit_api.Connection
{
    public class DBConnection
    {
        private string _connectionString = String.Empty;
        public DBConnection()
        {
            IConfigurationRoot builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _connectionString = builder.GetSection("ConnectionStrings:master").Value;
        }
        public string GetConnectionString()
        {
            return _connectionString;
        }

    }
}
