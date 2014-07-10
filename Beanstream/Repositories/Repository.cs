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
using System.Globalization;
using System.IO;
using System.Net;
using Beanstream.Data;
using Beanstream.Entities;
using Beanstream.Exceptions;

namespace Beanstream.Repositories
{
	public class Repository : IRepository
	{
		private IWebCommandExecuter _executer;

		public string ApiVersion { get; set; }
		public string Platform { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public int? MerchantId { get; set; }
		public string Passcode { get; set; }
		public string Url	{ get; set; }

		public Repository()
		{
		}

		public Repository(IWebCommandExecuter executer)
		{
			_executer = executer;
		}

		/*public string Create(object payment)
		{
			ThrowIfNullArgument(payment, "payment");

			return ProcessTransaction(HttpMethod.Post, GetUrl(), payment);
		}*/

		/*public string Return(int? transId, object payment)
		{
			ThrowIfNullArgument(transId, "TransId");
			ThrowIfNullArgument(payment, "payment");

			var url = GetUrl() +
			          BeanstreamUrls.ReturnsUri.Replace("{id}", transId.ToString());
			
			return ProcessTransaction(HttpMethod.Post, url, payment);
		}*/

		/*public string Void(int? transId, object payment)
		{
			ThrowIfNullArgument(transId, "TransId");
			ThrowIfNullArgument(payment, "payment");

			var url = GetUrl() +
				BeanstreamUrls.VoidsUri.Replace("{id}", transId.ToString());

			return ProcessTransaction(HttpMethod.Post, url, payment);
		}*/

		/*public string Complete(int? transId, object payment)
		{
			ThrowIfNullArgument(transId, "TransId");
			ThrowIfNullArgument(payment, "payment");

			var url = GetUrl() + 
				BeanstreamUrls.PreAuthCompletionsUri.Replace("{id}", transId.ToString());

			return ProcessTransaction(HttpMethod.Post, url, payment);
		}*/

		/*public string Continue(string merchantData, object continuation)
		{
			ThrowIfNullArgument(merchantData, "MerchantData");
			ThrowIfNullArgument(continuation, "continuation");

			var url = GetUrl() + 
				BeanstreamUrls.ContinuationsUri.Replace("{id}", merchantData);

			return ProcessTransaction(HttpMethod.Post, url, continuation);
		}*/

		/*private static void ThrowIfNullArgument(object value, string name)
		{
			if (value == null)
			{
				throw new ArgumentNullException(name);
			}
		}*/

		public string BuildUrl()
		{
			return Url
				.Replace("{v}", String.IsNullOrEmpty(ApiVersion) ? "v1" : ApiVersion)
				.Replace("{p}", String.IsNullOrEmpty(Platform) ? "www" : Platform);
		}

		public string ProcessTransaction(HttpMethod method, string url)
		{
			return ProcessTransaction (method, url, null);
		}

		public string ProcessTransaction(HttpMethod method, string url, object data)
		{
			try
			{
				var authScheme = "Basic";
				
				if (MerchantId != null)
				{
					Username = MerchantId.Value.ToString(CultureInfo.InvariantCulture);
					Password = Passcode;
					authScheme = "Passcode";
				}

				var authInfo = new Credentials(Username, Password, authScheme);
				var requestInfo = new RequestObject(method, url, authInfo, data);

				var command = new ExecuteWebRequest(requestInfo);
				if (_executer == null)
					_executer = new WebCommandExecuter();
				var result = _executer.ExecuteCommand(command);

				return result.Response;
			}
			catch (WebException ex) //catch web command exception
			{
				if (ex.Status != WebExceptionStatus.ProtocolError)
				{
					throw;
				}

				throw BeanstreamApiException(ex);
			}
		}

		private static Exception BeanstreamApiException(WebException webEx)
		{
			var response = webEx.Response as HttpWebResponse;

			if (response == null)
			{
				return new CommunicationException("Could not process the request succesfully", webEx);
			}

			var statusCode = response.StatusCode;
			var data = GetResponseBody(response); //Get from exception

			switch (statusCode)
			{
				case HttpStatusCode.Found: // 302
					return new RedirectionException(statusCode, data);

				case HttpStatusCode.BadRequest: // 400
					return new InvalidRequestException(statusCode, data); // user input error

				case HttpStatusCode.Unauthorized: // 401
					return new UnauthorizedException(statusCode, data); // authentication exception

				case HttpStatusCode.Forbidden: // 403
					return new ForbiddenException(statusCode, data); // authorization failure

				case HttpStatusCode.PaymentRequired: // 402
					return new BusinessRuleException(statusCode, data); // declined or account not live

				case HttpStatusCode.MethodNotAllowed: // 405
					return new InvalidRequestException(statusCode, data); // invalid request

				case HttpStatusCode.UnsupportedMediaType: // 415
					return new InvalidRequestException(statusCode, data);

				default:
					return new InternalServerException(statusCode, data);
			}
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
	}
}