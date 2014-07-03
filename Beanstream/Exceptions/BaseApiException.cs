using System;
using System.Net;

namespace Beanstream.Exceptions
{
	public abstract class BaseApiException : Exception
	{
		private readonly string _response;
		private readonly HttpStatusCode _statusCode;

		protected BaseApiException(HttpStatusCode statusCode, string response)
		{
			_response = response;
			_statusCode =  statusCode;
		}

		public string Response
		{
			get { return _response; }
		}

		public int StatusCode
		{
			get { return (int) _statusCode; }
		}
	}
}