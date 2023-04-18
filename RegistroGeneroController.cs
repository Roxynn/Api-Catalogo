using API_Catalogo.Data;
using API_Catalogo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace API_Catalogo.Controllers
{
    public class RegistroGeneroController : ApiController
    {
        // GET 
        public List<RegistroGenero> Get()
        {
            return RegistroGeneroData.Listar();
        }

        // GET 
        public RegistroGenero Get(int id)
        {
            return RegistroGeneroData.ObtenerG(id);
        }

        // POST 
        public bool Post([FromBody] RegistroGenero generos)
        {
            return RegistroGeneroData.RegistrarG(generos);
        }

        // PUT 
        public bool Put([FromBody] RegistroGenero generos)
        {
            return RegistroGeneroData.ModificarG(generos);
        }

        // DELETE 
        public bool Delete(int id)
        {
            return RegistroGeneroData.EliminarG(id);
        }
    }
}