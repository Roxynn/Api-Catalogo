using API_Catalogo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace API_Catalogo.Data
{
    public class RegistroGeneroData
    {
        public static bool RegistrarG(RegistroGenero generos)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion.conexion))
            {
                SqlCommand cmd = new SqlCommand("usp_RegistrarGenero", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Genero", generos.Genero);

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


        public static bool ModificarG(RegistroGenero generos)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion.conexion))
            {
                SqlCommand cmd = new SqlCommand("usp_ModificarGenero", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", generos.Id);
                cmd.Parameters.AddWithValue("@Genero", generos.Genero);
                

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


        public static List<RegistroGenero> Listar()
        {
            List<RegistroGenero> listadoGeneros = new List<RegistroGenero>();
            using (SqlConnection conexion = new SqlConnection(Conexion.conexion))
            {
                SqlCommand cmd = new SqlCommand("usp_ListarGeneros", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    conexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            listadoGeneros.Add(new RegistroGenero()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Genero = dr["Genero"].ToString()
                              
                            });
                        }
                    }

                    return listadoGeneros;
                }
                catch (Exception ex)
                {
                    return listadoGeneros;
                }
            }
        }
        public static RegistroGenero ObtenerG(int id)
        {
            RegistroGenero generos = new RegistroGenero();

            using (SqlConnection conexion = new SqlConnection(Conexion.conexion))
            {
                SqlCommand cmd = new SqlCommand("usp_ObtenerGenero", conexion);
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
                            generos = new RegistroGenero()
                            {
                                Id = Convert.ToInt32(dr["id"]),
                                Genero = dr["Genero"].ToString()

                            };
                        }
                    }

                    return generos;
                }
                catch (Exception ex)
                {
                    return generos;
                }
            }
        }

        public static bool EliminarG(int id)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion.conexion))
            {
                SqlCommand cmd = new SqlCommand("usp_EliminarGenero", conexion);
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