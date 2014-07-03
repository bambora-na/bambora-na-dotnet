using System.Net;

namespace Beanstream.Exceptions
{
	public class BusinessRuleException : BaseApiException
	{
		public BusinessRuleException(HttpStatusCode statusCode, string response)
			: base(statusCode, response)
		{ }
	}
}