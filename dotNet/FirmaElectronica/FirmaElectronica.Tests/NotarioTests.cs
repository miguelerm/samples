using NUnit.Framework;
using System;
using System.IO;

namespace FirmaElectronica.Tests
{
	[TestFixture ()]
	public class NotarioTests
	{
		[Test ()]
		public void CertificarDocumento_CertificaDocumentosFirmados() {
			var recursos = Herramientas.GetResourcesPath ();
			var pathCertificado = Path.Combine (recursos, "certificado.pfx");
			var pathDocumento = Path.Combine (recursos, "documento-firmado.pdf");
			var certificado = new Certificado (pathCertificado);
			var notario = new Notario (certificado);

			var resultado = notario.CertificarDocumento (pathDocumento);

			Assert.IsTrue (resultado);
		}

		[Test ()]
		public void CertificarDocumento_NoCertificaDocumentosSinFirma() {
			var recursos = Herramientas.GetResourcesPath ();
			var pathCertificado = Path.Combine (recursos, "certificado.pfx");
			var pathDocumento = Path.Combine (recursos, "documento.pdf");
			var certificado = new Certificado (pathCertificado);
			var notario = new Notario (certificado);

			var resultado = notario.CertificarDocumento (pathDocumento);

			Assert.IsFalse (resultado);
		}
	}
}

