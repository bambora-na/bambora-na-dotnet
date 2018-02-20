// The MIT License (MIT)
//
// Copyright (c) 2018 Bambora, Inc.
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
using Bambora.SDK.Data;
using Bambora.SDK.Exceptions;

/// <summary>
/// Creates the actual web request and returns the response object.
/// It requires a merchantID and passcode to connect to the Bambora REST API.
/// </summary>
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Bambora.SDK.Domain;


namespace Bambora.SDK.Requests
{
	public class HttpsWebRequest
	{

		private string _merchantId;
		private string _passcode;
		private IWebCommandExecuter _executer = new WebCommandExecuter();

		public int MerchantId {
			set { _merchantId = value.ToString (CultureInfo.InvariantCulture); }
		}

		public string Passcode {
			set { _passcode = value; }
		}

		public IWebCommandExecuter WebCommandExecutor { 
			set { _executer = value; } 
		}

		public string ProcessTransaction(HttpMethod method, string url)
		{
			return ProcessTransaction (method, url, null);
		}

		public string ProcessTransaction(HttpMethod method, string url, object data)
		{
			try
			{
				var authScheme = "Passcode";

				Credentials authInfo = null;
				// this request might not be using authorization
				if (_passcode != null)
					authInfo = new Credentials(_merchantId, _passcode, authScheme);

				var requestInfo = new RequestObject(method, url, authInfo, data);

				var command = new ExecuteWebRequest(requestInfo);
				var result = _executer.ExecuteCommand(command);

				return result.Response;
			}
			catch (WebException ex) //catch web command exception
			{
				if (ex.Status != WebExceptionStatus.ProtocolError)
				{
					throw;
				}

				throw BamboraApiException(ex);
			}
		}

		private static Exception BamboraApiException(WebException webEx)
		{
			var response = webEx.Response as HttpWebResponse;

			if (response == null)
			{
				return new CommunicationException("Could not process the request succesfully", webEx);
			}

			var statusCode = response.StatusCode;
			var data = GetResponseBody(response); //Get from exception

			var code = -1;
			var category = -1;
			var message = "";
			if (data != null) {
				if (response.ContentType.Contains("application/json") ) {
					try {
						JToken json = JObject.Parse(data);
						if ( json != null && json.SelectToken("code") != null )
							code = Convert.ToInt32( json.SelectToken("code") );
						if ( json != null && json.SelectToken("category") != null )
							category = Convert.ToInt32( json.SelectToken("category") );
						if ( json != null && json.SelectToken("message") != null )
							message = json.SelectToken("message").ToString();
					} catch (Exception e) {
						// data is not json and not in the format we expect
					}
				}
			}

			switch (statusCode)
			{
				case HttpStatusCode.Found: // 302
					return new RedirectionException(statusCode, data, message, category, code); // Used for redirection response in 3DS, Masterpass and Interac Online requests

				case HttpStatusCode.BadRequest: // 400
					return new InvalidRequestException(statusCode, data, message, category, code); // Often missing a required parameter

				case HttpStatusCode.Unauthorized: // 401
					return new UnauthorizedException(statusCode, data, message, category, code); // authentication exception

				case HttpStatusCode.PaymentRequired: // 402
					return new BusinessRuleException(statusCode, data, message, category, code); // Request failed business requirements or rejected by processor/bank

				case HttpStatusCode.Forbidden: // 403
					return new ForbiddenException(statusCode, data, message, category, code); // authorization failure

				case HttpStatusCode.NotFound: // 404
					return new NotFoundException(statusCode, data, message, category, code); // item(s) not found

				case HttpStatusCode.MethodNotAllowed: // 405
					return new InvalidRequestException(statusCode, data, message, category, code); // Sending the wrong HTTP Method

				case HttpStatusCode.UnsupportedMediaType: // 415
					return new InvalidRequestException(statusCode, data, message, category, code); // Sending an incorrect Content-Type

				default:
					return new InternalServerException(statusCode, data, message, category, code);
			}
		}

		private static string GetResponseBody(WebResponse response)
		{
			var stream = response.GetResponseStream();

			if (stream == null)
			{
				throw new Exception("Could not get a response from Bambora API");
			}

			using (var reader = new StreamReader(stream))
			{
				return reader.ReadToEnd();
			}
		}
	}
}