using System;
using System.Collections;
using Microsoft.Data.SqlClient;
using VentasLibrary.Database;
using VentasLibrary.Models;

namespace VentasLibrary
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

                string query = "get_ventas".Replace("'", "").Replace("%", "").Replace("--", "");
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                ArrayList ventas = new ArrayList();

                while (reader.Read())
                {
                    var venta = new
                    {
                        Id = (int)reader["Id"],
                        Producto = reader["Producto"].ToString(),
                        Unidades = (int)reader["Unidades"],
                        Total = (decimal)reader["Total"],
                        Empleado = reader["Empleado"].ToString(),
                        Fecha_Facturacion = reader["Fecha_Facturacion"].ToString()
                    };

                    ventas.Add(venta);
                }

                if (ventas.Count > 0)
                {
                    conn.Close();
                    return ventas;
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

                string query = "get_venta @Id".Replace("'", "").Replace("%", "").Replace("--", "");
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    var venta = new
                    {
                        Id = (int)reader["Id"],
                        Producto = reader["Producto"].ToString(),
                        Unidades = (int)reader["Unidades"],
                        Total = (decimal)reader["Total"],
                        Empleado = reader["Empleado"].ToString(),
                        Fecha_Facturacion = reader["Fecha_Facturacion"].ToString()
                    };

                    conn.Close();
                    return venta;
                }

                conn.Close();

                return null;
            }
        }

        public dynamic Create(Venta venta)
        {
            using(var conn = _db.GetConnection())
            {
                conn.Open();

                string query = "create_venta @Producto_id, @Empleado_id, @Unidades, @Total".Replace("%", "").Replace("--", "");
                SqlCommand cmd = new SqlCommand(@query, conn);
                cmd.Parameters.AddWithValue("@Producto_id", venta.Producto_Id);
                cmd.Parameters.AddWithValue("@Empleado_id", venta.Empleado_Id);
                cmd.Parameters.AddWithValue("@Unidades", venta.Unidades);
                cmd.Parameters.AddWithValue("@Total", venta.Total);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    var id = reader["VentaId"];

                    if (id != DBNull.Value)
                    {
                        conn.Close();
                        return new { message = "Created successfully" };
                    }
                }

                conn.Close();
                return null;
            }
        }

        public dynamic Update(int id, Venta venta)
        {
            using(var conn = _db.GetConnection())
            {
                conn.Open();

                string query = "SELECT * FROM ventas WHERE id = @Id".Replace("%", "").Replace("--", "");
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

                query = "update_venta @Id, @Producto_id, @Empleado_id, @Unidades, @Total".Replace("%", "").Replace("--", "");
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Producto_id", venta.Producto_Id);
                cmd.Parameters.AddWithValue("@Empleado_id", venta.Empleado_Id);
                cmd.Parameters.AddWithValue("@Unidades", venta.Unidades);
                cmd.Parameters.AddWithValue("@Total", venta.Total);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();

                conn.Close();
                return new { message = "Updated correctly!" };
            }
        }

        public dynamic Delete(int id)
        {
            using(var conn = _db.GetConnection())
            {
                conn.Open();

                string query = "SELECT * FROM ventas WHERE id = @Id".Replace("%", "").Replace("--", "");
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

                query = "DELETE FROM ventas WHERE id = @Id".Replace("%", "").Replace("--", "");
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();

                return new { message = "Deleted successfully" };
            }
        }
    }
}
