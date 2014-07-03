namespace Beanstream.Data
{
	/// <summary>
	/// Represents the result of a web command, namely the status code, description and response data.
	/// </summary>
	public class WebCommandResult<T>
	{
		/// <summary>
		/// The web response as an instance of {T}.
		/// </summary>
		public T Response {get; set;}

		/// <summary>
		/// The return value of the command. In the case of an HTTP web request
		/// this will be set to the value of the HTTP status code.
		/// </summary>
		public int ReturnValue{get; set; } 
	}
}