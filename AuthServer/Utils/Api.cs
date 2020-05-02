using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Utils
{
	public class Api
	{
		public static void MakeRequest(string url, string data)
		{
			var targetUri = new Uri($"{url}");
			var request = WebRequest.Create(targetUri);
			request.Method = "POST";


			byte[] byteArray = Encoding.UTF8.GetBytes(data);
			request.ContentType = "application/json";
			request.ContentLength = byteArray.Length;

			Stream dataStream = request.GetRequestStream();
			dataStream.Write(byteArray, 0, byteArray.Length);
			dataStream.Close();

			var webRequestResponse = request.GetResponse();
		}
	}
}
