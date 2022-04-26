using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace InmobiliariaSoazo.Models
{
	public class RepositorioPagos
	{
		string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=InmobiliariaSoazo;Trusted_Connection= TRue;MultipleActiveResultsets=true";




		public int Alta(Pagos entidad)
			{
				int res = -1;
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					string sql = $"INSERT INTO Pagos ( NroPago, Fecha, Importe, IdContrato ) " +
						"VALUES (@nroPago, @fecha, @importe, @idContrato);" +
						"SELECT SCOPE_IDENTITY();";//devuelve el id insertado (LAST_INSERT_ID para mysql)
					using (var command = new SqlCommand(sql, connection))
					{
						command.CommandType = CommandType.Text;
						command.Parameters.AddWithValue("@nroPago", entidad.NroPago);
						command.Parameters.AddWithValue("@fecha", entidad.Fecha);
						command.Parameters.AddWithValue("@importe", entidad.Importe);
						command.Parameters.AddWithValue("@idContrato", entidad.IdContrato);
						
						connection.Open();
						res = Convert.ToInt32(command.ExecuteScalar());
						entidad.IdContrato = res;
						connection.Close();
					}
				}
				return res;
			}
			public int Baja(int Id)
			{
				int res = -1;
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					string sql = $"DELETE FROM Inmuebles WHERE IdContrato = {Id}";
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
			public int Modificacion(Pagos entidad)
			{
				int res = -1;
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					string sql = "UPDATE Inmuebles SET " +
						"NroPago = @nroPago, Fecha=@fecha, Importe = @importe  " +
						"WHERE IdContrato = @id";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						
						command.Parameters.AddWithValue("@nroPago", entidad.NroPago);
						command.Parameters.AddWithValue("@fecha", entidad.Fecha);
						command.Parameters.AddWithValue("@importe", entidad.Importe);
						command.Parameters.AddWithValue("@id", entidad.IdContrato);
						command.CommandType = CommandType.Text;
						connection.Open();
						res = command.ExecuteNonQuery();
						connection.Close();
					}
				}
				return res;
			}

		public IList<Pagos> ObtenerTodos()
		{
			IList<Pagos> res = new List<Pagos>();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = "SELECT  NroPago, Fecha, Importe, IdContrato,  IdInquilino, Nombre, Apellido " +
					" FROM Pagos  INNER JOIN Contratos c ON IdContrato = c.Id " +
					"INNER JOIN Inquilinos i ON IdInquilino = i.Id ";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{

					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						var prop = new Pagos
						{
							NroPago = reader.GetInt32(0),
							Fecha = reader.GetDateTime(1),
							Importe = reader.GetDecimal(2),
							IdContrato = reader.GetInt32(3),
							Datos = new Contrato
							{
								Id = reader.GetInt32(3),
								IdInquilino = reader.GetInt32(4),
								Locatario = new Inquilino
								{
									Nombre = reader.GetString(5),
									Apellido = reader.GetString(6),
								}

							}


						};
						res.Add(prop);

					}
					connection.Close();
				}
			}
			return res;

		}
		public Pagos ObtenerPorId(int id)
		{
			Pagos entidad = null;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = "SELECT  NroPago, Fecha, Importe, IdContrato, IdInquilino, Nombre, Apellido " +
					" FROM Pagos  INNER JOIN Contratos c ON IdContrato = c.Id " +
					"INNER JOIN Inquilinos i ON IdInquilino = i.Id " + " WHERE i.IdContrato =@id";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.Parameters.Add("@id", SqlDbType.Int).Value = id;
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					if (reader.Read())
					{
						entidad = new Pagos
						{
							NroPago = reader.GetInt32(0),
							Fecha = reader.GetDateTime(1),
							Importe = reader.GetDecimal(2),
							IdContrato = reader.GetInt32(3),
							Datos = new Contrato
							{
								Id = reader.GetInt32(3),
								IdInquilino = reader.GetInt32(4),
								Locatario = new Inquilino
								{
									Nombre = reader.GetString(5),
									Apellido = reader.GetString(6),
								}

							}



						};
					}
					connection.Close();
				}
			}
			return entidad;
		}

	}

}

		
