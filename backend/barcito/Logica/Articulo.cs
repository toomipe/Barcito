namespace barcito.Logica
{
  public class Articulo
  {
    public int ArticuloID { get; set; }
    public string Nombre { get; set; }
    public string UrlImagen { get; set; }
    public string Descripcion { get; set; }
    public decimal Precio { get; set; }

    // Relación con la categoría
    public int CategoriaID { get; set; }
    public required Categoria Categoria { get; set; }
  }
}
