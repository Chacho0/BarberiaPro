using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace BarberiaPro.Controller
{
    [ApiController]
    [Route("api/pagos")]
    public class PagosController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public PagosController(IConfiguration configuration)
        {
            _configuration = configuration;
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"]; // Configura la clave secreta de Stripe
        }

        [HttpPost("crear-pago")]
        public async Task<IActionResult> CrearPago([FromBody] PagoRequest request)
        {
            try
            {
                // Crear un PaymentIntent con el monto dinámico
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)(request.Monto * 100), // Stripe espera el monto en centavos
                    Currency = "usd", // Cambia a la moneda que uses
                    PaymentMethodTypes = new List<string> { "card" }, // Métodos de pago permitidos
                    Metadata = new Dictionary<string, string>
                {
                    { "IdCitaProcesada", request.IdCitaProcesada.ToString() } // Metadatos adicionales
                }
                };

                var service = new PaymentIntentService();
                var paymentIntent = await service.CreateAsync(options);

                // Retornar el client_secret para que el frontend complete el pago
                return Ok(new { clientSecret = paymentIntent.ClientSecret });
            }
            catch (StripeException e)
            {
                return BadRequest(new { error = e.Message });
            }
        }
    }

    public class PagoRequest
    {
        public decimal Monto { get; set; } // Monto total a pagar
        public int IdCitaProcesada { get; set; } // ID de la cita procesada
    }
}