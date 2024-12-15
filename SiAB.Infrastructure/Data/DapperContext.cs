using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Infrastructure.Data
{
	public class DapperContext
	{
		private readonly string _connectionString;
		public DapperContext()
		{
			_connectionString = "Data Source=192.168.4.32;Initial Catalog=db_sipffaa;User ID=5softAdministrator;Password=5softAdministrator;Encrypt=True;Trust Server Certificate=True";
		}

		public IDbConnection CreateConnection()	
		{
			return new SqlConnection(_connectionString);
		}
	}
}
