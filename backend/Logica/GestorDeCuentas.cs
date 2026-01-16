using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace barcito.Logica
{
  public class GestorDeCuentas
  {
    private Persistencia.CuentaRepository cuentaRepo;
    private Persistencia.DetalleCuentaRepository detalleCuentaRepo;
    public GestorDeCuentas()
    {
      cuentaRepo = new Persistencia.CuentaRepository();
      detalleCuentaRepo = new Persistencia.DetalleCuentaRepository();
    }
    public int CrearCuenta(Cuenta cuentaData)
    {
      cuentaData.Fecha = DateTime.Now;
      cuentaData.Pagado = false;
      if (cuentaRepo.NotReplicated(cuentaData.Nombre, cuentaData.IdDevice))
      {
        return cuentaRepo.Save(cuentaData);
      }
      return cuentaRepo.FindIdByNombreYDispositivo(cuentaData.Nombre, cuentaData.IdDevice);
    }
    public Cuenta ObtenerCuentaPorId(int idCuenta)
    {
      return cuentaRepo.FindById(idCuenta);
    }
    public List<Cuenta> ObtenerTodasLasCuentas()
    {
      return cuentaRepo.FindAll();
    }

    public int ObtenerIdCuentaPorNombreYDispositivo(string nombre, string idDevice)
    {
      return cuentaRepo.FindIdByNombreYDispositivo(nombre, idDevice);
    }

    public string ObtenerCuentaYTotales()
    {
      var cuentas = cuentaRepo.FindAll();
      
      var result = cuentas.Select(cuenta => new
      {
        cuenta = new
        {
          cuenta.IdCuenta,
          cuenta.Nombre,
          cuenta.IdDevice,
          cuenta.Fecha,
          cuenta.Pagado,
          cuenta.Mesa
        },
        totales = new
        {
          total = detalleCuentaRepo.FindByCuentaId(cuenta.IdCuenta).Sum(d => d.Precio)
        }
      }).ToList();

      return JsonSerializer.Serialize(result);
    }
    
    public Cuenta? ObtenerCuentaPorDevice(string idDevice)
    {
      return cuentaRepo.FindByDevice(idDevice);
    }

    public async Task<bool> GenerarPagoEfectivo(List<int> detalles)
    {
      return await detalleCuentaRepo.GenerarPagoEfectivo(detalles);
    }

    public async Task<string> CrearPreferencia(List<DetalleCuenta> detalles)
    {
      using var client = new HttpClient();

      client.DefaultRequestHeaders.Authorization =
          new AuthenticationHeaderValue("Bearer", "APP_USR-3590866619117946-120321-e31e0ed04be7236b3a84c1619956b548-3038809371");

      /*
      var items = detalles.Select(d => new
      {
        title = d.Detalle ?? "Artículo",
        quantity = d.Cantidad,
        unit_price = d.Precio / d.Cantidad ?? 1,
        currency_id = "ARS"
      }).ToList(); 
      */

      var items = detalles.Select(d => new
      {
        title = "tubarcito",
        quantity = d.Cantidad,
        unit_price = 20,
        currency_id = "ARS"
      }).ToList();


      var body = new
      {
        items,
        back_urls = new
        {
          success = "https://4134zb58-4200.brs.devtunnels.ms/pagado/success",
          failure = "https://4134zb58-4200.brs.devtunnels.ms/pagado/failure",
          pending = "https://4134zb58-4200.brs.devtunnels.ms/pagado/pending"
        },
        auto_return = "approved",
        notification_url = "https://4134zb58-5196.brs.devtunnels.ms/api/pagos/webhook"
      };

      var json = JsonSerializer.Serialize(body);

      var content = new StringContent(json, Encoding.UTF8, "application/json");

      var response = await client.PostAsync(
          "https://api.mercadopago.com/checkout/preferences",
          content
      );

      response.EnsureSuccessStatusCode();

      var responseJson = await response.Content.ReadAsStringAsync();

      using var doc = JsonDocument.Parse(responseJson);
      
      return doc.RootElement.GetProperty("init_point").GetString()!;
    }

    internal object? MarcarPagado(int idCuenta)
    {
      return detalleCuentaRepo.MarkAsPaid(idCuenta);
    }
  }
}
