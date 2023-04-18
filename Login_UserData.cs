using API_Catalogo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace API_Catalogo.Data
{
    public class Login_UserData
    {
        public static bool RegistrarLU(Login_User login_User)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion.conexion))
            {
                SqlCommand cmd = new SqlCommand("usp_RegistrarUsuario", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Usuario", login_User.Usuario);
                cmd.Parameters.AddWithValue("@Contraseña", login_User.Contraseña);

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
        public static bool ModificarLU(Login_User login_User)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion.conexion))
            {
                SqlCommand cmd = new SqlCommand("usp_ModificarUsuario", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", login_User.id);
                cmd.Parameters.AddWithValue("@Usuario", login_User.Usuario);
                cmd.Parameters.AddWithValue("@Contraseña", login_User.Contraseña);
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


        public static List<Login_User> Listar()
        {
            List<Login_User> listadoLogin_User = new List<Login_User>();
            using (SqlConnection conexion = new SqlConnection(Conexion.conexion))
            {
                SqlCommand cmd = new SqlCommand("usp_ListarUsuarios", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    conexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            listadoLogin_User.Add(new Login_User()
                            {
                                id = Convert.ToInt32(dr["id"]),
                                Usuario= dr["Usuario"].ToString(),
                                Contraseña = dr["Contraseña"].ToString()

                            });
                        }
                    }

                    return listadoLogin_User;
                }
                catch (Exception ex)
                {
                    return listadoLogin_User;
                }
            }
        }
        public static Login_User ObtenerLU(int id)
        {
            Login_User login_User = new Login_User();

            using (SqlConnection conexion = new SqlConnection(Conexion.conexion))
            {
                SqlCommand cmd = new SqlCommand("usp_ObtenerUsuario", conexion);
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
                            login_User = new Login_User()
                            {
                                id = Convert.ToInt32(dr["id"]),
                                Usuario = dr["Usuario"].ToString(),
                                Contraseña = dr["Contraseña"].ToString()
                            };
                        }
                    }

                    return login_User;
                }
                catch (Exception ex)
                {
                    return login_User;
                }
            }
        }

        public static bool EliminarLU(int id)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion.conexion))
            {
                SqlCommand cmd = new SqlCommand("usp_EliminarUsuario", conexion);
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