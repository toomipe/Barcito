




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

    public List<DetalleCuentaP> ObtenerTodosLosDetallesPagadosPedidos(int idCuenta)
    {
      return detalleRepo.FindAllPaidOrders(idCuenta);
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

    internal List<DetalleCuentaCompleta> ObtenerDetallesPorCuenta(int idCuenta)
    {
        return detalleRepo.FindDetallesByCuentaId(idCuenta);
    }

    internal void MarcarPagado(int idDetalleCuenta)
    {
        detalleRepo.MarkAsPaid(idDetalleCuenta);
    }

    internal List<DetalleCuenta> VerDetalles()
    {
        return detalleRepo.FindAll();
    }

    internal List<DetalleCuentaDos> ObtenerDetallesNoPagadosDos(int idCuenta)
    {
        return detalleRepo.FindDetallesNoPagadosDos(idCuenta);
    }

    internal DetalleCuenta ObtenerDetallePorId(int idDetalleCuenta)
    {
        return detalleRepo.FindById(idDetalleCuenta);
    }

    internal List<DetalleCuentaCompleta> ObtenerDetallesEnPreparación()
    {
        return detalleRepo.FindDetallesEnPreparación();
    }

    internal void ActualizarEstadoDetalle(int idDetalleCuenta, string nuevoEstado)
    {
        detalleRepo.UpdateEstado(idDetalleCuenta, nuevoEstado);
    }

    internal List<DetalleConCuenta> ObtenerDetallesParaEntregar()
    {
        return detalleRepo.FindDetallesParaEntregar();
    }

    internal List<DetalleConCuenta> ObtenerDetallesPorMesa()
    {
        return detalleRepo.FindDetallesPorMesa();
    }
  }
  
}