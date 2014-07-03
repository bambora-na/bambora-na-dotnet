using System;

namespace Beanstream.Exceptions
{
	public class CommunicationException : Exception
	{
		public CommunicationException(string message, Exception exception)
			: base(message, exception)
		{ }
	}
}