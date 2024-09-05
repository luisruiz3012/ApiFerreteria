using EmpleadosLibrary.Database;
using EmpleadosLibrary.Models;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace EmpleadosLibrary
{
    public class Methods
    {
        private readonly DB _db;

        public Methods() {
            _db = new DB();    
        }

        public dynamic Get()
        {
            using(var conn = _db.GetConnection())
            {
                conn.Open();

                string query = "SELECT * FROM empleados".Replace("'", "").Replace("%", "").Replace("--", "");
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                List<Empleado> empleados = new List<Empleado>();

                while (reader.Read())
                {
                    Empleado empleado = new Empleado
                    {
                        Id = (int)reader["Id"],
                        Nombre = reader["Nombre"].ToString(),
                        Apellido = reader["Apellido"].ToString()
                    };

                    empleados.Add(empleado);
                }

                if (empleados.Count > 0)
                {
                    conn.Close();
                    return empleados;
                }

                conn.Close();

                return null;
            }
        }

        public dynamic GetById(int id)
        {
            using(var conn = _db.GetConnection()) {
                conn.Open();

                string query = "SELECT * FROM empleados WHERE id = @Id".Replace("'", "").Replace("%", "").Replace("--", "");
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Empleado empleado = new Empleado
                    {
                        Id = (int)reader["Id"],
                        Nombre = reader["Nombre"].ToString(),
                        Apellido = reader["Apellido"].ToString(),
                    };

                    return empleado;
                }

                return null;
            }
        }

        public dynamic Create(Empleado empleado)
        {
            using(var conn = _db.GetConnection())
            {
                conn.Open();

                string query = "INSERT INTO empleados (nombre, apellido) VALUES (@Nombre, @Apellido)".Replace("'", "").Replace("%", "").Replace("--", "");
                SqlCommand cmd = new SqlCommand (query, conn);
                cmd.Parameters.AddWithValue("@Nombre", empleado.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", empleado.Apellido);
                cmd.ExecuteNonQuery();

                conn.Close();

                conn.Open();

                query = "SELECT * FROM empleados WHERE nombre = @Nombre AND apellido = @Apellido".Replace("'", "").Replace("%", "").Replace("--", "");
                cmd = new SqlCommand (query, conn);
                cmd.Parameters.AddWithValue("@Nombre", empleado.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", empleado.Apellido);
                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.Read()) { conn.Close(); return null; }

                conn.Close();

                return new { message = "Created successfully!" };
            }
        }

        public dynamic Update(int id, Empleado empleado)
        {
            using(var conn = _db.GetConnection())
            {
                conn.Open();

                string query = "SELECT * FROM empleados WHERE id = @id".Replace("'", "").Replace("%", "").Replace("--", "");
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.Read()) { conn.Close(); return null; };

                conn.Close();

                conn.Open();

                query = "UPDATE empleados SET nombre = @Nombre, apellido = @Apellido WHERE id = @Id".Replace("'", "").Replace("%", "").Replace("--", "");
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Nombre", empleado.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", empleado.Apellido);
                cmd.ExecuteNonQuery();

                conn.Close();

                return new { message = "Updated correctly!" };
            }
        }

        public dynamic Delete(int id)
        {
            using (var conn = _db.GetConnection())
            {
                conn.Open();

                string query = "SELECT * FROM empleados WHERE id = @id".Replace("'", "").Replace("%", "").Replace("--", "");
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.Read()) { conn.Close(); return null; };

                conn.Close();

                conn.Open();

                query = "DELETE FROM empleados WHERE id = @Id".Replace("'", "").Replace("%", "").Replace("--", "");
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();

                conn.Close();

                return new { message = "Deleted successfully!" };
            }
        }
    }
}
