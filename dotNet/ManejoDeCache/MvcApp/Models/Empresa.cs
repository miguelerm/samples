using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApp.Models
{
    public class Empresa
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string[] Usuarios { get; set; }
    }
}