using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web;

namespace API_Catalogo.Models
{
    public class RegistroPeliculas
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public DateTime Año { get; set; }
        public string Director { get ; set; }
        public string Genero { get; set; }
        public byte[] Poster { get; set; }

        public DateTime AñoC
        {
            get { return Año.Date; }
            set { Año = value; }
        }
    }
}