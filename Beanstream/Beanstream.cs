using Beanstream.Data;
using Beanstream.Repositories;

namespace Beanstream
{
	public static class Beanstream
	{
		/// <summary>
		/// The Beanstream merchant ID 
		/// </summary>
		public static int? MerchantId { get; set; }

		/// <summary>
		/// The API Key (Passcode) for accessing the API.
		/// </summary>
		public static string ApiKey { get; set; }

		/// <summary>
		/// The username for accessing the API.
		/// </summary>
		public static string Username { get; set; }

		/// <summary>
		/// The password for accessing the API.
		/// </summary>
		public static string Password { get; set; }
		
		/// <summary>
		/// The api version to use
		/// </summary>
		public static string ApiVersion { get; set; }

		/// <summary>
		/// The Beanstream platform to use. e.g www or payments
		/// </summary>
		public static string Platform { get; set; }

		public static Payments Payments() 
		{
			return new Payments();
		}


		public static Profiles Profiles()
		{
			return new Profiles();
		}


		public static void ThrowIfNullArgument(object value, string name)
		{
			if (value == null)
			{
				throw new System.ArgumentNullException(name);
			}
		}

	}

}
