using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace API_Catalogo.Data
{
    public class Conexion
    {
    private static string connectionString = "Data Source=.;Initial Catalog=catalogoDB;Integrated Security=True";

        public static string conexion { get; internal set; }

        public static void GuardarFavorito(int idUsuario, int idPelicula)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            // Preparar la consulta SQL
            string query = "INSERT INTO UsuariosPeliculasFavoritas (IdUsuario, IdPelicula) VALUES (@ID_User, @id_pelis)";
            SqlCommand command = new SqlCommand(query, connection);

            // Asignar los parámetros
            command.Parameters.AddWithValue("@ID_User", idUsuario);
            command.Parameters.AddWithValue("@id_pelis", idPelicula);

            // Ejecutar la consulta
            command.ExecuteNonQuery();
        }
    }
}
}