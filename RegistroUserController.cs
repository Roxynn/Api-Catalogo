using API_Catalogo.Data;
using API_Catalogo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace API_Catalogo.Controllers
{
    public class RegistroUserController : ApiController
    {
        // GET 
        public List<RegistroUser> Get()
        {
            return RegistroUserData.Listar();
        }

        // GET 
        public RegistroUser Get(int id)
        {
            return RegistroUserData.ObtenerRU(id);
        }

        // POST 
        public bool Post([FromBody] RegistroUser registroUser)
        {
            return RegistroUserData.RegistrarRU(registroUser);
        }

        // PUT 
        public bool Put([FromBody] RegistroUser registroUser)
        {
            return RegistroUserData.ModificarRU(registroUser);
        }

        // DELETE 
        public bool Delete(int id)
        {
            return RegistroUserData.EliminarRU(id);
        }
        [HttpPost]
        [Route("registro")]
        public IHttpActionResult RegistroUser([FromBody] RegistroUser usuario)
        {
            try
            {
                
                string connectionString = "Data Source=.;Initial Catalog=catalogoDB;Integrated Security=True";

               
                string query = "INSERT INTO RegistroUser (User contraseña) VALUES (@User, @contraseña)";

              
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        
                        command.Parameters.AddWithValue("@User", usuario.User);
                        command.Parameters.AddWithValue("@Contraseña", usuario.Contraseña);
            

                        
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }

                return Ok("Usuario registrado exitosamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}

  