using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Model;
using ProyectoFinal.Repository;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        [HttpPost("CargarVenta")]
        public void CargarVenta([FromBody] List<Producto> pv, int idUsuario)

        {
            ADO_Venta.CargarVenta(pv, idUsuario);
        }
    }
}


