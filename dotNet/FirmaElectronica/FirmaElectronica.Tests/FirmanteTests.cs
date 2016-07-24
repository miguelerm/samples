using NUnit.Framework;
using System;
using System.IO;

namespace FirmaElectronica.Tests
{
	[TestFixture ()]
	public class FirmanteTests
	{
		[Test ()]
		public void Firmar_FirmaCorrectaDelDocumento ()
		{
			var recursos = Herramientas.GetResourcesPath ();
			var pathCertificado = Path.Combine (recursos, "certificado.pfx");
			var pathDocumentoSinFirma = Path.Combine (recursos, "documento.pdf");
			var pathDocumentoConFirma = Path.Combine (recursos, "documento-con-firma-test.pdf");
			var certificado = new Certificado (pathCertificado);
			var firmante = new Firmante (certificado);

			firmante.Firmar (pathDocumentoSinFirma, pathDocumentoConFirma);

			Assert.IsTrue (File.Exists (pathDocumentoConFirma));
		}


	}
}

