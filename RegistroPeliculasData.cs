using API_Catalogo.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace API_Catalogo.Data
{

    public class RegistroPeliculasData
    {
        public static bool RegistrarP(RegistroPeliculas peliculas)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion.conexion))
            {
                SqlCommand cmd = new SqlCommand("usp_RegistrarPelicula", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Titulo", peliculas.Titulo);
                cmd.Parameters.AddWithValue("@Año", peliculas.AñoC.Date);
                cmd.Parameters.AddWithValue("@Director", peliculas.Director);
                cmd.Parameters.AddWithValue("@Genero", peliculas.Genero);
                cmd.Parameters.AddWithValue("@Poster", peliculas.Poster);

                try
                {
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        
            public static bool ModificarP(RegistroPeliculas peliculas)
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("usp_ModificarPelicula", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", peliculas.Id);
                    cmd.Parameters.AddWithValue("@Titulo", peliculas.Titulo);
                    cmd.Parameters.AddWithValue("@Año", peliculas.AñoC.Date); ;
                    cmd.Parameters.AddWithValue("@Director", peliculas.Director);
                    cmd.Parameters.AddWithValue("@Genero", peliculas.Genero);
                    cmd.Parameters.AddWithValue("@Poster", peliculas.Poster);

                    try
                    {
                        conexion.Open();
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }
        

        public static List<RegistroPeliculas> Listar()
        {
            List<RegistroPeliculas> listadoPeliculas = new List<RegistroPeliculas>();
            using (SqlConnection conexion = new SqlConnection(Conexion.conexion))
            {
                SqlCommand cmd = new SqlCommand("usp_ListarPeliculas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    conexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            listadoPeliculas.Add(new RegistroPeliculas()
                            {
                                Id = Convert.ToInt32(dr["id"]),
                                Titulo = dr["Titulo"].ToString(),
                                AñoC = Convert.ToDateTime(dr["Año"].ToString()),
                                Director = dr["Director"].ToString(),
                                Genero = dr["Genero"].ToString(),
                                Poster = (byte[])dr["Poster"]
                            });
                        }
                    }

                    return listadoPeliculas;
                }
                catch (Exception ex)
                {
                    return listadoPeliculas;
                }
            }
        }
        public static RegistroPeliculas ObtenerP(int id)
        {
            RegistroPeliculas pelicula = new RegistroPeliculas();

            using (SqlConnection conexion = new SqlConnection(Conexion.conexion))
            {
                SqlCommand cmd = new SqlCommand("usp_ObtenerPelicula", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);

                try
                {
                    conexion.Open();
                    cmd.ExecuteNonQuery();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            pelicula = new RegistroPeliculas()
                            {
                                Id = Convert.ToInt32(dr["id"]),
                                Titulo = dr["Titulo"].ToString(),
                                AñoC = Convert.ToDateTime(dr["Año"].ToString()),
                                Director = dr["Director"].ToString(),
                                Genero = dr["Genero"].ToString(),
                                Poster = (byte[])dr["Poster"]
                            };
                        }
                    }

                    return pelicula;
                }
                catch (Exception ex)
                {
                    return pelicula;
                }
            }
        }

        public static bool EliminarP(int id)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion.conexion))
            {
                SqlCommand cmd = new SqlCommand("usp_EliminarPelicula", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);

                try
                {
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
    }
}