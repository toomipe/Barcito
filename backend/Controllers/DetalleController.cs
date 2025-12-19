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

        // GET api/<DetalleCuenta>
        [HttpGet("{idCuenta}/{idArticulo}/{cantidad}")]
        public void Get(int idCuenta, int idArticulo, int cantidad)
        {
            GestorDeDetalles gestorDeDetalles = new GestorDeDetalles();
            gestorDeDetalles.CrearDetalle(idCuenta, idArticulo, cantidad);
        }

        // POST api/<DetalleCuenta>
        [HttpPost]
        public void Post([FromBody] DetalleCuenta detalle)
        {
            GestorDeDetalles gestorDeDetalles = new GestorDeDetalles();
            gestorDeDetalles.CrearDetalle(detalle.IdCuenta, detalle.IdArticulo, detalle.Cantidad);
        }

        // POST api/Detalle/actualizar
        [HttpPost("actualizar")]
        public IActionResult PostActualizar([FromBody] DetalleUpdateDto dto)
        {
            Console.WriteLine(
                $"POST Detalle → id: {dto.IdDetalleCuenta}, detalle: {dto.Detalle}, cantidad: {dto.Cantidad}"
            );

            GestorDeDetalles gestor = new GestorDeDetalles();
            gestor.ActualizarDetalle(
                dto.IdDetalleCuenta,
                dto.Detalle ?? "",
                dto.Cantidad
            );

            return Ok();
        }


        // DELETE api/Detalle/5
        [HttpDelete("{idDetalle}")]
        public IActionResult Delete(int idDetalle)
        {
            GestorDeDetalles gestor = new GestorDeDetalles();
            gestor.EliminarDetalle(idDetalle);
            return NoContent();
        }
            
    }
}
