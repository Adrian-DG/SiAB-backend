namespace SiAB.API.Helpers
{
	public class DatabaseConnectionService : IDatabaseConnectionService
	{
		private readonly IConfiguration _configuration;
		private readonly IWebHostEnvironment _environment;
		public DatabaseConnectionService(IConfiguration configuration, IWebHostEnvironment environment)
		{
			_configuration = configuration;
			_environment = environment;
		}

		public string GetConnectionString()
		{
			return _environment.IsProduction()
				? _configuration.GetConnectionString("ProdConnection")
				: _configuration.GetConnectionString("DevConnection");
		}
	}
}
