using System.Text.Json;
using System.Text.Json.Serialization;

namespace barcito.Logica
{
  public class GestorDeCuentas
  {
    private Persistencia.CuentaRepository cuentaRepo;
    public GestorDeCuentas()
    {
      cuentaRepo = new Persistencia.CuentaRepository();
    }
    public int CrearCuenta(Cuenta cuentaData)
    {
      cuentaData.Fecha = DateTime.Now;
      cuentaData.Pagado = false;
      if (cuentaRepo.NotReplicated(cuentaData.Nombre, cuentaData.IdDevice))
      {
        return cuentaRepo.Save(cuentaData);
      }
      return -1;
    }
    public Cuenta ObtenerCuentaPorId(int idCuenta)
    {
      return cuentaRepo.FindById(idCuenta);
    }
    public List<Cuenta> ObtenerTodasLasCuentas()
    {
      return cuentaRepo.FindAll();
    }
  }
}
