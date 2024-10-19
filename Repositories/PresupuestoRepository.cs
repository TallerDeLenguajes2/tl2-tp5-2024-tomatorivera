using Microsoft.Data.Sqlite;
using Models;

namespace Persistence;

public class PresupuestoRepository : IRepository<Presupuesto>
{
    private readonly string connectionString = "Data Source=Db/Tienda.db;Cache=Shared";

    public void Insertar(Presupuesto obj)
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            var sqlQuery = @"INSERT INTO Presupuestos (NombreDestinatario, FechaCreacion) VALUES ($nombreDestinatario, $fechaCreacion)";
            var sqlCmd = new SqliteCommand(sqlQuery, connection);
            sqlCmd.Parameters.AddWithValue("$nombreDestinatario", obj.NombreDestinatario);
            sqlCmd.Parameters.AddWithValue("$fechaCreacion", obj.FechaCreacion);
            sqlCmd.ExecuteNonQuery();

            connection.Close();
        }
    }

    public void Eliminar(int id)
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            var sqlQuery = @"DELETE FROM Presupuestos WHERE idPresupuesto=$id";
            var sqlCmd = new SqliteCommand(sqlQuery, connection);
            sqlCmd.Parameters.AddWithValue("$id", id);
            sqlCmd.ExecuteNonQuery();

            connection.Close();
        }
    }

    public List<Presupuesto> Listar()
    {
        var presupuestos = new List<Presupuesto>();

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            var sqlQuery = @"SELECT * FROM Presupuestos";
            var sqlCmd = new SqliteCommand(sqlQuery, connection);
            using (var sqlReader = sqlCmd.ExecuteReader())
            {
                while(sqlReader.Read())
                    presupuestos.Add(generarPresupuesto(sqlReader));
            }

            connection.Close();
        }

        return presupuestos;
    }

    public void Modificar(int id, Presupuesto obj)
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            var sqlQuery = @"UPDATE Presupuestos SET NombreDestinatario=$nombreDestinatario, FechaCreacion=$fechaCreacion WHERE idPresupuesto=$id";
            var sqlCmd = new SqliteCommand(sqlQuery, connection);
            sqlCmd.Parameters.AddWithValue("$nombreDestinatario", obj.NombreDestinatario);
            sqlCmd.Parameters.AddWithValue("$fechaCreacion", obj.FechaCreacion);
            sqlCmd.Parameters.AddWithValue("$id", obj.IdPresupuesto);
            sqlCmd.ExecuteNonQuery();

            connection.Close();
        }
    }

    public Presupuesto Obtener(int id)
    {
        Presupuesto presupuestoBuscado;

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            var sqlQuery = @"SELECT * FROM Presupuestos WHERE idPresupuesto=$id";
            var sqlCmd = new SqliteCommand(sqlQuery, connection);
            sqlCmd.Parameters.AddWithValue("$id", id);
            using (var sqlReader = sqlCmd.ExecuteReader())
            {
                presupuestoBuscado = sqlReader.Read() ? generarPresupuesto(sqlReader)
                                                      : new Presupuesto();
            }

            connection.Close();
        }

        return presupuestoBuscado;
    }

    /// <summary>
    /// Genera un objeto <see cref="Presupuesto"/> partir de los datos 
    /// de un SqliteDataReader generado por alguna consulta
    /// </summary>
    /// <param name="reader">Lector de alguna consulta</param>
    /// <returns>Nueva instancia de <see cref="Presupuesto"/> con los datos
    /// traídos del reader o una instancia por defecto si es que ocurre algun error</returns>
    private Presupuesto generarPresupuesto(SqliteDataReader reader)
    {
        try {
            return new Presupuesto(Convert.ToInt32(reader[0]),
                                   Convert.ToString(reader[1]) ?? string.Empty,
                                   Convert.ToString(reader[2]) ?? string.Empty);
        } catch (Exception) {
            return new Presupuesto();
        }
    }
}
