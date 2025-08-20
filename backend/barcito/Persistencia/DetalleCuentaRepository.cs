using barcito.Logica;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace barcito.Persistencia
{
  public class DetalleCuentaRepository
  {
    private ConexiónMySQL conexiónMySQL;

    public DetalleCuentaRepository()
    {
      conexiónMySQL = new ConexiónMySQL();
    }

    public void Save(DetalleCuenta detalle)
    {
      string QUERY = "INSERT INTO DetalleCuenta (idCuenta, idArticulo, cantidad, detalle, descuento) " +
                     "VALUES (@idCuenta, @idArticulo, @cantidad, @detalle, @descuento)";
      MySqlCommand miComando = new MySqlCommand(QUERY);
      miComando.Connection = conexiónMySQL.GetConnection();

      miComando.Parameters.AddWithValue("@idCuenta", detalle.IdCuenta);
      miComando.Parameters.AddWithValue("@idArticulo", detalle.IdArticulo);
      miComando.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
      miComando.Parameters.AddWithValue("@detalle", detalle.Detalle ?? "");
      miComando.Parameters.AddWithValue("@descuento", detalle.Descuento == default(decimal) ? 0 : detalle.Descuento);
      Console.WriteLine(QUERY);
      miComando.ExecuteNonQuery();
    }
        
    public DetalleCuenta FindById(int idDetalleCuenta)
    {
      string QUERY = "SELECT * FROM DetalleCuenta WHERE idDetalleCuenta = @idDetalleCuenta";
      MySqlCommand miComando = new MySqlCommand(QUERY);
      miComando.Connection = conexiónMySQL.GetConnection();
      miComando.Parameters.AddWithValue("@idDetalleCuenta", idDetalleCuenta);

      MySqlDataReader mReader = miComando.ExecuteReader();
      DetalleCuenta detalle = null;

      if (mReader.Read())
      {
        detalle = new DetalleCuenta
        {
          IdDetalleCuenta = mReader.GetInt32("idDetalleCuenta"),
          IdCuenta = mReader.GetInt32("idCuenta"),
          IdArticulo = mReader.GetInt32("idArticulo"),
          Cantidad = mReader.GetInt32("cantidad"),
          Detalle =mReader.GetString("detalle"),
          Descuento = mReader.GetDecimal("descuento")
        };
      }

      mReader.Close();
      return detalle;
    }

    public List<DetalleCuenta> FindAll()
    {
      string QUERY = "SELECT * FROM DetalleCuenta";
      MySqlCommand miComando = new MySqlCommand(QUERY);
      miComando.Connection = conexiónMySQL.GetConnection();

      MySqlDataReader mReader = miComando.ExecuteReader();
      List<DetalleCuenta> detalles = new List<DetalleCuenta>();

      while (mReader.Read())
      {
        DetalleCuenta detalle = new DetalleCuenta
        {
          IdDetalleCuenta = mReader.GetInt32("idDetalleCuenta"),
          IdCuenta = mReader.GetInt32("idCuenta"),
          IdArticulo = mReader.GetInt32("idArticulo"),
          Cantidad = mReader.GetInt32("cantidad"),
          Detalle = mReader.GetString("detalle"),
          Descuento = mReader.GetDecimal("descuento")
        };
        detalles.Add(detalle);
      }
      mReader.Close();
      return detalles;
    }

    public bool Update(DetalleCuenta detalle)
    {
      string QUERY = "UPDATE DetalleCuenta SET idCuenta = @idCuenta, idArticulo = @idArticulo, " +
                     "cantidad = @cantidad, detalle = @detalle, descuento = @descuento " +
                     "WHERE idDetalleCuenta = @idDetalleCuenta";
      MySqlCommand miComando = new MySqlCommand(QUERY);
      miComando.Connection = conexiónMySQL.GetConnection();

      miComando.Parameters.AddWithValue("@idCuenta", detalle.IdCuenta);
      miComando.Parameters.AddWithValue("@idArticulo", detalle.IdArticulo);
      miComando.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
      miComando.Parameters.AddWithValue("@detalle", detalle.Detalle ?? "");
      miComando.Parameters.AddWithValue("@descuento", detalle.Descuento);
      miComando.Parameters.AddWithValue("@idDetalleCuenta", detalle.IdDetalleCuenta);

      int filasAfectadas = miComando.ExecuteNonQuery();
      return filasAfectadas > 0;
    }

    public void Delete(int idCuenta)
    {
      string QUERY = "DELETE FROM DetalleCuenta WHERE idCuenta = @idCuenta";
      MySqlCommand miComando = new MySqlCommand(QUERY);
      miComando.Connection = conexiónMySQL.GetConnection();

      miComando.Parameters.AddWithValue("@idDetalleCuenta", idCuenta);
      miComando.ExecuteNonQuery();
    }

    public List<DetalleCuenta> FindByCuentaId(int idCuenta)
    {
      string QUERY = "SELECT * FROM detallecuenta WHERE idCuenta = @idCuenta";
      MySqlCommand miComando = new MySqlCommand(QUERY);
      miComando.Connection = conexiónMySQL.GetConnection();
      miComando.Parameters.AddWithValue("@idCuenta", idCuenta);

      Console.WriteLine(QUERY);

      MySqlDataReader mReader = miComando.ExecuteReader();
      List<DetalleCuenta> detalles = new List<DetalleCuenta>();

      while (mReader.Read())
      {
        DetalleCuenta detalle = new DetalleCuenta
        {
          IdDetalleCuenta = mReader.IsDBNull(mReader.GetOrdinal("idDetalleCuenta")) ? 0 : mReader.GetInt32("idDetalleCuenta"),
          IdCuenta = mReader.IsDBNull(mReader.GetOrdinal("idCuenta")) ? 0 : mReader.GetInt32("idCuenta"),
          IdArticulo = mReader.IsDBNull(mReader.GetOrdinal("idArticulo")) ? 0 : mReader.GetInt32("idArticulo"),
          Cantidad = mReader.IsDBNull(mReader.GetOrdinal("cantidad")) ? 0 : mReader.GetInt32("cantidad"),
          Detalle = mReader.IsDBNull(mReader.GetOrdinal("detalle")) ? null : mReader.GetString("detalle"),
          Descuento = mReader.IsDBNull(mReader.GetOrdinal("descuento")) ? 0 : mReader.GetDecimal("descuento")
        };
        detalles.Add(detalle);
      }

      mReader.Close();
      return detalles;
    }
  }
}
