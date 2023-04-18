using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_Catalogo.Models
{
    public class Login_User
    {
        public int id { get; set; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
    }
}