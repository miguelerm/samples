using System;
using System.Diagnostics;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.security;
using Org.BouncyCastle.Security;

namespace FirmaElectronica
{
	public class Notario
	{
		private readonly Certificado certificado;

		public Notario (Certificado certificado)
		{
			this.certificado = certificado;
		}

		public bool CertificarDocumento (string rutaDocumentoFirmado)
		{
			using (var reader = new PdfReader (rutaDocumentoFirmado)) {
				var campos = reader.AcroFields;
				var nombresDefirmas = campos.GetSignatureNames ();
				foreach (var nombre in nombresDefirmas) {

					// Solo se verificará la última revision del documento.
					if (campos.GetRevision (nombre) != campos.TotalRevisions)
						continue;

					// Solo se verificará si la firma es de todo el documento.
					if (!campos.SignatureCoversWholeDocument (nombre))
						continue;

					var firma = campos.VerifySignature (nombre);

					if (!firma.Verify ())
						continue;

					foreach (var certificadoDocumento in firma.Certificates) {

						foreach (var certificadoDeConfianza in certificado.Chain) {
							try {
								certificadoDocumento.Verify (certificadoDeConfianza.GetPublicKey ());
								// Si llega hasta aquí, es porque la última firma fue realizada con el certificado del sistema.
								return true;
							} catch (InvalidKeyException) {
								continue;
							} catch (Exception ex) {
								Trace.TraceError ("Error: {0}", ex);
								continue;
							}
						}
					}
				}
			}

			return false;
		}
	}
}

