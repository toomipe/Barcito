using barcito.Logica;
using barcito.Persistencia;
using MySql.Data.MySqlClient;

namespace barcito.Persistencia
{
  public class CuentaRepository
  {
    private ConexiónMySQL conexiónMySQL;

    public CuentaRepository()
    {
      conexiónMySQL = new ConexiónMySQL();
    }

    public int Save(Cuenta cuenta)
    {
      string QUERY = "INSERT INTO Cuenta (nombre, fecha, idDevice, pagado) " +
                      "VALUES (@nombre, @fecha, @idDevice, @pagado)";
      MySqlCommand miComando = new MySqlCommand(QUERY);
      miComando.Connection = conexiónMySQL.GetConnection();

      miComando.Parameters.AddWithValue("@nombre", cuenta.Nombre);
      miComando.Parameters.AddWithValue("@fecha", cuenta.Fecha);
      miComando.Parameters.AddWithValue("@idDevice", cuenta.IdDevice);
      miComando.Parameters.AddWithValue("@pagado", cuenta.Pagado ? 1 : 0);

      miComando.ExecuteNonQuery();
      return (int)miComando.LastInsertedId;
    }

    public Cuenta FindById(int idCuenta)
    {
      string QUERY = "SELECT * FROM Cuenta WHERE idCuenta = @idCuenta";
      MySqlCommand miComando = new MySqlCommand(QUERY);
      miComando.Connection = conexiónMySQL.GetConnection();
      miComando.Parameters.AddWithValue("@idCuenta", idCuenta);

      MySqlDataReader mReader = miComando.ExecuteReader();
      Cuenta cuenta = new Cuenta();

      if (mReader.Read())
      {
        cuenta = new Cuenta
        {
          IdCuenta = mReader.GetInt32("idCuenta"),
          Nombre = mReader.GetString("nombre"),
          Fecha = mReader.GetDateTime("fecha"),
          IdDevice = mReader.GetString("idDevice"),
          Pagado = mReader.GetInt32("pagado") == 1
        };
      }

      mReader.Close();
      return cuenta;
    }

    public List<Cuenta> FindAll()
    {
      string QUERY = "SELECT * FROM Cuenta";
      MySqlCommand miComando = new MySqlCommand(QUERY);
      miComando.Connection = conexiónMySQL.GetConnection();

      MySqlDataReader mReader = miComando.ExecuteReader();
      List<Cuenta> cuentas = new List<Cuenta>();

      while (mReader.Read())
      {
        Cuenta cuenta = new Cuenta
        {
          IdCuenta = mReader.GetInt32("idCuenta"),
          Nombre = mReader.GetString("nombre"),
          Fecha = mReader.GetDateTime("fecha"),
          IdDevice = mReader.GetString("idDevice"),
          Pagado = mReader.GetInt32("pagado") == 1
        };
        cuentas.Add(cuenta);
      }

      mReader.Close();
      return cuentas;
    }


    public bool NotReplicated(string nombre, string device)
    {
      string QUERY = @"
          SELECT *
          FROM Cuenta
          WHERE nombre = @nombre
            AND idDevice = @device
            AND fecha >= @fecha";

      MySqlCommand miComando = new MySqlCommand(QUERY);
      miComando.Connection = conexiónMySQL.GetConnection();

      miComando.Parameters.AddWithValue("@nombre", nombre);
      miComando.Parameters.AddWithValue("@device", device);
      miComando.Parameters.AddWithValue("@fecha", DateTime.Now.Date);

      MySqlDataReader mReader = miComando.ExecuteReader();

      List<Cuenta> cuentas = new List<Cuenta>();

      while (mReader.Read())
      {
        Cuenta cuenta = new Cuenta
        {
          IdCuenta = mReader.GetInt32("idCuenta"),
          Nombre = mReader.GetString("nombre"),
          Fecha = mReader.GetDateTime("fecha"),
          IdDevice = mReader.GetString("idDevice"),
          Pagado = mReader.GetInt32("pagado") == 1,
          Mesa = mReader.GetInt32("mesa")
        };
        cuentas.Add(cuenta);
      }

      mReader.Close();

      return cuentas.Count == 0;
    }


