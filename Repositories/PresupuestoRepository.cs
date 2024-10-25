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
            sqlCmd.Parameters.AddWithValue("$fechaCreacion", DateTime.Now.ToString("yyyy-MM-ddd"));
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
        throw new NotImplementedException();
    }

    public void Modificar(int id, Presupuesto obj)
    {
        throw new NotImplementedException();
    }

    public Presupuesto Obtener(int id)
    {
        throw new NotImplementedException();
    }
}
