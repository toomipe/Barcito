using barcito.Logica;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace barcito.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class DetalleController : ControllerBase
  {
    // GET: api/<DetalleCuenta>
    [HttpGet]
    public IEnumerable<string> Get()
    {
      Console.WriteLine("pasa la api");
      return new string[] { "value1", "value2" };
    }

    // GET api/<DetalleCuenta>/5
    [HttpGet("{idCuenta}")]
    public List<DetalleCuenta> Get(int idCuenta)
    {
      GestorDeDetalles gestorDeDetalles = new GestorDeDetalles();
      return gestorDeDetalles.ObtenerCuentaPorId(idCuenta);
    }

    // POST api/<DetalleCuenta>
    [HttpPost]
    public void Post([FromBody] DetalleCuenta detalle)
    {
      GestorDeDetalles gestorDeDetalles = new GestorDeDetalles();
      gestorDeDetalles.CrearDetalle(detalle);
    }

    // PUT api/<DetalleCuenta>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<DetalleCuenta>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
