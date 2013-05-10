using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace MvcApplication.Extensions
{
    public static class MvcHtmlStringExtensions
    {
        public static MvcHtmlString AddInputPrefix(this MvcHtmlString htmlString, string prefix)
        {
            // Estos son los nuevos formatos para los valores de los atributos "id" y "name"
            // de los inputs cuando tienen que ser colecciones o arreglos,
            // La idea es que el formato para los ids sea Propiedad_Indice_SubPropiedad y
            // que el formato para los names sea Propiedad[Indice].SubPropiedad.

            var newIdFormat = "$1=\"" + prefix + "_${index}_$2\"";
            var newNameFormat = "name=\"" + prefix + "[${index}].$1\"";

            var regexOptions = RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled;

            // idAttributeRegex para buscar textos que sean id="algo" o data-valmsg-for="algo",
            // nameAttributeRegex para buscar textos que sean name="algo".
            var idAttributeRegex = new Regex("(id|data-valmsg-for)=\"(.+?)\"", regexOptions);
            var nameAttributeRegex = new Regex("name=\"(.+?)\"", regexOptions);

            // Se remplazan los ids y names originales por el nuevo formato deseado.
            var html = htmlString.ToString();
            html = idAttributeRegex.Replace(html, newIdFormat);
            html = nameAttributeRegex.Replace(html, newNameFormat);

            // Si existe el atributo name="Propiedad[Indice].Index" se remplaza por el atributo
            // name="Propiedad[].Index"
            html = html.Replace("name=\"" + prefix + "[${index}].Index\"", "name=\"" + prefix + "[].Index\"");

            return new MvcHtmlString(html);
        }
    }
}