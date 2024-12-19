using SiAB.Application.Contracts;
using SiAB.Core.Exceptions;
using SiAB.Core.Models.JCE;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Infrastructure.Repositories.JCE
{
    public class JCERepository : IJCERepository
	{
		public JCEResult GetInfoCivilByCedula(string cedula)
		{
			try
			{
				var cedulaArray = cedula.Trim().Split('-');

				string URLPrefix = "https://dataportal.jce.gob.do";
				string ServiceID = "2fc26698-1dae-4d0a-8914-768c4eae967a";
				string Mun_ced = cedulaArray[0];
				string Seq_ced = cedulaArray[1];
				string Ver_ced = cedulaArray[2];

				string URL = string.Format("{4}/idcons/IndividualDataHandler.aspx?ServiceID={0}&ID1={1}&ID2={2}&ID3={3}",
					ServiceID,
					Mun_ced,
					Seq_ced,
					Ver_ced,
					URLPrefix);

				System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

				System.Net.HttpWebRequest objRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(URL);
				objRequest.Method = "GET";
				//because server is protected from hackers attacks, we have to define UserAgent like web browser
				objRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";
				System.Net.HttpWebResponse objResponse = (System.Net.HttpWebResponse)objRequest.GetResponse();
				System.IO.Stream dataStream = objResponse.GetResponseStream();
				System.IO.StreamReader reader = new System.IO.StreamReader(dataStream);
				string XMLResponse = reader.ReadToEnd();

				reader.Close();
				dataStream.Close();
				objResponse.Close();

				System.Xml.XmlDocument objDoc = new System.Xml.XmlDocument();
				objDoc.LoadXml(XMLResponse);

				StringBuilder sb = new StringBuilder();

				string nombres = "";
				string apellido1 = "";
				string apellido2 = "";
				string fecha_nac = "";
				string lugar_nac = "";
				string sexo = "";
				string est_civil = "";
				byte[] fotobase64 = null;

				foreach (System.Xml.XmlElement objElement in objDoc.DocumentElement.ChildNodes)
				{
					sb.AppendFormat("{0} = {1}\r\n", objElement.Name, objElement.InnerText);

					switch (objElement.Name.ToLowerInvariant())
					{
						case "nombres":
							nombres = objElement.InnerText;
							break;
						case "apellido1":
							apellido1 = objElement.InnerText;
							break;
						case "apellido2":
							apellido2 = objElement.InnerText;
							break;
						case "fecha_nac":
							var tmp_fecha = objElement.InnerText.Trim().Split(' ');
							fecha_nac = tmp_fecha[0];
							break;
						case "lugar_nac":
							lugar_nac = objElement.InnerText;
							break;
						case "sexo":
							sexo = objElement.InnerText;
							break;
						case "est_civil":
							est_civil = objElement.InnerText;
							break;
					}

					if (objElement.Name.ToLowerInvariant() == "fotourl")
					{
						string FotoURL = string.Format("{0}{1}", URLPrefix, objElement.InnerText);
						objRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(FotoURL);
						objRequest.Method = "GET";
						//because server protected from the hackers attacks, we have to define UserAgent like web browser
						objRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";

						objResponse = (System.Net.HttpWebResponse)objRequest.GetResponse();
						dataStream = objResponse.GetResponseStream();
						Bitmap bImage = new Bitmap(dataStream);

						System.IO.MemoryStream ms = new MemoryStream();
						bImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
						byte[] byteImage = ms.ToArray();
						fotobase64 = byteImage; /*Convert.ToBase64String(byteImage);*/
					}
				}

				if (nombres != "")
				{
					JCEResult jCEResult = new JCEResult
					{
						Foto = System.Convert.ToBase64String(fotobase64),
						Cedula = cedula.Replace("-",""),
						NombreCompleto = String.Concat(nombres, " ", apellido1, " ", apellido2),
						Sexo = sexo,
					};

					return jCEResult;
				}
				else
				{
					return new JCEResult();
				}
			}
			catch (Exception)
			{
				throw new BaseException("No se encontraron coincidencias.", HttpStatusCode.NotFound);
			}
		}
	}
}
