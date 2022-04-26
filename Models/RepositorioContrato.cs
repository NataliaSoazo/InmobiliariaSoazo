using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace InmobiliariaSoazo.Models
{
	public class RepositorioContrato
	{


		string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=InmobiliariaSoazo;Trusted_Connection= TRue;MultipleActiveResultsets=true";






		public IList<Contrato> ObtenerTodos()
		{
			IList<Contrato> res = new List<Contrato>();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{ 

				string sql = @" SELECT i.Id, FechaInicio, FechaTerm, IdInquilino,  p.Nombre, p.Apellido,IdInmueble, m.Direccion 
								FROM Contratos i INNER JOIN Inquilinos p ON i.IdInquilino = p.Id 
								INNER JOIN Inmuebles m ON i.IdInmueble = m.Id		
				";
                using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						Contrato entidad = new Contrato()
						{
							Id = reader.GetInt32(0),
							FechaInicio = reader.GetDateTime(1),
							FechaTerm = reader.GetDateTime(2),
							IdInquilino = reader.GetInt32(3),
							

							Locatario = new Inquilino
                            {
								Nombre = reader.GetString(4),
								Apellido = reader.GetString(5),
			
                            },
							IdInmueble = reader.GetInt32(6),
							Datos = new Inmueble
							{
								
								Direccion = reader.GetString(7)


							}
							
						};										

						res.Add(entidad);
					}
						
					}
					connection.Close();
				}

			return res;
		}
			

		public int Alta(Contrato entidad)
		{
			int res = -1;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"INSERT INTO Contratos ( FechaInicio, FechaTerm,IdInquilino, IdInmueble) " +
					"VALUES (@fechaInicio, @fechaTerm, @idInquilino, @idInmueble );" +
					"SELECT SCOPE_IDENTITY();";//devuelve el id insertado (LAST_INSERT_ID para mysql)
				using (var command = new SqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@idInquilino", entidad.IdInquilino);
					command.Parameters.AddWithValue("@idInmueble", entidad.IdInmueble);
					command.Parameters.AddWithValue("@fechaInicio", entidad.FechaInicio);
					command.Parameters.AddWithValue("@fechaTerm", entidad.FechaTerm);
					connection.Open();
					res = Convert.ToInt32(command.ExecuteScalar());
					entidad.Id = res;
					connection.Close();
				}
			}
			return res;
		}
	
	public int Modificacion(Contrato entidad)
		{
			int res = -1;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = "UPDATE Contratos SET " +
				"FechaInicio = @fechaInicio, FechaTerm = @fechaTerm, IdInquilino = @idInquilino, IdInmueble = @idInmueble " +
					"WHERE Id = @id";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					
					command.Parameters.AddWithValue("@fechaInicio", entidad.FechaInicio);
					command.Parameters.AddWithValue("@fechaTerm", entidad.FechaTerm);
					command.Parameters.AddWithValue("@idInquilino", entidad.IdInquilino);
					command.Parameters.AddWithValue("@idInmueble", entidad.IdInmueble);
					command.Parameters.AddWithValue("@id", entidad.Id);
					command.CommandType = CommandType.Text;
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}



		public Contrato ObtenerPorId(int id)
		{
			Contrato entidad = null;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = @" SELECT i.Id, FechaInicio, FechaTerm, IdInquilino,  p.Nombre, p.Apellido,IdInmueble, m.Direccion 
								FROM Contratos i INNER JOIN Inquilinos p ON i.IdInquilino = p.Id 
								INNER JOIN Inmuebles m ON i.IdInmueble = m.Id" +
                               $" WHERE i.Id=@id";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.Parameters.Add("@id", SqlDbType.Int).Value = id;
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					if (reader.Read())
					{
						entidad = new Contrato
						{
							Id = reader.GetInt32(0),
							FechaInicio = reader.GetDateTime(1),
							FechaTerm = reader.GetDateTime(2),
							IdInquilino = reader.GetInt32(3),


							Locatario = new Inquilino
							{
								Nombre = reader.GetString(4),
								Apellido = reader.GetString(5),

							},
							IdInmueble = reader.GetInt32(6),
							Datos = new Inmueble
							{

								Direccion = reader.GetString(7)


							}


						};
					}
					connection.Close();
				}
			}
			return entidad;
		}
		public int Baja(int Id)
		{
			int res = -1;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"DELETE FROM Contratos WHERE Id = {Id}";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}
	}
}
    

