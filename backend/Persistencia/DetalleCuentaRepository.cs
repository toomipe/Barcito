using barcito.Logica;
using Microsoft.Extensions.ObjectPool;
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

      // Console.WriteLine(QUERY);
      miComando.ExecuteNonQuery();
    }

    public DetalleCuenta FindById(int idDetalleCuenta)
    {
      string QUERY = "SELECT * FROM DetalleCuenta WHERE idDetalleCuenta = @idDetalleCuenta";
      MySqlCommand miComando = new MySqlCommand(QUERY);
      miComando.Connection = conexiónMySQL.GetConnection();
      miComando.Parameters.AddWithValue("@idDetalleCuenta", idDetalleCuenta);

      MySqlDataReader mReader = miComando.ExecuteReader();
      DetalleCuenta detalle = new DetalleCuenta();

      if (mReader.Read())
      {
        detalle = new DetalleCuenta
        {
          IdDetalleCuenta = mReader.GetInt32("idDetalleCuenta"),
          IdCuenta = mReader.GetInt32("idCuenta"),
          IdArticulo = mReader.GetInt32("idArticulo"),
          Cantidad = mReader.GetInt32("cantidad"),
          Detalle = mReader.GetString("detalle"),
          Precio = mReader.GetDecimal("precio"),
          Pagado = mReader.GetBoolean("pagado"),
          Estado = mReader.GetString("estado")
        };
      }

      mReader.Close();
      return detalle;
    }

    public List<DetalleCuentaCompleta> FindDetallesEnPreparación()
    {
      string QUERY = "SELECT * FROM DetalleCuenta JOIN Articulo ON DetalleCuenta.idArticulo = Articulo.ArticuloID WHERE estado = 'EP' OR estado = 'DC'";
      MySqlCommand miComando = new MySqlCommand(QUERY);
      miComando.Connection = conexiónMySQL.GetConnection();

      MySqlDataReader mReader = miComando.ExecuteReader();
      List<DetalleCuentaCompleta> detalles = new List<DetalleCuentaCompleta>();

      while (mReader.Read())
      {
        DetalleCuentaCompleta detalle = new DetalleCuentaCompleta
        {
          IdDetalleCuenta = mReader.GetInt32("idDetalleCuenta"),
          IdCuenta = mReader.GetInt32("idCuenta"),
          IdArticulo = mReader.GetInt32("idArticulo"),
          Cantidad = mReader.GetInt32("cantidad"),
          Detalle = mReader.GetString("detalle"),
          Precio = mReader.GetDecimal("precio"),
          Pagado = mReader.GetBoolean("pagado"),
          Estado = mReader.GetString("estado"),
          NombreArticulo = mReader.GetString("Nombre"),
          DescripcionArticulo = mReader.GetString("Descripcion"),
          DetalleArticulo = mReader.GetString("detalle")
        };
        detalles.Add(detalle);
      }
      mReader.Close();
      return detalles;
    }

    public List<DetalleConCuenta> FindDetallesParaEntregar()
    {
      string QUERY = @"
        SELECT 
          dc.idDetalleCuenta,
          dc.idCuenta,
          dc.idArticulo,
          dc.cantidad,
          dc.detalle AS detalleCuenta,
          dc.precio,
          dc.pagado,
          dc.estado,
          a.Nombre AS nombreArticulo,
          a.Descripcion AS descripcionArticulo,
          dc.detalle AS detalleArticulo,
          c.nombre AS nombreCuenta,
          c.mesa
        FROM DetalleCuenta dc
        JOIN Articulo a ON dc.idArticulo = a.ArticuloID
        JOIN Cuenta c ON dc.idCuenta = c.idCuenta
        WHERE dc.estado = 'PE'
        ";

      MySqlCommand miComando = new MySqlCommand(QUERY);
      miComando.Connection = conexiónMySQL.GetConnection();

      MySqlDataReader mReader = miComando.ExecuteReader();
      List<DetalleConCuenta> detalles = new List<DetalleConCuenta>();

      while (mReader.Read())
      {
        DetalleConCuenta detalle = new DetalleConCuenta
        {
          IdDetalleCuenta = mReader.GetInt32("idDetalleCuenta"),
          IdCuenta = mReader.GetInt32("idCuenta"),
          IdArticulo = mReader.GetInt32("idArticulo"),
          Cantidad = mReader.GetInt32("cantidad"),
          Detalle = mReader.GetString("detalleCuenta"),
          Precio = mReader.GetDecimal("precio"),
          Pagado = mReader.GetBoolean("pagado"),
          Estado = mReader.GetString("estado"),
          NombreArticulo = mReader.GetString("nombreArticulo"),
          DescripcionArticulo = mReader.GetString("descripcionArticulo"),
          DetalleArticulo = mReader.GetString("detalleArticulo"),
          nombre = mReader.GetString("nombreCuenta"),
          mesa = mReader.GetInt32("mesa")
        };

        detalles.Add(detalle);
      }

      mReader.Close();
      return detalles;
    }

    public List<DetalleConCuenta> FindDetallesPorMesa()
    {
      string QUERY = @"
        SELECT 
          dc.idDetalleCuenta,
          dc.idCuenta,
          dc.idArticulo,
          dc.cantidad,
          dc.detalle AS detalleCuenta,
          dc.precio,
          dc.pagado,
          dc.estado,
          a.Nombre AS nombreArticulo,
          a.Descripcion AS descripcionArticulo,
          dc.detalle AS detalleArticulo,
          c.nombre AS nombreCuenta,
          c.mesa
        FROM DetalleCuenta dc
        JOIN Articulo a ON dc.idArticulo = a.ArticuloID
        JOIN Cuenta c ON dc.idCuenta = c.idCuenta";

      MySqlCommand miComando = new MySqlCommand(QUERY);
      miComando.Connection = conexiónMySQL.GetConnection();

      MySqlDataReader mReader = miComando.ExecuteReader();
      List<DetalleConCuenta> detalles = new List<DetalleConCuenta>();

      while (mReader.Read())
      {
        DetalleConCuenta detalle = new DetalleConCuenta
        {
          IdDetalleCuenta = mReader.GetInt32("idDetalleCuenta"),
          IdCuenta = mReader.GetInt32("idCuenta"),
          IdArticulo = mReader.GetInt32("idArticulo"),
          Cantidad = mReader.GetInt32("cantidad"),
          Detalle = mReader.GetString("detalleCuenta"),
          Precio = mReader.GetDecimal("precio"),
          Pagado = mReader.GetBoolean("pagado"),
          Estado = mReader.GetString("estado"),
          NombreArticulo = mReader.GetString("nombreArticulo"),
          DescripcionArticulo = mReader.GetString("descripcionArticulo"),
          DetalleArticulo = mReader.GetString("detalleArticulo"),
          nombre = mReader.GetString("nombreCuenta"),
          mesa = mReader.GetInt32("mesa")
        };

        detalles.Add(detalle);
      }

      mReader.Close();
      return detalles;
    }

    public void UpdateEstado(int idDetalleCuenta, string estado)
    {
      string QUERY = "UPDATE DetalleCuenta SET estado = @estado WHERE idDetalleCuenta = @idDetalleCuenta";
      MySqlCommand miComando = new MySqlCommand(QUERY);
      miComando.Connection = conexiónMySQL.GetConnection();
      miComando.Parameters.AddWithValue("@estado", estado);
      miComando.Parameters.AddWithValue("@idDetalleCuenta", idDetalleCuenta);

      miComando.ExecuteNonQuery();
    }

    public List<DetalleCuenta> FindAllNotPaid()
    {
      string QUERY = "SELECT * FROM DetalleCuenta WHERE pagado = 0";
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
          Pagado = mReader.GetBoolean("pagado"),
          Estado = mReader.GetString("estado")
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


    public List<DetalleCuenta> FindByCuentaIdNotPaid(int idCuenta)
    {
      string QUERY = "SELECT * FROM detallecuenta WHERE idCuenta = @idCuenta AND pagado = 0";
      MySqlCommand miComando = new MySqlCommand(QUERY);
      miComando.Connection = conexiónMySQL.GetConnection();
      miComando.Parameters.AddWithValue("@idCuenta", idCuenta);

      // Console.WriteLine(QUERY);

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
          Pagado = mReader.IsDBNull(mReader.GetOrdinal("pagado")) ? false : mReader.GetBoolean("pagado"),
          Estado = mReader.IsDBNull(mReader.GetOrdinal("estado")) ? null : mReader.GetString("estado")
        };
        detalles.Add(detalle);
      }

      mReader.Close();
      return detalles;
    }

    public List<DetalleCuenta> FindByCuentaIdPaid(int idCuenta)
    {
      string QUERY = "SELECT * FROM detallecuenta WHERE idCuenta = @idCuenta AND pagado = 1";
      MySqlCommand miComando = new MySqlCommand(QUERY);
      miComando.Connection = conexiónMySQL.GetConnection();
      miComando.Parameters.AddWithValue("@idCuenta", idCuenta);

      // Console.WriteLine(QUERY);

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
          Pagado = mReader.IsDBNull(mReader.GetOrdinal("pagado")) ? false : mReader.GetBoolean("pagado"),
          Estado = mReader.IsDBNull(mReader.GetOrdinal("estado")) ? null : mReader.GetString("estado")
        };
        detalles.Add(detalle);
      }

      mReader.Close();
      return detalles;
    }

    public List<DetalleCuentaP> FindAllPaidOrders(int idCuenta)
    {
      string QUERY = "SELECT * FROM detalleCuenta JOIN articulo ON detalleCuenta.idArticulo = articulo.ArticuloId WHERE idCuenta = @idCuenta AND detalleCuenta.pagado = 1";
      MySqlCommand miComando = new MySqlCommand(QUERY);
      miComando.Connection = conexiónMySQL.GetConnection();
      miComando.Parameters.AddWithValue("@idCuenta", idCuenta);

      MySqlDataReader mReader = miComando.ExecuteReader();
      List<DetalleCuentaP> detalles = new List<DetalleCuentaP>();

      while (mReader.Read())
      {
        DetalleCuentaP detalle = new DetalleCuentaP
        {
          idDetalleP = mReader.GetInt32("idDetalleCuenta"),
          urlImagen = mReader.IsDBNull(mReader.GetOrdinal("urlImagen")) ? null : mReader.GetString("urlImagen"),
          nombre = mReader.IsDBNull(mReader.GetOrdinal("nombre")) ? null : mReader.GetString("nombre"),
          descripcion = mReader.IsDBNull(mReader.GetOrdinal("descripcion")) ? null : mReader.GetString("descripcion"),
          precio = mReader.IsDBNull(mReader.GetOrdinal("precio")) ? 0 : mReader.GetDecimal("precio"),
          cantidad = mReader.IsDBNull(mReader.GetOrdinal("cantidad")) ? 0 : mReader.GetInt32("cantidad"),
          estado = mReader.IsDBNull(mReader.GetOrdinal("estado")) ? null : mReader.GetString("estado")
        };
        detalles.Add(detalle);
      }
      mReader.Close();
      return detalles;
    }
    public async Task<bool> GenerarPagoEfectivo(List<int> detalles)
    {
      int filasAfectadas = 0;

      foreach (var idDetalle in detalles)
      {
        string QUERY = "UPDATE DetalleCuenta SET pagado = 1, estado = 'EP' WHERE idDetalleCuenta = @idDetalleCuenta";

        using var miComando = new MySqlCommand(QUERY);
        miComando.Connection = conexiónMySQL.GetConnection();
        miComando.Parameters.AddWithValue("@idDetalleCuenta", idDetalle);

        Console.WriteLine(QUERY);

        Console.WriteLine("Ejecutando actualización de pago para detalle ID: " + idDetalle);

        filasAfectadas += await miComando.ExecuteNonQueryAsync();
      }

      return filasAfectadas == detalles.Count;
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
          Pagado = mReader.GetBoolean("pagado"),
          Estado = mReader.GetString("estado")
        };
        detalles.Add(detalle);
      }
      mReader.Close();
      return detalles;
    }

    public List<DetalleCuenta> FindByCuentaId(int idCuenta)
    {
      string QUERY = "SELECT * FROM detallecuenta WHERE idCuenta = @idCuenta";
      MySqlCommand miComando = new MySqlCommand(QUERY);
      miComando.Connection = conexiónMySQL.GetConnection();
      miComando.Parameters.AddWithValue("@idCuenta", idCuenta);

      // Console.WriteLine(QUERY);

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
          Pagado = mReader.IsDBNull(mReader.GetOrdinal("pagado")) ? false : mReader.GetBoolean("pagado"),
          Estado = mReader.IsDBNull(mReader.GetOrdinal("estado")) ? null : mReader.GetString("estado")
        };
        detalles.Add(detalle);
      }

      mReader.Close();
      return detalles;
    }


    //detalles pedidos con info del articulo
    public List<DetalleCuentaCompleta> FindDetallesByCuentaId(int idCuenta)
    {
      string QUERY = "SELECT * FROM detallecuenta JOIN articulo ON detallecuenta.idArticulo = articulo.ArticuloID WHERE idCuenta = @idCuenta";
      MySqlCommand miComando = new MySqlCommand(QUERY);
      miComando.Connection = conexiónMySQL.GetConnection();
      miComando.Parameters.AddWithValue("@idCuenta", idCuenta);

      // Console.WriteLine(QUERY);

      MySqlDataReader mReader = miComando.ExecuteReader();
      List<DetalleCuentaCompleta> detalles = new List<DetalleCuentaCompleta>();

      while (mReader.Read())
      {
        DetalleCuentaCompleta detalle = new DetalleCuentaCompleta
        {
          IdDetalleCuenta = mReader.IsDBNull(mReader.GetOrdinal("idDetalleCuenta")) ? 0 : mReader.GetInt32("idDetalleCuenta"),
          IdCuenta = mReader.IsDBNull(mReader.GetOrdinal("idCuenta")) ? 0 : mReader.GetInt32("idCuenta"),
          IdArticulo = mReader.IsDBNull(mReader.GetOrdinal("idArticulo")) ? 0 : mReader.GetInt32("idArticulo"),
          Cantidad = mReader.IsDBNull(mReader.GetOrdinal("cantidad")) ? 0 : mReader.GetInt32("cantidad"),
          Detalle = mReader.IsDBNull(mReader.GetOrdinal("detalle")) ? null : mReader.GetString("detalle"),
          Precio = mReader.IsDBNull(mReader.GetOrdinal("precio")) ? 0 : mReader.GetDecimal("precio"),
          Pagado = mReader.IsDBNull(mReader.GetOrdinal("pagado")) ? false : mReader.GetBoolean("pagado"),
          Estado = mReader.IsDBNull(mReader.GetOrdinal("estado")) ? null : mReader.GetString("estado"),
          NombreArticulo = mReader.IsDBNull(mReader.GetOrdinal("Nombre")) ? null : mReader.GetString("Nombre"),
          DescripcionArticulo = mReader.IsDBNull(mReader.GetOrdinal("Descripcion")) ? null : mReader.GetString("Descripcion"),
          DetalleArticulo = mReader.IsDBNull(mReader.GetOrdinal("detalle")) ? null : mReader.GetString("detalle"),
          formaDePago = mReader.IsDBNull(mReader.GetOrdinal("formaPago")) ? null : mReader.GetBoolean("formaPago")
        };
        detalles.Add(detalle);
      }

      mReader.Close();
      return detalles;
    }

    internal object? MarkAsPaidByCuenta(int idCuenta)
    {
      string QUERY = "UPDATE detallecuenta SET pagado = 1 WHERE idCuenta = @idCuenta";
      MySqlCommand miComando = new MySqlCommand(QUERY);
      miComando.Connection = conexiónMySQL.GetConnection();
      miComando.Parameters.AddWithValue("@idCuenta", idCuenta);

      return miComando.ExecuteNonQuery();
    }

    internal object? MarkAsPaid(int idDetalleCuenta)
    {
      string QUERY = "UPDATE detallecuenta SET pagado = 1 WHERE idDetalleCuenta = @idDetalleCuenta";
      MySqlCommand miComando = new MySqlCommand(QUERY);
      miComando.Connection = conexiónMySQL.GetConnection();
      miComando.Parameters.AddWithValue("@idDetalleCuenta", idDetalleCuenta);

      return miComando.ExecuteNonQuery();
    }

    internal List<DetalleCuentaDos> FindDetallesNoPagadosDos(int idCuenta)
    {
      string QUERY = "SELECT * FROM detallecuenta JOIN articulo ON detallecuenta.idArticulo = articulo.ArticuloID WHERE idCuenta = @idCuenta AND pagado = 0";
      MySqlCommand miComando = new MySqlCommand(QUERY);
      miComando.Connection = conexiónMySQL.GetConnection();
      miComando.Parameters.AddWithValue("@idCuenta", idCuenta);

      MySqlDataReader mReader = miComando.ExecuteReader();
      List<DetalleCuentaDos> detalles = new List<DetalleCuentaDos>();

      while (mReader.Read())
      {
        DetalleCuentaDos detalle = new DetalleCuentaDos();

        detalle.idDetalleP = mReader.IsDBNull(mReader.GetOrdinal("idDetalleCuenta")) ? 0 : mReader.GetInt32("idDetalleCuenta");
        detalle.urlImagen = mReader.IsDBNull(mReader.GetOrdinal("urlImagen")) ? null : mReader.GetString("urlImagen");
        detalle.nombre = mReader.IsDBNull(mReader.GetOrdinal("Nombre")) ? null : mReader.GetString("Nombre");
        detalle.descripcion = mReader.IsDBNull(mReader.GetOrdinal("Descripcion")) ? null : mReader.GetString("Descripcion");
        detalle.precio = mReader.IsDBNull(mReader.GetOrdinal("precio")) ? 0 : mReader.GetDecimal("precio");
        detalle.cantidad = mReader.IsDBNull(mReader.GetOrdinal("cantidad")) ? 0 : mReader.GetInt32("cantidad");
        detalle.detalle = mReader.IsDBNull(mReader.GetOrdinal("detalle")) ? null : mReader.GetString("detalle");
        detalle.estado = mReader.IsDBNull(mReader.GetOrdinal("estado")) ? null : mReader.GetString("estado");

        detalles.Add(detalle);
      }

      mReader.Close();
      return detalles;
    }
  }
}
