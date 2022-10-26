using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Model;
using ProyectoFinal.Repository;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        [HttpPost("AgregarProducto")]
        public void AgregarProducto([FromBody] Producto pr)

        {
            ADO_Producto.AgregarProducto(pr);
        }

        [HttpDelete("EliminarProducto")]
        public void EliminarProducto([FromBody] int id)
        {
            ADO_Producto.EliminarProducto(id);
        }

        [HttpPut("ModificarProducto")]
        public void ModificarProducto([FromBody] Producto pr)
        {
            ADO_Producto.ModificarProducto(pr);
        }
    }
}


