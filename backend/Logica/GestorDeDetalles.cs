namespace barcito.Logica
{
  public class GestorDeDetalles
  {
    private Persistencia.DetalleCuentaRepository detalleRepo;
    public GestorDeDetalles()
    {
      detalleRepo = new Persistencia.DetalleCuentaRepository();
    }
    public void CrearDetalle(int idCuenta, int idArticulo, int cantidad)
    {
      detalleRepo.Save(idCuenta, idArticulo, cantidad);
    }
    public List<DetalleCuenta> ObtenerCuentaPorId(int idCuenta)
    {
      return detalleRepo.FindByCuentaId(idCuenta);
    }
    public List<DetalleCuenta> ObtenerTodasLasCuentas()
    {
      return detalleRepo.FindAll();
    }
    public void ActualizarDetalle(int idDetalle, string detalle, int cantidad)
    {
      detalleRepo.Update(idDetalle, detalle, cantidad);
    }
    public void EliminarDetalle(int idDetalle)
    {
      detalleRepo.Delete(idDetalle);
    }
  }
}
