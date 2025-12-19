using Microsoft.AspNetCore.Mvc;
using barcito.Logica;
using barcito.Persistencia;

namespace barcito.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArticuloController : ControllerBase
{
  [HttpGet]
  public ActionResult<IEnumerable<Articulo>> GetAllArticulo()
  {
    ArticuloRepository articuloRepo = new ArticuloRepository();
    return Ok(articuloRepo.FindAll());
  }

  [HttpGet("{articuloId}")]
  public ActionResult<Articulo> GetArticuloByID(int articuloId)
  {
    ArticuloRepository articuloRepo = new ArticuloRepository();
    return articuloRepo.FindById(articuloId);
  }

  [HttpGet("porCategoria/{categoriaID}")]
  public ActionResult<Categoria> GetArticuloByCategoriaID(int categoriaID)
  {
    ArticuloRepository articuloRepo = new ArticuloRepository();
    var articu = articuloRepo.FindByCategoriaId(categoriaID);

    if (articu != null)
    {
      return Ok(articu);
    }

    return NotFound($"No se encontró la categoría con ID {categoriaID}.");
  }

}

