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

using System;
using Beanstream.Data;

/// <summary>
///  DEPRECATED
/// </summary>
namespace Beanstream.Repositories
{
	public class Payments
	{
		//private IRepository _repository;

		/*public IRepository Repository
		{
			private get
			{
				if (_repository == null)
				{
					_repository = new HttpsWebRequest(
						new WebCommandExecuter()
					);
				}

				_repository.ApiVersion = Beanstream.ApiVersion;
				_repository.MerchantId = Beanstream.MerchantId;
				_repository.Passcode = Beanstream.ApiKey;
				//_repository.Username = Beanstream.Username;
				//_repository.Password = Beanstream.Password;
				//_repository.Platform = Beanstream.Platform;
				_repository.Url = BeanstreamUrls.BasePaymentsUrl;

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
		public string Create(object payment)
		{
			Beanstream.ThrowIfNullArgument(payment, "payment");

			string url = Repository.BuildUrl ();
			return Repository.ProcessTransaction(HttpMethod.Post, url, payment);
		}

		/// <summary>
		/// This method is used for processing Pre-Auth Completions. 
		/// </summary>
		/// <param name="transId">The transaction Id of the Pre-Auth to complete</param>
		/// <param name="data">An object representing a request data</param>
		/// <remarks>For more information on object see http://developer.beanstream.com</remarks>
		/// <returns></returns>
		public string Complete(int? transId, object payment)
		{
			Beanstream.ThrowIfNullArgument(transId, "TransId");
			Beanstream.ThrowIfNullArgument(payment, "payment");

			var url = Repository.BuildUrl() + 
				BeanstreamUrls.PreAuthCompletionsUri.Replace("{id}", transId.ToString());

			return Repository.ProcessTransaction(HttpMethod.Post, url, payment);
		}

		/// <summary>
		/// This method is used for processing Returns. 
		/// </summary>
		/// <param name="transId">The transaction Id of the initial purchase</param>
		/// <param name="data">An object representing a request data</param>
		/// <remarks>For more information on object see http://developer.beanstream.com</remarks>
		/// <returns></returns>
		public string Return(int? transId, object payment)
		{
			Beanstream.ThrowIfNullArgument(transId, "TransId");
			Beanstream.ThrowIfNullArgument(payment, "payment");

			var url = Repository.BuildUrl() +
				BeanstreamUrls.ReturnsUri.Replace("{id}", transId.ToString());

			return Repository.ProcessTransaction(HttpMethod.Post, url, payment);
		}

		/// <summary>
		/// This method is used for processing Voids. 
		/// </summary>
		/// <param name="transId">The transaction Id of the initial purchase/return</param>
		/// <param name="data">An object representing a request data</param>
		/// <remarks>For more information on object see http://developer.beanstream.com</remarks>
		/// <returns></returns>
		public string Void(int? transId, object payment)
		{
			Beanstream.ThrowIfNullArgument(transId, "TransId");
			Beanstream.ThrowIfNullArgument(payment, "payment");

			var url = Repository.BuildUrl() +
				BeanstreamUrls.VoidsUri.Replace("{id}", transId.ToString());

			return Repository.ProcessTransaction(HttpMethod.Post, url, payment);
		}

		/// <summary>
		/// This method is used for continuing a 3D Secure or Interac transactions. 
		/// </summary>
		/// <param name="merchantData">The merchant data from the initial request</param>
		/// <param name="data">An object representing a request data</param>
		/// <remarks>For more information on object see http://developer.beanstream.com</remarks>
		/// <returns></returns>
		public string Continue(string merchantData, object continuation)
		{
			Beanstream.ThrowIfNullArgument(merchantData, "MerchantData");
			Beanstream.ThrowIfNullArgument(continuation, "continuation");

			var url = Repository.BuildUrl() + 
				BeanstreamUrls.ContinuationsUri.Replace("{id}", merchantData);

			return Repository.ProcessTransaction(HttpMethod.Post, url, continuation);
			//return Repository.Continue(merchantData, data);
		}*/
	}
}

