using System;
using System.Globalization;
using System.IO;
using System.Net;
using Beanstream.Data;
using Beanstream.Entities;
using Beanstream.Exceptions;

namespace Beanstream.Repository
{
	public class PaymentsRepository : IPaymentsRepository
	{
		private IWebCommandExecuter _executer;

		public string ApiVersion { get; set; }
		public string Platform { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public int? MerchantId { get; set; }
		public string Passcode { get; set; }

		public PaymentsRepository()
		{
		}

		public PaymentsRepository(IWebCommandExecuter executer)
		{
			_executer = executer;
		}

		public string Create(object payment)
		{
			ThrowIfNullArgument(payment, "payment");

			return ProcessTransaction(HttpMethod.Post, GetPaymentsUrl(), payment);
		}

		public string Return(int? transId, object payment)
		{
			ThrowIfNullArgument(transId, "TransId");
			ThrowIfNullArgument(payment, "payment");

			var url = GetPaymentsUrl() +
			          BeanstreamUrls.ReturnsUri.Replace("{id}", transId.ToString());
			
			return ProcessTransaction(HttpMethod.Post, url, payment);
		}

		public string Void(int? transId, object payment)
		{
			ThrowIfNullArgument(transId, "TransId");
			ThrowIfNullArgument(payment, "payment");

			var url = GetPaymentsUrl() +
				BeanstreamUrls.VoidsUri.Replace("{id}", transId.ToString());

			return ProcessTransaction(HttpMethod.Post, url, payment);
		}

		public string Complete(int? transId, object payment)
		{
			ThrowIfNullArgument(transId, "TransId");
			ThrowIfNullArgument(payment, "payment");

			var url = GetPaymentsUrl() + 
				BeanstreamUrls.PreAuthCompletionsUri.Replace("{id}", transId.ToString());

			return ProcessTransaction(HttpMethod.Post, url, payment);
		}

		public string Continue(string merchantData, object continuation)
		{
			ThrowIfNullArgument(merchantData, "MerchantData");
			ThrowIfNullArgument(continuation, "continuation");

			var url = GetPaymentsUrl() + 
				BeanstreamUrls.ContinuationsUri.Replace("{id}", merchantData);

			return ProcessTransaction(HttpMethod.Post, url, continuation);
		}

		private static void ThrowIfNullArgument(object value, string name)
		{
			if (value == null)
			{
				throw new ArgumentNullException(name);
			}
		}

		private string GetPaymentsUrl()
		{
			return BeanstreamUrls.BasePaymentsUrl
				.Replace("{v}", String.IsNullOrEmpty(ApiVersion) ? "v1" : ApiVersion)
				.Replace("{p}", String.IsNullOrEmpty(Platform) ? "www" : Platform);
		}

		private string ProcessTransaction(HttpMethod method, string url, object payment)
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
				var requestInfo = new RequestObject(method, url, authInfo, payment);

			//try
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
				case HttpStatusCode.Found:
					return new RedirectionException(statusCode, data);
				case HttpStatusCode.BadRequest:
					return new InvalidRequestException(statusCode, data);
				case HttpStatusCode.Unauthorized:
					return new UnauthorizedException(statusCode, data);
				case HttpStatusCode.Forbidden:
					return new ForbiddenException(statusCode, data);
				case HttpStatusCode.PaymentRequired:
					return new BusinessRuleException(statusCode, data);
				case HttpStatusCode.MethodNotAllowed:
					return new InvalidRequestException(statusCode, data);
				case HttpStatusCode.UnsupportedMediaType:
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