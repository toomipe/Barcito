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
        public List<DetalleCuenta> GetNoPagados(int idCuenta)
        {
            GestorDeDetalles gestorDeDetalles = new GestorDeDetalles();
            return gestorDeDetalles.ObtenerCuentaPorIdNoPagado(idCuenta);
        }

        [HttpGet("dos/{idCuenta}")]
        public List<DetalleCuentaDos> GetDetalleCuentaDos(int idCuenta)
        {
            GestorDeDetalles gestorDeDetalles = new GestorDeDetalles();
            return gestorDeDetalles.ObtenerDetallesNoPagadosDos(idCuenta);
        }

        // GET api/consultaCuenta/<DetalleCuenta>/5
        [HttpGet("consultaCuenta/{idCuenta}")]
        public List<DetalleCuentaCompleta> GetConsultaCuenta(int idCuenta)
        {
            GestorDeDetalles gestorDeDetalles = new GestorDeDetalles();
            return gestorDeDetalles.ObtenerDetallesPorCuenta(idCuenta);
        }

        // GET api/pagado/<DetalleCuenta>/5
        [HttpGet("pagado/{idCuenta}")]
        public List<DetalleCuenta> GetPagados(int idCuenta)
        {
            GestorDeDetalles gestorDeDetalles = new GestorDeDetalles();
            return gestorDeDetalles.ObtenerDetallesPorIdPagado(idCuenta);
        }

        [HttpGet("detallesEnPreparación")]
        public List<DetalleCuentaCompleta> GetDetallesEnPreparación()
        {
            GestorDeDetalles gestorDeDetalles = new GestorDeDetalles();
            return gestorDeDetalles.ObtenerDetallesEnPreparación();
        }

        [HttpGet("detallesParaEntregar")]
        public List<DetalleConCuenta> GetDetallesParaEntregar()
        {
            GestorDeDetalles gestorDeDetalles = new GestorDeDetalles();
            return gestorDeDetalles.ObtenerDetallesParaEntregar();
        }

        [HttpGet("DetallesPorMesa")]
        public List<DetalleConCuenta> GetDetallesPorMesa()
        {
            GestorDeDetalles gestorDeDetalles = new GestorDeDetalles();
            return gestorDeDetalles.ObtenerDetallesPorMesa();
        }

        [HttpGet("pagadoP/{idCuenta}")]
        public List<DetalleCuentaP> GetPagadosP(int idCuenta)
        {
            GestorDeDetalles gestorDeDetalles = new GestorDeDetalles();
            return gestorDeDetalles.ObtenerTodosLosDetallesPagadosPedidos(idCuenta);
        }

        [HttpGet("id/{idDetalleCuenta}")]
        public DetalleCuenta GetDetalle(int idDetalleCuenta)
        {
            GestorDeDetalles gestorDeDetalles = new GestorDeDetalles();
            return gestorDeDetalles.ObtenerDetallePorId(idDetalleCuenta);
        }

        // GET api/<DetalleCuenta>
        [HttpGet("{idCuenta}/{idArticulo}/{cantidad}")]
        public void Get(int idCuenta, int idArticulo, int cantidad)
        {
            GestorDeDetalles gestorDeDetalles = new GestorDeDetalles();
            gestorDeDetalles.CrearDetalle(idCuenta, idArticulo, cantidad);
        }

        [HttpGet("verDetalles")]
        public List<DetalleCuenta> VerDetalles()
        {
            GestorDeDetalles gestorDeDetalles = new GestorDeDetalles();
            return gestorDeDetalles.VerDetalles();
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

        [HttpPut("marcarPagado/{idDetalleCuenta}")]
        public IActionResult PutMarcarPagado(int idDetalleCuenta)
        {
            GestorDeDetalles gestor = new GestorDeDetalles();
            gestor.MarcarPagado(idDetalleCuenta);
            return NoContent();
        }

        [HttpPut("marcarPreparado/{idDetalleCuenta}")]
        public IActionResult PutMarcarPreparado(int idDetalleCuenta)
        {
            GestorDeDetalles gestor = new GestorDeDetalles();
            gestor.ActualizarEstadoDetalle(idDetalleCuenta, "PE");
            return NoContent();
        }

        [HttpPut("marcarEntregado/{idDetalleCuenta}")]
        public IActionResult PutMarcarEntregado(int idDetalleCuenta)
        {
            GestorDeDetalles gestor = new GestorDeDetalles();
            gestor.ActualizarEstadoDetalle(idDetalleCuenta, "E");
            return NoContent();
        }

        [HttpPut("marcarDevueltoCocina/{idDetalleCuenta}")]
        public IActionResult PutMarcarDevueltoCocina(int idDetalleCuenta)
        {
            GestorDeDetalles gestor = new GestorDeDetalles();
            gestor.ActualizarEstadoDetalle(idDetalleCuenta, "DC");
            return NoContent();
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
