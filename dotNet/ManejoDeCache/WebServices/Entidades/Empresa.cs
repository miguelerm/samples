using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServices.Entidades
{
    public class Empresa
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string[] Usuarios { get; set; }
    }
}