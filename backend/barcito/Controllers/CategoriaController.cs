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
        var catego = categoriaRepo.FindById(categoriaId);

        if (catego != null)
        {
            return Ok(catego);
        }

        return NotFound($"No se encontró la categoría con ID {categoriaId}.");
    }


}

