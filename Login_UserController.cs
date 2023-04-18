using API_Catalogo.Data;
using API_Catalogo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Data.SqlClient;

namespace API_Catalogo.Controllers
{
    public class Login_UserController : ApiController
    {    // GET 
        public List<Login_User> Get()
        {

            return Login_UserData.Listar();
        }

        // GET 
        public Login_User Get(int id)
        {
            return Login_UserData.ObtenerLU(id);
        }

        // POST 
        public bool Post([FromBody] Login_User login_User)
        {
            return Login_UserData.RegistrarLU(login_User);
        }

        // PUT 
        public bool Put([FromBody] Login_User login_User)
        {
            return Login_UserData.ModificarLU(login_User);
        }

        // DELETE 
        public bool Delete(int id)
        {
            return Login_UserData.EliminarLU(id);
        }
        [HttpGet]
        [Route("authenticate")]
        public Login_User Authenticate(string username, string password)
        {
            string connectionString = "Data Source=.;Initial Catalog=catalogoDB;Integrated Security=True";

            Login_User authenticatedUser = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Login_User WHERE Usuario = @Username AND Contraseña = @Password";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        authenticatedUser = new Login_User
                        {
                            id = (int)reader["Id"],
                            Usuario = (string)reader["Username"],
                            Contraseña = (string)reader["Password"]
                        };
                    }
                }
            }

            // Devolver el usuario autenticado, si se encontró uno; de lo contrario, devolver null
            return authenticatedUser;
        }

    }
}