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
    public void PostNuevaCuenta([FromBody] Cuenta cuenta)
    {
      Console.WriteLine("Nueva cuenta recibida: " + cuenta.Nombre + cuenta.IdDevice);
      GestorDeCuentas gestorDeCuentas = new GestorDeCuentas();
      gestorDeCuentas.CrearCuenta(cuenta);
    }

    // PUT api/<CuentaController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value) 
    {
    }

    // DELETE api/<CuentaController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
