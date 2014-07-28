// The MIT License (MIT)
//
// Copyright (c) 2014 Beanstream Internet Commerce Corp, Digital River, Inc.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//

using Beanstream.Data;
using Beanstream.Repositories;
using Beanstream.Requests;

namespace Beanstream
{
	public class Beanstream
	{
		/// <summary>
		/// The Beanstream merchant ID 
		/// </summary>
		public int MerchantId { get; set; }

		/// <summary>
		/// The API Key (Passcode) for accessing the API.
		/// </summary>
		public string ApiKey { get; set; }

		public string ProfilesApiKey { get; set; }

		/// <summary>
		/// The api version to use
		/// </summary>
		public string ApiVersion { get; set; }


		private Configuration _configuration { get; set; }

		public Configuration Configuration {
			get {
				if (_configuration == null)
					_configuration = new Configuration {
						MerchantId = this.MerchantId,
						ApiPasscode = ApiKey,
						ProfilesPasscode = ProfilesApiKey,
						Version = ApiVersion
					};
				return _configuration;
			}
		}

		public IWebCommandExecuter WebCommandExecuter { get; set; }

		private TransactionRepository _transaction;

		public TransactionRepository Transaction 
		{ 
			get { 
				if (_transaction == null)
					_transaction = new TransactionRepository ();
				_transaction.Configuration = Configuration;
				if (WebCommandExecuter != null)
					_transaction.WebCommandExecuter = WebCommandExecuter;
				return _transaction;
			} 
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
