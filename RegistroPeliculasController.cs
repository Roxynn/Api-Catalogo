using API_Catalogo.Data;
using API_Catalogo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace API_Catalogo.Controllers
{
    public class RegistroPeliculasController : ApiController
    {
      
            
    // GET 
    public List<RegistroPeliculas> Get()
        {
            // Obtiene la lista de todas las películas registradas en la base de datos
            return RegistroPeliculasData.Listar();
        }

        // GET 
        public RegistroPeliculas Get(int id)
        {
            // Obtiene la información de la película con el ID especificado
            return RegistroPeliculasData.ObtenerP(id);
        }

        // POST 
        public bool Post([FromBody] RegistroPeliculas peliculas)
        {
            // registra una nueva película en la base de datos
            return RegistroPeliculasData.RegistrarP(peliculas);
        }

        // PUT 
        public bool Put([FromBody] RegistroPeliculas peliculas)
        {

            //Actualiza la información de la película con el ID especificado
            return RegistroPeliculasData.ModificarP(peliculas);
        }

        // DELETE 
        public bool Delete(int id)
        {

            // Elimina la película con el ID especificado de la base de datos
            return RegistroPeliculasData.EliminarP(id);
        }
        [HttpPost]
        [Route("RegistroPeliculas/{id}")]
        public IHttpActionResult ObtenerPelicula(int id)
        {
            try
            {

                RegistroPeliculas pelicula = RegistroPeliculasData.ObtenerP(id);

                if (pelicula == null)
                {

                    return NotFound();
                }
                else
                {

                    return Ok(pelicula);
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        private IHttpActionResult StatusCode(int v, string message)
        {
            throw new NotImplementedException();
        }
      

        [HttpPost]
        [Route("usuario/{idUsuario}/{idPelicula}/peliculas-favoritas")]
        public IHttpActionResult GuardarPeliculaFavorita(int idUsuario, int idPelicula)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.conexion))
                {
                    conexion.Open();

                    // Crear el comando SQL para insertar el registro en la tabla UsuarioPeliculaFavorita
                    string query = "INSERT INTO UsuariosPeliculasFavoritas (IdUsuario, IdPelicula) VALUES (@idUsuario, @idPelicula)";
                    SqlCommand comando = new SqlCommand(query, conexion);
                    comando.Parameters.AddWithValue("@idUsuario", idUsuario);
                    comando.Parameters.AddWithValue("@idPelicula", idPelicula);

                    int filasAfectadas = comando.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        return Ok("Pelicula guardada como favorita para el usuario con ID " + idUsuario);
                    }
                    else
                    {
                        return BadRequest("No se pudo guardar la pelicula como favorita para el usuario con ID " + idUsuario);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("RegistroPeliculas/{idUsuario}/peliculas-favoritas")]
        public IHttpActionResult GetPeliculasFavoritas(int IdUsuario)
        {
            try
            {
                List<RegistroPeliculas> peliculas = new List<RegistroPeliculas>();

                using (SqlConnection conexion = new SqlConnection(Conexion.conexion))
                {
                    conexion.Open();

                    string query = "SELECT p.* FROM RegistroPeliculas p INNER JOIN UsuariosPeliculasFavoritas uf ON p.Id = uf.IdPelicula WHERE uf.IdUsuario = @userId";
                    SqlCommand comando = new SqlCommand(query, conexion);
                    comando.Parameters.AddWithValue("@IdUsuario", IdUsuario);

                    SqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        RegistroPeliculas pelicula = new RegistroPeliculas();
                        pelicula.Id = Convert.ToInt32(reader["Id"]);
                        pelicula.Titulo = reader["Titulo"].ToString();
                        pelicula.Genero = reader["Genero"].ToString();
                        pelicula.Director = reader["Director"].ToString();
                        pelicula.AñoC = Convert.ToDateTime(reader["Año"]);
                        pelicula.Poster = (byte[])reader["Poster"];

                        peliculas.Add(pelicula);

                        reader.Close();
                    }

                    return Ok(peliculas);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("movies/user/{userId}/{peliculaId}/favoritos")]
        public IHttpActionResult EliminarPeliculaFavorita(int userId, int peliculaId)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.conexion))
                {
                    conexion.Open();

                    // Crear el comando SQL para eliminar el registro correspondiente de la tabla UsuarioPeliculaFavorita
                    string query = "DELETE FROM UsuariosPeliculasFavoritas WHERE IdUsuario = @IdUsuario AND IdPelicula = @IdPelicula";
                    SqlCommand comando = new SqlCommand(query, conexion);
                    comando.Parameters.AddWithValue("@IdUsuario", userId);
                    comando.Parameters.AddWithValue("@IdPelicula", peliculaId);

                    int filasAfectadas = comando.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        return Ok("Pelicula eliminada de la lista de favoritos para el usuario con ID " + userId);
                    }
                    else
                    {
                        return BadRequest("No se pudo eliminar la pelicula de la lista de favoritos para el usuario con ID " + userId);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

      
    }
}