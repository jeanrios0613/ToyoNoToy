using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace managerelchenchenvuelve.Services
{
	public class DatabaseConnection
    {
        private readonly string _connectionString;

        public DatabaseConnection(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("conexion"); 
        }

        public DataTable ExecuteQuery(string query, params SqlParameter[] parameters)
		{
			DataTable result = new DataTable();

			using (SqlConnection connection = new SqlConnection(_connectionString))
			using (SqlCommand command = new SqlCommand(query, connection))
			{
				if (parameters != null)
					command.Parameters.AddRange(parameters);

				using (SqlDataAdapter adapter = new SqlDataAdapter(command))
				{
					adapter.Fill(result);
				}
			}

			return result;
		}

		public int ExecuteNonQuery(string query, params SqlParameter[] parameters)
		{
			int rowsAffected;

			using (SqlConnection connection = new SqlConnection(_connectionString))
			using (SqlCommand command = new SqlCommand(query, connection))
			{
				if (parameters != null)
					command.Parameters.AddRange(parameters);

				connection.Open();
				rowsAffected = command.ExecuteNonQuery();
			}

			return rowsAffected;
		}

		public object ExecuteScalar(string query, params SqlParameter[] parameters)
		{
			object result;

			using (SqlConnection connection = new SqlConnection(_connectionString))
			using (SqlCommand command = new SqlCommand(query, connection))
			{
				if (parameters != null)
					command.Parameters.AddRange(parameters);

				connection.Open();
				result = command.ExecuteScalar();
			}

			return result;
		}
	}
}
