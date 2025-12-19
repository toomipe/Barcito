namespace barcito.Logica
{
  public class DetalleCuenta
  {
    public int IdDetalleCuenta { get; set; }

    public int IdCuenta { get; set; }
    public Cuenta? Cuenta { get; set; }

    public int IdArticulo { get; set; }
    public bool? Pagado { get; set; } = false;
    public int Cantidad { get; set; }
    public string? Detalle { get; set; }
    public decimal? Precio { get; set; }
  }
}
