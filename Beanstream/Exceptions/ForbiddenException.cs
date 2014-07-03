using System.Net;

namespace Beanstream.Exceptions
{
	public class ForbiddenException : BaseApiException
	{
		public ForbiddenException(HttpStatusCode statusCode, string response)
			: base(statusCode, response)
		{ }
	}
}