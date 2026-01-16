namespace barcito.Logica
{
  public class DetalleCuentaCompleta
  {
    public int IdDetalleCuenta { get; set; }

    public int IdCuenta { get; set; }
    public Cuenta? Cuenta { get; set; }

    public int IdArticulo { get; set; }
    public bool? Pagado { get; set; } = false;
    public int Cantidad { get; set; }
    public string? Detalle { get; set; }
    public decimal? Precio { get; set; }
    public string? Estado { get; set; }

    public string? NombreArticulo { get; set; }

    public string? DescripcionArticulo { get; set; }

    public string? DetalleArticulo { get; set; }

    public bool? formaDePago { get; set; }
  }
}
