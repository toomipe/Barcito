namespace barcito.Logica
{
  public class GestorDeDetalles
  {
    private Persistencia.DetalleCuentaRepository detalleRepo;
    public GestorDeDetalles()
    {
      detalleRepo = new Persistencia.DetalleCuentaRepository();
    }
    public void CrearDetalle(DetalleCuenta detalleData)
    {
      detalleRepo.Save(detalleData);
    }
    public List<DetalleCuenta> ObtenerCuentaPorId(int idCuenta)
    {
      return detalleRepo.FindByCuentaId(idCuenta);
    }
    public List<DetalleCuenta> ObtenerTodasLasCuentas()
    {
      return detalleRepo.FindAll();
    }
  }
}
