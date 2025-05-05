using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data; 

namespace managerelchenchenvuelve.Controllers
{
    public class DatabaseServerAdmin(IConfiguration configuration) : Controller
    {
        private readonly string _connectionString = configuration.GetConnectionString("conexionDb");
        [AllowAnonymous]
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
            Console.WriteLine("conexion: " + _connectionString); 
            Console.WriteLine("SQlquery:....." + result);
            return result;
        }
        [AllowAnonymous]
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
        [AllowAnonymous]
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
