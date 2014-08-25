// The MIT License (MIT)
//
// Copyright (c) 2014 Beanstream Internet Commerce Corp, Digital River, Inc.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//

using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Beanstream.Web;
using Beanstream.Entities;

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
			if (_requestObject.Credentials != null) // we might use this for a no auth connection
				httpRequest.Headers.Add("Authorization", GetAuthorizationHeaderString(_requestObject.Credentials));
			httpRequest.ContentType = "application/json";

			var data = JsonConvert.SerializeObject(
				_requestObject.Data,
				Formatting.Indented,
				new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore } // ignore null values
			);

			// useful to examine output
			//Console.WriteLine ("URL: "+Url);
			//Console.WriteLine ("Request Data:\n"+data);

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