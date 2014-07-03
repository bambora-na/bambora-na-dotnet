using System.Net;

namespace Beanstream.Exceptions
{
	public class InvalidRequestException : BaseApiException
	{
		public InvalidRequestException(HttpStatusCode statusCode, string response)
			: base(statusCode, response)
		{ }
	}
}