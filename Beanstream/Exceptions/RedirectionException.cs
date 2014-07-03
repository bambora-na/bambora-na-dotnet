using System.Net;

namespace Beanstream.Exceptions
{
	public class RedirectionException : BaseApiException
	{
		public RedirectionException(HttpStatusCode statusCode, string response)
			: base(statusCode, response)
		{ }
	}
}
