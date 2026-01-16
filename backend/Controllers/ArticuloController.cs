using Microsoft.AspNetCore.Mvc;
using barcito.Logica;
using barcito.Persistencia;

namespace barcito.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArticuloController : ControllerBase
{
    // GET: api/Articulo
    [HttpGet]
    public ActionResult<IEnumerable<Articulo>> GetAllArticulo()
    {
        ArticuloRepository articuloRepo = new ArticuloRepository();
        return Ok(articuloRepo.FindAll());
    }

    // GET: api/Articulo/5
    [HttpGet("{articuloId}")]
    public ActionResult<Articulo> GetArticuloByID(int articuloId)
    {
        ArticuloRepository articuloRepo = new ArticuloRepository();
        var articulo = articuloRepo.FindById(articuloId);

        if (articulo == null)
            return NotFound($"No se encontró el artículo con ID {articuloId}");

        return Ok(articulo);
    }

    // GET: api/Articulo/porCategoria/3
    [HttpGet("porCategoria/{categoriaID}")]
    public ActionResult<IEnumerable<Articulo>> GetArticuloByCategoriaID(int categoriaID)
    {
        ArticuloRepository articuloRepo = new ArticuloRepository();
        var articulos = articuloRepo.FindByCategoriaId(categoriaID);

        if (articulos == null || !articulos.Any())
            return NotFound($"No se encontraron artículos para la categoría {categoriaID}");

        return Ok(articulos);
    }

    // POST: api/Articulo
    [HttpPost]
    public ActionResult<Articulo> CreateArticulo([FromBody] Articulo articulo)
    {

        Console.WriteLine(articulo == null ? "Artículo es nulo" : "Artículo no es nulo");
        Console.WriteLine(articulo);
        
        if (articulo == null)
            return BadRequest("Artículo inválido");

        ArticuloRepository articuloRepo = new ArticuloRepository();
        articuloRepo.Save(articulo);

        return CreatedAtAction(
            nameof(GetArticuloByID),
            new { articuloId = articulo.ArticuloID },
            articulo
        );
    }

    // PUT: api/Articulo/5
    [HttpPut("{articuloId}")]
    public IActionResult UpdateArticulo(int articuloId, [FromBody] Articulo articulo)
    {
        if (articulo == null || articulo.ArticuloID != articuloId)
            return BadRequest("Datos inconsistentes");

        ArticuloRepository articuloRepo = new ArticuloRepository();
        var existente = articuloRepo.FindById(articuloId);

        if (existente == null)
            return NotFound($"No se encontró el artículo con ID {articuloId}");

        articuloRepo.Update(articulo);
        return NoContent();
    }

    // DELETE: api/Articulo/5
    [HttpDelete("{articuloId}")]
    public IActionResult DeleteArticulo(int articuloId)
    {
        ArticuloRepository articuloRepo = new ArticuloRepository();
        var existente = articuloRepo.FindById(articuloId);

        if (existente == null)
            return NotFound($"No se encontró el artículo con ID {articuloId}");

        articuloRepo.Delete(articuloId);
        return NoContent();
    }
}
