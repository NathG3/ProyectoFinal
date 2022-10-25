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
        [HttpPost]
        public void AgregarProducto([FromBody] Producto pr)

        {
            ADO_Producto.AgregarProducto(pr);
        }

        [HttpDelete]
        public void EliminarProducto([FromBody] int id)
        {
            ADO_Producto.EliminarProducto(id);
        }

        [HttpPut]
        public void ModificarProducto([FromBody] Producto prd)
        {
            //ADO_Producto.ModificarProducto(prd);
        }
    }
}


