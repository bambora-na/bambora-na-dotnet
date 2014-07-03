using System;
using System.IO;
using System.Net;
using System.Text;
using Beanstream.Entities;
using Beanstream.Web;
using Newtonsoft.Json;

namespace Beanstream
{
	public class ExecuteWebRequest : IWebCommandSpec<string>
	{
		private readonly RequestObject _requestObject;

		public ExecuteWebRequest(RequestObject requestObject)
		{
			Url = requestObject.Url;
			_requestObject = requestObject;
		}

		public Uri Url { get; private set; }

		public void PrepareRequest(WebRequest request)
		{
			var httpRequest = request as HttpWebRequest;

			if (httpRequest == null)
			{
				throw new InvalidOperationException("URL AuthType not supported: " + Url.Scheme);
			}

			httpRequest.Method = _requestObject.Method.ToString();
			httpRequest.Headers.Add("Authorization", GetAuthorizationHeaderString(_requestObject.Credentials));
			httpRequest.ContentType = "application/json";

			var data = JsonConvert.SerializeObject(_requestObject.Data);
			
			using (var writer = new StreamWriter(request.GetRequestStream()))
			{
				writer.Write(data);
			}
		}

		public string MapResponse(WebResponse response)
		{
			if (response == null)
			{
				throw new Exception("Could not get a response from Beanstream API");
			}
			
			return GetResponseBody(response);
		}

		private static string GetResponseBody(WebResponse response)
		{
			var stream = response.GetResponseStream();

			if (stream == null)
			{
				throw new Exception("Could not get a response from Beanstream API");
			}

			using (var reader = new StreamReader(stream))
			{
				return reader.ReadToEnd();
			}
		}

		private static string GetAuthorizationHeaderString(Credentials credentials)
		{
			var plainAuth = Encoding.UTF8.GetBytes(String.Format("{0}:{1}", credentials.Username, credentials.Password));
			var base64Auth = Convert.ToBase64String(plainAuth);

			return String.Format("{0} {1}", credentials.AuthScheme, base64Auth);
		}
	}
}