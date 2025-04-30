using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data; 

namespace managerelchenchenvuelve.Controllers
{
    public class DatabaseServerAdmin : Controller
    {
        private readonly string _connectionString;

        public DatabaseServerAdmin(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("conexionDb");
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
            Console.WriteLine("SQlquey:....." + result);
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

            Console.WriteLine("SQlquey:....." + rowsAffected);
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
            Console.WriteLine("SQlquey:....." + result);
            return result;
        }
    }
}
