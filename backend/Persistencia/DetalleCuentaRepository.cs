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

    public void Save(int idCuenta, int idArticulo, int cantidad)
    {

      // cargando el articulo para obtener el precio
      ArticuloRepository articuloRepo = new ArticuloRepository();
      Articulo articulo = articuloRepo.FindById(idArticulo);

      string QUERY = "INSERT INTO DetalleCuenta (idCuenta, idArticulo, cantidad, detalle, precio) " +
                "VALUES (@idCuenta, @idArticulo, @cantidad, @detalle, @precio)";
      MySqlCommand miComando = new MySqlCommand(QUERY);
      miComando.Connection = conexiónMySQL.GetConnection();

      miComando.Parameters.AddWithValue("@idCuenta", idCuenta);
      miComando.Parameters.AddWithValue("@idArticulo", idArticulo);
      miComando.Parameters.AddWithValue("@cantidad", cantidad);
      miComando.Parameters.AddWithValue("@detalle", "");
      miComando.Parameters.AddWithValue("@precio", articulo.Precio * cantidad);

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
          Precio = mReader.GetDecimal("precio"),
          Pagado = mReader.GetBoolean("pagado")
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
          Precio = mReader.GetDecimal("precio"),
          Pagado = mReader.GetBoolean("pagado")
        };
        detalles.Add(detalle);
      }
      mReader.Close();
      return detalles;
    }

    public bool Update(int idDetalleCuenta, string detalle, int cantidad)
    {
      ArticuloRepository articuloRepo = new ArticuloRepository();
      DetalleCuenta detalleCuenta = FindById(idDetalleCuenta);

      
      Articulo articulo = articuloRepo.FindById(detalleCuenta.IdArticulo);
      string query = @"
          UPDATE DetalleCuenta 
          SET cantidad = @cantidad,
              detalle = @detalle,
              precio = @precio
          WHERE idDetalleCuenta = @idDetalleCuenta";

      using (MySqlCommand cmd = new MySqlCommand(query, conexiónMySQL.GetConnection()))
      {
          cmd.Parameters.AddWithValue("@cantidad", cantidad);
          cmd.Parameters.AddWithValue("@detalle", detalle ?? "");
          cmd.Parameters.AddWithValue("@precio", cantidad * articulo.Precio);
          cmd.Parameters.AddWithValue("@idDetalleCuenta", idDetalleCuenta);

          int filasAfectadas = cmd.ExecuteNonQuery();
          return filasAfectadas > 0;
      }
    }


    public void Delete(int idDetalleCuenta)
    {
      string QUERY = "DELETE FROM DetalleCuenta WHERE idDetalleCuenta = @idDetalleCuenta";

      using (MySqlCommand cmd = new MySqlCommand(QUERY, conexiónMySQL.GetConnection()))
      {
        cmd.Parameters.AddWithValue("@idDetalleCuenta", idDetalleCuenta);
        cmd.ExecuteNonQuery();
      }
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
          Precio = mReader.IsDBNull(mReader.GetOrdinal("precio")) ? 0 : mReader.GetDecimal("precio"),
          Pagado = mReader.IsDBNull(mReader.GetOrdinal("pagado")) ? false : mReader.GetBoolean("pagado")
        };
        detalles.Add(detalle);
      }

      mReader.Close();
      return detalles;
    }
  }
}