    public bool Update(Cuenta cuenta)
    {
      string QUERY = "UPDATE Cuenta SET nombre = @nombre, fecha = @fecha, idDevice = @idDevice, pagado = @pagado " +
                      "WHERE idCuenta = @idCuenta";
      MySqlCommand miComando = new MySqlCommand(QUERY);
      miComando.Connection = conexiónMySQL.GetConnection();

      miComando.Parameters.AddWithValue("@nombre", cuenta.Nombre);
      miComando.Parameters.AddWithValue("@fecha", cuenta.Fecha);
      miComando.Parameters.AddWithValue("@idDevice", cuenta.IdDevice);
      miComando.Parameters.AddWithValue("@pagado", cuenta.Pagado ? 1 : 0);
      miComando.Parameters.AddWithValue("@idCuenta", cuenta.IdCuenta);

      int filasAfectadas = miComando.ExecuteNonQuery();
      return filasAfectadas > 0;
    }

    public void Delete(int idCuenta)
    {
      string QUERY = "DELETE FROM Cuenta WHERE idCuenta = @idCuenta";
      MySqlCommand miComando = new MySqlCommand(QUERY);
      miComando.Connection = conexiónMySQL.GetConnection();

      miComando.Parameters.AddWithValue("@idCuenta", idCuenta);
      miComando.ExecuteNonQuery();
    }

    public List<Cuenta> FindByDeviceId(string idDevice)
    {
      string QUERY = "SELECT * FROM Cuenta WHERE idDevice = @idDevice";
      MySqlCommand miComando = new MySqlCommand(QUERY);
      miComando.Connection = conexiónMySQL.GetConnection();
      miComando.Parameters.AddWithValue("@idDevice", idDevice);

      MySqlDataReader mReader = miComando.ExecuteReader();
      List<Cuenta> cuentas = new List<Cuenta>();

      while (mReader.Read())
      {
        Cuenta cuenta = new Cuenta
        {
          IdCuenta = mReader.GetInt32("idCuenta"),
          Nombre = mReader.GetString("nombre"),
          Fecha = mReader.GetDateTime("fecha"),
          IdDevice = mReader.GetString("idDevice"),
          Pagado = mReader.GetInt32("pagado") == 1
        };
        cuentas.Add(cuenta);
      }

      mReader.Close();
      return cuentas;
    }

    internal int FindIdByNombreYDispositivo(string nombre, string idDevice)
    {
      string QUERY = "SELECT * FROM cuenta WHERE idDevice = @idDevice AND nombre = @nombre ORDER BY fecha DESC LIMIT 1";
      MySqlCommand miComando = new MySqlCommand(QUERY);
      miComando.Connection = conexiónMySQL.GetConnection();
      miComando.Parameters.AddWithValue("@idDevice", idDevice);
      miComando.Parameters.AddWithValue("@nombre", nombre);

      MySqlDataReader mReader = miComando.ExecuteReader();

      int Idcuenta = 0;
      if (mReader.Read())
      {
        Idcuenta = mReader.GetInt32("idCuenta");
      }

      mReader.Close();
      return Idcuenta;

    }

    public Cuenta? FindByDevice(string idDevice)
    {
      string QUERY = "SELECT * FROM Cuenta WHERE idDevice = @idDevice ORDER BY fecha DESC LIMIT 1";
      MySqlCommand miComando = new MySqlCommand(QUERY);
      miComando.Connection = conexiónMySQL.GetConnection();
      miComando.Parameters.AddWithValue("@idDevice", idDevice);

      MySqlDataReader mReader = miComando.ExecuteReader();
      Cuenta cuenta = new Cuenta();

      if (mReader.Read())
      {
        cuenta = new Cuenta
        {
          IdCuenta = mReader.GetInt32("idCuenta"),
          Nombre = mReader.GetString("nombre"),
          Fecha = mReader.GetDateTime("fecha"),
          IdDevice = mReader.GetString("idDevice"),
          Pagado = mReader.GetInt32("pagado") == 1
        };
      }

      mReader.Close();
      return cuenta;
    }
  }
}

