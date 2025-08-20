using barcito.Logica;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace barcito.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CuentaController : ControllerBase
  {
    // GET: api/<CuentaController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
      return new string[] { "value1", "value2" };
    }

    // GET api/<CuentaController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
      return "value";
    }

    // POST api/<CuentaController>
    [HttpPost]
    public int PostNuevaCuenta([FromBody] Cuenta cuenta)
    {
      GestorDeCuentas gestorDeCuentas = new GestorDeCuentas();
      return gestorDeCuentas.CrearCuenta(cuenta);
    }
  }
}
