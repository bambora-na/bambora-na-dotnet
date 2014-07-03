using Beanstream.Data;
using Beanstream.Repository;

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

		public static class Payments
		{
			private static IPaymentsRepository _repository;

			public static IPaymentsRepository Repository
			{
				private get
				{
					if (_repository == null)
					{
						_repository = new PaymentsRepository(
							new WebCommandExecuter()
						);
					}

					_repository.ApiVersion = ApiVersion;
					_repository.MerchantId = MerchantId;
					_repository.Passcode = ApiKey;
					_repository.Username = Username;
					_repository.Password = Password;
					_repository.Platform = Platform;

					return _repository;
				}
				set
				{
					_repository = value;
				}
			}

			/// <summary>
			/// This method is used for processing Purchases and Pre-Authorizations. 
			/// </summary>
			/// <param name="data">An object representing a request data</param>
			/// <remarks>For more information on object see http://developer.beanstream.com</remarks>
			/// <returns></returns>
			public static string Create(object data)
			{
				return Repository.Create(data);
			}

			/// <summary>
			/// This method is used for processing Pre-Auth Completions. 
			/// </summary>
			/// <param name="transId">The transaction Id of the Pre-Auth to complete</param>
			/// <param name="data">An object representing a request data</param>
			/// <remarks>For more information on object see http://developer.beanstream.com</remarks>
			/// <returns></returns>
			public static string Complete(int? transId, object data)
			{
				return Repository.Complete(transId, data);
			}

			/// <summary>
			/// This method is used for processing Returns. 
			/// </summary>
			/// <param name="transId">The transaction Id of the initial purchase</param>
			/// <param name="data">An object representing a request data</param>
			/// <remarks>For more information on object see http://developer.beanstream.com</remarks>
			/// <returns></returns>
			public static string Return(int? transId, object data)
			{
				return Repository.Return(transId, data);
			}

			/// <summary>
			/// This method is used for processing Voids. 
			/// </summary>
			/// <param name="transId">The transaction Id of the initial purchase/return</param>
			/// <param name="data">An object representing a request data</param>
			/// <remarks>For more information on object see http://developer.beanstream.com</remarks>
			/// <returns></returns>
			public static string Void(int? transId, object data)
			{
				return Repository.Void(transId, data);
			}

			/// <summary>
			/// This method is used for continuing a 3D Secure or Interac transactions. 
			/// </summary>
			/// <param name="merchantData">The merchant data from the initial request</param>
			/// <param name="data">An object representing a request data</param>
			/// <remarks>For more information on object see http://developer.beanstream.com</remarks>
			/// <returns></returns>
			public static string Continue(string merchantData, object data)
			{
				return Repository.Continue(merchantData, data);
			}
		}
	}
}
