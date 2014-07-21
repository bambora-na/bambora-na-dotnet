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
using Beanstream.Requests;
using Beanstream.Repositories;
using Newtonsoft.Json;

namespace Beanstream
{
	public class TransactionRepository
	{
		private Configuration _configuration;

		public Configuration Configuration {
			set { _configuration = value; }
		}


		public PaymentResponse MakeCardPayment(CardPaymentRequest paymentRequest) {

			string url = BeanstreamUrls.BasePaymentsUrl
							.Replace("{v}", String.IsNullOrEmpty(_configuration.Version) ? "v1" : "v"+_configuration.Version)
	   					    .Replace("{p}", String.IsNullOrEmpty(_configuration.Platform) ? "www" : _configuration.Platform);

			HttpsWebRequest req = new HttpsWebRequest () {
				MerchantId = _configuration.MerchantId,
				Passcode = _configuration.ApiPasscode,
			};
				
			paymentRequest.merchant_id = _configuration.MerchantId.ToString();

			string response = req.ProcessTransaction (HttpMethod.Post, url, paymentRequest);
			return JsonConvert.DeserializeObject<PaymentResponse>(response);

		}

		public PaymentResponse MakeTokenPayment(TokenPaymentRequest paymentRequest) {

			string url = BeanstreamUrls.BasePaymentsUrl
				.Replace("{v}", String.IsNullOrEmpty(_configuration.Version) ? "v1" : "v"+_configuration.Version)
				.Replace("{p}", String.IsNullOrEmpty(_configuration.Platform) ? "www" : _configuration.Platform);

			HttpsWebRequest req = new HttpsWebRequest () {
				MerchantId = _configuration.MerchantId,
				Passcode = _configuration.ApiPasscode,
			};

			paymentRequest.merchant_id = _configuration.MerchantId.ToString();

			string response = req.ProcessTransaction (HttpMethod.Post, url, paymentRequest);
			return JsonConvert.DeserializeObject<PaymentResponse>(response);

		}

		public PaymentResponse Return(string paymentId, ReturnRequest returnRequest) {

			string url = BeanstreamUrls.BasePaymentsUrl + BeanstreamUrls.ReturnsUri;

			url.Replace("{v}", String.IsNullOrEmpty(_configuration.Version) ? "v1" : "v"+_configuration.Version)
				.Replace("{p}", String.IsNullOrEmpty(_configuration.Platform) ? "www" : _configuration.Platform)
				.Replace("{id}", String.IsNullOrEmpty(paymentId) ? "" : paymentId);


			HttpsWebRequest req = new HttpsWebRequest () {
				MerchantId = _configuration.MerchantId,
				Passcode = _configuration.ApiPasscode,
			};
			returnRequest.merchant_id = _configuration.MerchantId.ToString();

			string response = req.ProcessTransaction (HttpMethod.Post, url, returnRequest);
			return JsonConvert.DeserializeObject<PaymentResponse>(response);

		}

		public PaymentResponse UnreferencedReturn(UnreferencedCardReturnRequest returnRequest) {
			string url = BeanstreamUrls.BasePaymentsUrl + BeanstreamUrls.ReturnsUri;

			url.Replace("{v}", String.IsNullOrEmpty(_configuration.Version) ? "v1" : "v"+_configuration.Version)
				.Replace("{p}", String.IsNullOrEmpty(_configuration.Platform) ? "www" : _configuration.Platform)
				.Replace("{id}", 0);


			HttpsWebRequest req = new HttpsWebRequest () {
				MerchantId = _configuration.MerchantId,
				Passcode = _configuration.ApiPasscode,
			};
			returnRequest.merchant_id = _configuration.MerchantId.ToString();

			string response = req.ProcessTransaction (HttpMethod.Post, url, returnRequest);
			return JsonConvert.DeserializeObject<PaymentResponse>(response);
		}

		public PaymentResponse UnreferencedReturn(UnreferencedSwipeReturnRequest returnRequest) {
			string url = BeanstreamUrls.BasePaymentsUrl + BeanstreamUrls.ReturnsUri;

			url.Replace("{v}", String.IsNullOrEmpty(_configuration.Version) ? "v1" : "v"+_configuration.Version)
				.Replace("{p}", String.IsNullOrEmpty(_configuration.Platform) ? "www" : _configuration.Platform)
				.Replace("{id}", 0);


			HttpsWebRequest req = new HttpsWebRequest () {
				MerchantId = _configuration.MerchantId,
				Passcode = _configuration.ApiPasscode,
			};
			returnRequest.merchant_id = _configuration.MerchantId.ToString();

			string response = req.ProcessTransaction (HttpMethod.Post, url, returnRequest);
			return JsonConvert.DeserializeObject<PaymentResponse>(response);
		}

		public PaymentResponse Void(String paymentId) {
			string url = BeanstreamUrls.BasePaymentsUrl + BeanstreamUrls.VoidsUri;

			url.Replace("{v}", String.IsNullOrEmpty(_configuration.Version) ? "v1" : "v"+_configuration.Version)
				.Replace("{p}", String.IsNullOrEmpty(_configuration.Platform) ? "www" : _configuration.Platform)
				.Replace("{id}", 0);


			HttpsWebRequest req = new HttpsWebRequest () {
				MerchantId = _configuration.MerchantId,
				Passcode = _configuration.ApiPasscode,
			};

			var VoidPayment = new 
			{
				merchant_id = _configuration.MerchantId
			};
			
			string response = req.ProcessTransaction (HttpMethod.Post, url, VoidPayment);
			return JsonConvert.DeserializeObject<PaymentResponse>(response);
		}
	}
}

