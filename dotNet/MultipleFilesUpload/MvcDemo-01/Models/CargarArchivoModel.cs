using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcDemo_01.Models
{
    public class CargarArchivoModel
    {
        public string Tipo { get; set; }
        public HttpPostedFileBase Archivo { get; set; }
    }
}