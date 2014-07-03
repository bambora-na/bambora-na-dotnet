namespace Beanstream.Entities
{
	public class Credentials
	{
		private readonly string _username;
		private readonly string _password;
		private readonly string _authScheme;

		public Credentials(string username, string password, string authScheme)
		{
			_username = username;
			_password = password;
			_authScheme = authScheme;
		}

		public string Username
		{
			get { return _username; }
		}

		public string Password
		{
			get { return _password; }
		}

		public string AuthScheme
		{
			get { return _authScheme; }
		}
	}
}
