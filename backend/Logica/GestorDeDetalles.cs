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
    public List<DetalleCuenta> ObtenerCuentaPorIdNoPagado(int idCuenta)
    {
      return detalleRepo.FindByCuentaIdNotPaid(idCuenta);
    }

    public List<DetalleCuenta> ObtenerDetallesPorIdPagado(int idCuenta)
    {
      return detalleRepo.FindByCuentaIdPaid(idCuenta);
    }
    public List<DetalleCuenta> ObtenerTodosLosDetallesNoPagados()
    {
      return detalleRepo.FindAllNotPaid();
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
