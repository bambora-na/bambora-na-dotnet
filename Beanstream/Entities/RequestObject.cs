using System;

namespace Beanstream.Entities
{
	public class RequestObject
	{
		private readonly HttpMethod _method;
		private readonly String _url;
		private readonly Credentials _credentials;
		private readonly object _data;

		public RequestObject(HttpMethod method, string url, Credentials credentials, object data)
		{
			_method = method;
			_url = url;
			_data = data;
			_credentials = credentials;
		}

		public object Data
		{
			get { return _data; }
		}

		public HttpMethod Method
		{
			get { return _method; }
		}

		public Uri Url
		{
			get { return new Uri(_url); }
		}

		public Credentials Credentials
		{
			get { return _credentials; }
		}
	}
}