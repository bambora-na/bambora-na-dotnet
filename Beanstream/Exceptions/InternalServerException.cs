using System.Net;

namespace Beanstream.Exceptions
{
	public class InternalServerException : BaseApiException
	{
		public InternalServerException(HttpStatusCode statusCode, string response)
			: base(statusCode, response)
		{ }
	}
}