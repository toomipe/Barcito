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
    [HttpGet("{nombre}/{idDevice}")]
    public int Get(string nombre, string idDevice)
    {
      GestorDeCuentas gestorDeCuentas = new GestorDeCuentas();
      return gestorDeCuentas.ObtenerIdCuentaPorNombreYDispositivo(nombre, idDevice);
    }

    // POST api/<CuentaController>
    [HttpPost]
    public int PostNuevaCuenta([FromBody] Cuenta cuenta)
    {
      GestorDeCuentas gestorDeCuentas = new GestorDeCuentas();
      return gestorDeCuentas.CrearCuenta(cuenta);
    }
    // GET api/cuenta/device/{idDevice}
    [HttpGet("device/{idDevice}")]
    public ActionResult<Cuenta> GetByDevice(string idDevice)
    {
      GestorDeCuentas gestorDeCuentas = new GestorDeCuentas();

      var cuenta = gestorDeCuentas.ObtenerCuentaPorDevice(idDevice);

      if (cuenta == null)
        return NotFound();

      return Ok(cuenta);
    }

    // POST api/cuenta/device
    [HttpPost("device")]
    public ActionResult<int> CrearCuentaPorDevice([FromBody] Cuenta cuenta)
    {
      GestorDeCuentas gestorDeCuentas = new GestorDeCuentas();

      var idCuenta = gestorDeCuentas.CrearCuenta(cuenta);

      return Ok(idCuenta);
    }

    /*
    // POST api/<CuentaController>
    [HttpPost]
    public int PagarCuenta([FromBody] DetalleCuenta detallesCuenta[])
    {
        GestorDeCuentas gestorDeCuentas = new GestorDeCuentas();
        return gestorDeCuentas.CrearCuenta(cuenta);
    }*/
  }
}
