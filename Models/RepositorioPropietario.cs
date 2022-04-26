using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace InmobiliariaSoazo.Models
{
    public class RepositorioPropietario
    {
        string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=InmobiliariaSoazo;Trusted_Connection= TRue;MultipleActiveResultsets=true";
        

        public RepositorioPropietario()
        {

        }
        public IList<Propietario> Obtenertodos()
        {
            var res = new List<Propietario>();
            //reemplaza el List<Propietario> x = new List<propietario>();
            //azucar sintactica
            //var es palabra clave no un tipo
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"SELECT Id, Nombre, Apellido, Dni, Telefono, Email, Domicilio 
                              FROM Propietarios;";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var prop = new Propietario
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Apellido = reader.GetString(2),
                            Dni = reader.GetString(3),
                            Telefono = reader.GetString(4),
                            Email = reader.GetString(5),
                            Domicilio = reader.GetString(6),
                        };
                        res.Add(prop);

                    }
                    conn.Close();
                }
            }
                return res;
         
        }
        public int Alta(Propietario p)
        {
            var res = 1;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @$"INSERT INTO Propietarios (Nombre, Apellido, Dni, Telefono, Email, Domicilio)
                             VALUES (@{nameof(p.Nombre)},@{nameof(p.Apellido)},@{nameof(p.Dni)},@{nameof(p.Telefono)},@{nameof(p.Email)},@{nameof(p.Domicilio)});
                             SELECT SCOPE_IDENTITY(); ";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue($"@{nameof(p.Nombre)}", p.Nombre);
                    command.Parameters.AddWithValue($"@{nameof(p.Apellido)}", p.Apellido);
                    command.Parameters.AddWithValue($"@{nameof(p.Dni)}", p.Dni);
                    command.Parameters.AddWithValue($"@{nameof(p.Telefono)}", p.Telefono);
                    command.Parameters.AddWithValue($"@{nameof(p.Email)}", p.Email);
                    command.Parameters.AddWithValue($"@{nameof(p.Domicilio)}", p.Domicilio);
                    conn.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                    conn.Close();
                    p.Id = res;

                }
            }

            return res;
        }
        public Propietario ObtenerPorId(int id)
        {

            Propietario p = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT Id, Nombre, Apellido, Dni, Telefono, Email,Domicilio FROM Propietarios" +
                    $" WHERE Id=@id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        p = new Propietario
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
            return p;
        }

        public int Baja(int id)
        {
            int res = -1;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = $"DELETE FROM Propietarios WHERE Id = {id}";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    
                    conn.Open();
                    res = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
            return res;
        }

        public int Modificacion(Propietario p)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"UPDATE Propietarios SET Nombre=@nombre, Apellido=@apellido, Dni=@dni, Telefono=@telefono, Email=@email, Domicilio=@domicilio " +
                    $"WHERE Id = @id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@nombre", p.Nombre);
                    command.Parameters.AddWithValue("@apellido", p.Apellido);
                    command.Parameters.AddWithValue("@dni", p.Dni);
                    command.Parameters.AddWithValue("@telefono", p.Telefono);
                    command.Parameters.AddWithValue("@email", p.Email);
                    command.Parameters.AddWithValue("@domicilio", p.Domicilio);
                  
                    command.Parameters.AddWithValue("@id", p.Id);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }

        public static implicit operator RepositorioPropietario(RepositorioInmueble v)
        {
            throw new NotImplementedException();
        }
    }
}
