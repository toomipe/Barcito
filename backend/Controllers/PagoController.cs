using Microsoft.AspNetCore.Mvc;
using System.Linq;
using barcito.Logica;

namespace barcito.Controllers
{
    [ApiController]
    [Route("api/Pago")]
    public class PagosController : ControllerBase
    {
        private readonly GestorDeCuentas _mp;

        public PagosController(GestorDeCuentas mp)
        {
            _mp = mp;
        }

        [HttpPost("CrearPreferencia")]
        public async Task<IActionResult> CrearPreferencia([FromBody] CrearPagoRequest request)
        {
            if (request.Detalles == null || !request.Detalles.Any())
                return BadRequest("No hay detalles para pagar");

            var url = await _mp.CrearPreferencia(request.Detalles);

            return Ok(new { url });
        }

        [HttpPost("Efectivo")]
        public async Task<IActionResult> Efectivo([FromBody] List<int> Detalles)
        {
            Console.WriteLine("Iniciando pago en efectivo...", Detalles);
            if (Detalles == null)
                return BadRequest("No hay detalles para pagar");

            await _mp.GenerarPagoEfectivo(Detalles);

            return Ok();
        }

    }

    [ApiController]
    [Route("api/pagos/webhook")]
    public class MercadoPagoWebhookController : ControllerBase
    {
        [HttpPost]
        public IActionResult RecibirNotificacion([FromBody] object data)
        {
            // Por ahora solo logueamos
            Console.WriteLine("Webhook Mercado Pago recibido:");
            Console.WriteLine(data?.ToString());

            // MP necesita SIEMPRE 200 OK
            return Ok();
        }
    }
}
