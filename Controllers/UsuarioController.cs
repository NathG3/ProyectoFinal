﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Model;
using ProyectoFinal.Repository;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        //[HttpGet(Name = "GetUsuarios")]

        //[HttpGet]
        //public List<Usuario> Get()
        //{
        //    return ADO_Usuario.TraerUsuario();
        //}

        [HttpPut]
        public void Modificar([FromBody] Usuario us)
        {
            ADO_Usuario.ModificarUsuario(us);
        }

        [HttpPost]
        public void Agregar([FromBody] Usuario us)

        {
            ADO_Usuario.AgregarUsuario(us);
        }
    }
}

