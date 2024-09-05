using ProductosLibrary.Database;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using ProductosLibrary.Models;

namespace ProductosLibrary
{
    public class Methods
    {
        private readonly DB _db;

        public Methods()
        {
            _db = new DB();
        }

        public dynamic Get()
        {
            using(var conn = _db.GetConnection())
            {
                conn.Open();

                string query = "SELECT * FROM productos".Replace("'", "").Replace("%", "").Replace("--", "");
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                List<Producto> productos = new List<Producto>();

                while (reader.Read())
                {
                    Producto producto = new Producto { 
                        Id = (int)reader["Id"],
                        Nombre = reader["Nombre"].ToString(),
                        Precio = (decimal)reader["Precio"],
                        Unidades = (int)reader["Unidades"]
                    };

                    productos.Add(producto);
                }

                if (productos.Count > 0)
                {
                    conn.Close();
                    return productos;
                }

                conn.Close();
                return null;
            }
        }

        public dynamic GetById(int id)
        {
            using(var conn = _db.GetConnection())
            {
                conn.Open();

                string query = "SELECT * FROM productos WHERE id = @Id".Replace("'", "").Replace("%", "").Replace("--", "");
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Producto producto = new Producto
                    {
                        Id = (int)reader["Id"],
                        Nombre = reader["Nombre"].ToString(),
                        Precio = (decimal)reader["Precio"],
                        Unidades = (int)reader["Unidades"]
                    };

                    conn.Close();
                    return producto;
                }

                conn.Close();
                return null;
            }
        }

        public dynamic Create(Producto producto)
        {
            using(var conn = _db.GetConnection())
            {
                conn.Open();

                string query = "INSERT INTO productos (nombre, precio, unidades) VALUES (@Nombre, @Precio, @Unidades)".Replace("'", "").Replace("%", "").Replace("--", "");
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                cmd.Parameters.AddWithValue("@Unidades", producto.Unidades);
                cmd.ExecuteNonQuery();

                conn.Close();

                conn.Open();
                query = "SELECT * FROM productos WHERE nombre = @Nombre AND precio = @Precio AND unidades= @Unidades".Replace("'", "").Replace("%", "").Replace("--", "");
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                cmd.Parameters.AddWithValue("@Unidades", producto.Unidades);
                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.Read())
                {
                    return null;
                }

                return new { message = "Created sucessfully" };
            }
        }

        public dynamic Update(int id, Producto producto)
        {
            using(var conn = _db.GetConnection())
            {
                conn.Open();

                string query = "SELECT * FROM productos WHERE id = @Id".Replace("'", "").Replace("%", "").Replace("--", "");
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.Read())
                {
                    conn.Close();
                    return null;
                }

                conn.Close();
                conn.Open();

                query = "UPDATE productos SET nombre = @Nombre, precio = @Precio, unidades = @Unidades WHERE id = @Id".Replace("'", "").Replace("%", "").Replace("--", "");
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                cmd.Parameters.AddWithValue("@Unidades", producto.Unidades);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();

                conn.Close();

                return new { message = "Updated successfully" };
            }
        }

        public dynamic Delete(int id)
        {
            using(var conn = _db.GetConnection())
            {
                conn.Open();

                string query = "SELECT * FROM productos WHERE id = @Id".Replace("'", "").Replace("%", "").Replace("--", "");
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.Read())
                {
                    conn.Close();
                    return null;
                }

                conn.Close();
                conn.Open();

                query = "DELETE FROM productos WHERE id = @Id".Replace("'", "").Replace("%", "").Replace("--", "");
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();

                return new { message = "Deleted successfully" };
            }
        }
    }
}
