using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_Catalogo.Models
{
    public class RegistroUser
    {
        public int id {  get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string User { get; set; }
        public string Contraseña { get; set; }

    }
}