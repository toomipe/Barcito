namespace barcito.Logica
{
  public class Cuenta
  {
    public int IdCuenta { get; set; }
    public string Nombre { get; set; } = null!;
    public DateTime Fecha { get; set; }
    public string IdDevice { get; set; } = null!;
    public bool Pagado { get; set; } = true;  
  }

}
