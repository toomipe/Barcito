using Microsoft.AspNetCore.Mvc;
using barcito.Logica;
using barcito.Persistencia;

namespace barcito.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriaController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Categoria>> GetAllCategorias()
    {
        CategoriaRepository categoriaRepo = new CategoriaRepository();
        return Ok(categoriaRepo.FindAll());
    }

    [HttpGet("{categoriaId}")]
    public ActionResult<Categoria> GetCategoriaByID(int categoriaId)
    {
        CategoriaRepository categoriaRepo = new CategoriaRepository();
        var categoria = categoriaRepo.FindById(categoriaId);

        if (categoria == null)
            return NotFound($"No se encontró la categoría con ID {categoriaId}.");

        return Ok(categoria);
    }

    [HttpPost]
    public ActionResult CreateCategoria([FromBody] Categoria categoria)
    {
        if (categoria == null)
            return BadRequest("La categoría es inválida.");

        if (string.IsNullOrWhiteSpace(categoria.Nombre))
            return BadRequest("El nombre de la categoría es obligatorio.");

        CategoriaRepository categoriaRepo = new CategoriaRepository();
        categoriaRepo.Save(categoria);

        return CreatedAtAction(
            nameof(GetCategoriaByID),
            new { categoriaId = categoria.CategoriaID },
            categoria
        );
    }

    [HttpPut("{categoriaId}")]
    public ActionResult UpdateCategoria(int categoriaId, [FromBody] Categoria categoria)
    {
        if (categoria == null)
            return BadRequest("La categoría es inválida.");

        if (categoriaId != categoria.CategoriaID)
            return BadRequest("El ID de la URL no coincide con el cuerpo.");

        CategoriaRepository categoriaRepo = new CategoriaRepository();
        var existente = categoriaRepo.FindById(categoriaId);

        if (existente == null)
            return NotFound($"No se encontró la categoría con ID {categoriaId}.");

        categoriaRepo.Update(categoria);
        return NoContent(); 
    }

    [HttpDelete("{categoriaId}")]
    public ActionResult DeleteCategoria(int categoriaId)
    {
        CategoriaRepository categoriaRepo = new CategoriaRepository();
        var existente = categoriaRepo.FindById(categoriaId);

        if (existente == null)
            return NotFound($"No se encontró la categoría con ID {categoriaId}.");

        categoriaRepo.Delete(categoriaId);
        return NoContent(); 
    }
}
