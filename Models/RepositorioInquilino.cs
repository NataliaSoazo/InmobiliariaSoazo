using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace InmobiliariaSoazo.Models
{
    public class RepositorioInquilino
    {

        string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=InmobiliariaSoazo;Trusted_Connection= TRue;MultipleActiveResultsets=true";

        
        public RepositorioInquilino()
        {

        }
        public IList<Inquilino> Obtenertodos()
        {
            var res = new List<Inquilino>();
            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"SELECT Id, Nombre, Apellido, Dni, Telefono, Email, Domicilio 
                              FROM Inquilinos;";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var i = new Inquilino
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Apellido = reader.GetString(2),
                            Dni = reader.GetString(3),
                            Telefono = reader.GetString(4),
                            Email = reader.GetString(5),
                            Domicilio = reader.GetString(6),
                        };
                        res.Add(i);

                    }
                    conn.Close();
                }
            }
            return res;

        }
        public int Alta(Inquilino i)
        {
            var res = 1;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @$"INSERT INTO Inquilinos (Nombre, Apellido, Dni, Telefono, Email, Domicilio)
                             VALUES (@{nameof(i.Nombre)},@{nameof(i.Apellido)},@{nameof(i.Dni)},@{nameof(i.Telefono)},@{nameof(i.Email)},@{nameof(i.Domicilio)});
                             SELECT SCOPE_IDENTITY(); ";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue($"@{nameof(i.Nombre)}", i.Nombre);
                    command.Parameters.AddWithValue($"@{nameof(i.Apellido)}", i.Apellido);
                    command.Parameters.AddWithValue($"@{nameof(i.Dni)}", i.Dni);
                    command.Parameters.AddWithValue($"@{nameof(i.Telefono)}", i.Telefono);
                    command.Parameters.AddWithValue($"@{nameof(i.Email)}", i.Email);
                    command.Parameters.AddWithValue($"@{nameof(i.Domicilio)}", i.Domicilio);
                    conn.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                    conn.Close();
                    i.Id = res;

                }
            }

            return res;
        }
        public Inquilino ObtenerPorId(int id)
        {

            Inquilino i  = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT Id, Nombre, Apellido, Dni, Telefono, Email, Domicilio FROM Inquilinos" +
                    $" WHERE Id=@id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        i = new Inquilino
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Apellido = reader.GetString(2),
                            Dni = reader.GetString(3),
                            Telefono = reader.GetString(4),
                            Email = reader.GetString(5),
                            Domicilio = reader.GetString(6),
                           
                        };
                    }
                    connection.Close();
                }
            }
            return i;
        }

        public int Baja(int id)
        {
            int res = -1;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = $"DELETE FROM Inquilinos WHERE Id = {id}";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {

                    conn.Open();
                    res = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
            return res;
        }

        public int Modificacion(Inquilino i )
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"UPDATE Inquilinos SET Nombre=@nombre, Apellido=@apellido, Dni=@dni, Telefono=@telefono, Email=@email, Domicilio=@domicilio " +
                    $"WHERE Id = @id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@nombre", i.Nombre);
                    command.Parameters.AddWithValue("@apellido", i.Apellido);
                    command.Parameters.AddWithValue("@dni", i.Dni);
                    command.Parameters.AddWithValue("@telefono", i.Telefono);
                    command.Parameters.AddWithValue("@email", i.Email);
                    command.Parameters.AddWithValue("@domicilio", i.Domicilio);
                    command.Parameters.AddWithValue("@id", i.Id);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }
    }
}
