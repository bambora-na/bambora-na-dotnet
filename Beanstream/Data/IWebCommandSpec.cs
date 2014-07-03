using System;
using System.Net;

namespace Beanstream.Web
{
	/// <summary>
	/// Represents a web command, i.e. An HTTP web request and its response.
	/// </summary>
	/// <typeparam name="T">The type representing the response from the request.</typeparam>
	public interface IWebCommandSpec<out T>
	{
		/// <summary>
		/// The URL of the web command.
		/// </summary>
		Uri Url { get; }

		/// <summary>
		/// Prepares the web request for executing.
		/// </summary>
		/// <param name="request">The request to prepare.</param>
		void PrepareRequest(WebRequest request);

		/// <summary>
		/// Maps the web response to an instance of type {T}.
		/// </summary>
		/// <param name="response">The response to map.</param>
		/// <returns>An instance of type {T} representing the data from the web response.</returns>
		T MapResponse(WebResponse response);
	}
}