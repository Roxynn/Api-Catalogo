using API_Catalogo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace API_Catalogo.Data
{
    public class RegistroUserData
    {
        public static bool RegistrarRU(RegistroUser registroUser)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion.conexion))
            {
                SqlCommand cmd = new SqlCommand("usp_RRegistroUser", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", registroUser.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", registroUser.Apellido);
                cmd.Parameters.AddWithValue("@Usuario", registroUser.User);
                cmd.Parameters.AddWithValue("@Contraseña", registroUser.Contraseña);
               
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
        public static bool ModificarRU(RegistroUser registroUser)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion.conexion))
            {
                SqlCommand cmd = new SqlCommand("usp_ModificarRegistroUser", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", registroUser.id);
                cmd.Parameters.AddWithValue("@Nombre", registroUser.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", registroUser.Apellido);
                cmd.Parameters.AddWithValue("@Usuario", registroUser.User);
                cmd.Parameters.AddWithValue("@Contraseña", registroUser.Contraseña);
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


        public static List<RegistroUser> Listar()
        {
            List<RegistroUser> listadoRegistroUser = new List<RegistroUser>();
            using (SqlConnection conexion = new SqlConnection(Conexion.conexion))
            {
                SqlCommand cmd = new SqlCommand(" usp_ListarRegistroUsers", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    conexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            listadoRegistroUser.Add(new RegistroUser()
                            {
                                id = Convert.ToInt32(dr["id"]),
                                Nombre = dr["Nombre"].ToString(),
                                Apellido = dr["Apellido"].ToString(),
                                User = dr["Usuario"].ToString(),
                                Contraseña = dr["Contraseña"].ToString()
                             
                            });
                        }
                    }

                    return listadoRegistroUser;
                }
                catch (Exception ex)
                {
                    return listadoRegistroUser;
                }
            }
        }
        public static RegistroUser ObtenerRU(int id)
        {
            RegistroUser registroUser = new RegistroUser();

            using (SqlConnection conexion = new SqlConnection(Conexion.conexion))
            {
                SqlCommand cmd = new SqlCommand("usp_ObtenerRegistroUser", conexion);
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
                            registroUser = new RegistroUser()
                            {
                                id = Convert.ToInt32(dr["id"]),
                                Nombre = dr["Nombre"].ToString(),
                                Apellido = dr["Apellido"].ToString(),
                                User = dr["Usuario"].ToString(),
                                Contraseña = dr["Contraseña"].ToString()
                            };
                        }
                    }

                    return registroUser;
                }
                catch (Exception ex)
                {
                    return registroUser;
                }
            }
        }

        public static bool EliminarRU(int id)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion.conexion))
            {
                SqlCommand cmd = new SqlCommand("usp_EliminarRegistroUser", conexion);
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