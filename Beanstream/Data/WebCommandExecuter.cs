using System;
using System.Net;
using Beanstream.Web;

namespace Beanstream.Data
{
	public class WebCommandExecuter : IWebCommandExecuter
	{
		public WebCommandResult<T> ExecuteCommand<T>(IWebCommandSpec<T> spec)
		{
			if(spec == null) 
			{
				throw new ArgumentNullException("spec");
			}

			var result = new WebCommandResult<T>();
			var request = WebRequest.Create(spec.Url);

			spec.PrepareRequest(request);

			using(var response = request.GetResponse() as HttpWebResponse)
			{
				if (response == null)
				{
					throw new Exception("Could not get a response from Beanstream API");
				}

				result.ReturnValue = (int) response.StatusCode;

				if(response.StatusCode == (HttpStatusCode) 200)
				{
					result.Response = spec.MapResponse(response);
				}
			}

			return result;
		}
	}
}