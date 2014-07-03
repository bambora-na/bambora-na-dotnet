using System.Net;

namespace Beanstream.Exceptions
{
	public class UnauthorizedException : BaseApiException
	{
		public UnauthorizedException(HttpStatusCode statusCode, string response)
			: base(statusCode, response)
		{ }
	}
}