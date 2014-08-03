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

/// <summary>
/// Transaction repository is used to process payments and returns.
/// 
/// Use this if you want to collect payments with credit cards, Interac, or with tokens using the Legato service.
/// 
/// Each transaction returns a PaymentResponse that holds the transaction's information, including the payment ID when creating a payment.
/// This Payment ID is used for returns.
/// 
/// Usage:
/// 
///  Beanstream beanstream = new Beanstream () {
///		MerchantId = YOUR_MERCHANT_ID,
///		ApiKey = "YOUR_API_KEY",
///		ApiVersion = "1"
///	 };
///
///PaymentResponse response = beanstream.Transaction.MakeCardPayment (
///	new CardPaymentRequest {
///		order_number = "ABC1234567890997",
///		amount = "100.00",
///		card = new Card {
///			name = "John Doe",
///			number = "5100000010001004",
///			expiry_month = "12",
///			expiry_year = "18",
///			cvd = "123"
///		}
///	}
///);
/// 
/// </summary>
using Beanstream.Data;


namespace Beanstream
{
	public class TransactionRepository
	{
		private Configuration _configuration;
		private IWebCommandExecuter _webCommandExecuter = new WebCommandExecuter ();

		public Configuration Configuration {
			set { _configuration = value; }
		}

		public IWebCommandExecuter WebCommandExecuter {
			set { _webCommandExecuter = value; }
		}

		/// <summary>
		/// Make a credit card payment.
		/// </summary>
		/// <returns>he payment result</returns>
		/// <param name="paymentRequest">Payment request.</param>
		public PaymentResponse MakeCardPayment(CardPaymentRequest paymentRequest) {

			Beanstream.ThrowIfNullArgument (paymentRequest, "MakeCardPayment");

			string url = BeanstreamUrls.BasePaymentsUrl
							.Replace("{v}", String.IsNullOrEmpty(_configuration.Version) ? "v1" : "v"+_configuration.Version)
	   					    .Replace("{p}", String.IsNullOrEmpty(_configuration.Platform) ? "www" : _configuration.Platform);

			HttpsWebRequest req = new HttpsWebRequest () {
				MerchantId = _configuration.MerchantId,
				Passcode = _configuration.ApiPasscode,
				WebCommandExecutor = _webCommandExecuter
			};

				
			paymentRequest.merchant_id = _configuration.MerchantId.ToString();

			string response = req.ProcessTransaction (HttpMethod.Post, url, paymentRequest);
			return JsonConvert.DeserializeObject<PaymentResponse>(response);

		}

		/// <summary>
		/// Make a payment using a token from the Legato service.
		/// This token represents the credit card so you do not have to store the credit card information
		/// yourself. This helps you easily be PCI compliant.
		/// </summary>
		/// <returns>The payment result</returns>
		/// <param name="paymentRequest">Payment request.</param>
		public PaymentResponse MakeTokenPayment(TokenPaymentRequest paymentRequest) {

			string url = BeanstreamUrls.BasePaymentsUrl
				.Replace("{v}", String.IsNullOrEmpty(_configuration.Version) ? "v1" : "v"+_configuration.Version)
				.Replace("{p}", String.IsNullOrEmpty(_configuration.Platform) ? "www" : _configuration.Platform);

			HttpsWebRequest req = new HttpsWebRequest () {
				MerchantId = _configuration.MerchantId,
				Passcode = _configuration.ApiPasscode,
				WebCommandExecutor = _webCommandExecuter
			};

			paymentRequest.merchant_id = _configuration.MerchantId.ToString();

			string response = req.ProcessTransaction (HttpMethod.Post, url, paymentRequest);
			return JsonConvert.DeserializeObject<PaymentResponse>(response);

		}

		/// <summary>
		/// Return a previous payment made through Beanstream
		/// </summary>
		/// <returns>The payment result</returns>
		/// <param name="paymentId">Payment identifier.</param>
		/// <param name="returnRequest">Return request.</param>
		public PaymentResponse Return(string paymentId, ReturnRequest returnRequest) {

			if (returnRequest == null || paymentId == null)
				throw new ArgumentNullException ("return");

			if (returnRequest.amount == null)
				throw new ArgumentNullException ("An amount is required in order to make a return.");

			string url = BeanstreamUrls.ReturnsUrl
				.Replace("{v}", String.IsNullOrEmpty(_configuration.Version) ? "v1" : "v"+_configuration.Version)
				.Replace("{p}", String.IsNullOrEmpty(_configuration.Platform) ? "www" : _configuration.Platform)
				.Replace("{id}", String.IsNullOrEmpty(paymentId) ? "" : paymentId);


			HttpsWebRequest req = new HttpsWebRequest () {
				MerchantId = _configuration.MerchantId,
				Passcode = _configuration.ApiPasscode,
				WebCommandExecutor = _webCommandExecuter
			};
			returnRequest.merchant_id = _configuration.MerchantId.ToString();

			string response = req.ProcessTransaction (HttpMethod.Post, url, returnRequest);
			return JsonConvert.DeserializeObject<PaymentResponse>(response);

		}

		/// <summary>
		/// Return a previous card payment that was not made through Beanstream. Use this if you would like to
		/// return a payment but that payment was performed on another gateway.
		/// </summary>
		/// <returns>The return result</returns>
		/// <param name="returnRequest">Return request.</param>
		public PaymentResponse UnreferencedReturn(UnreferencedCardReturnRequest returnRequest) {
			string url = BeanstreamUrls.ReturnsUrl
				.Replace("{v}", String.IsNullOrEmpty(_configuration.Version) ? "v1" : "v"+_configuration.Version)
				.Replace("{p}", String.IsNullOrEmpty(_configuration.Platform) ? "www" : _configuration.Platform)
				.Replace("{id}", "0");


			HttpsWebRequest req = new HttpsWebRequest () {
				MerchantId = _configuration.MerchantId,
				Passcode = _configuration.ApiPasscode,
				WebCommandExecutor = _webCommandExecuter
			};
			returnRequest.merchant_id = _configuration.MerchantId.ToString();

			string response = req.ProcessTransaction (HttpMethod.Post, url, returnRequest);
			return JsonConvert.DeserializeObject<PaymentResponse>(response);
		}


		/// <summary>
		/// Return a previous swipe payment that was not made through Beanstream. Use this if you would like to
		/// return a payment but that payment was performed on another payment service.
		/// </summary>
		/// <returns>The return result</returns>
		/// <param name="returnRequest">Return request.</param>
		public PaymentResponse UnreferencedReturn(UnreferencedSwipeReturnRequest returnRequest) {
			string url = BeanstreamUrls.ReturnsUrl
				.Replace("{v}", String.IsNullOrEmpty(_configuration.Version) ? "v1" : "v"+_configuration.Version)
				.Replace("{p}", String.IsNullOrEmpty(_configuration.Platform) ? "www" : _configuration.Platform)
				.Replace("{id}", "0");


			HttpsWebRequest req = new HttpsWebRequest () {
				MerchantId = _configuration.MerchantId,
				Passcode = _configuration.ApiPasscode,
				WebCommandExecutor = _webCommandExecuter
			};
			returnRequest.merchant_id = _configuration.MerchantId.ToString();

			string response = req.ProcessTransaction (HttpMethod.Post, url, returnRequest);
			return JsonConvert.DeserializeObject<PaymentResponse>(response);
		}
			
		/// <summary>
		/// Void the specified paymentId.
		/// </summary>
		/// <returns>The return result</returns>
		/// <param name="paymentId">Payment identifier from a previous transaction.</param>
		public PaymentResponse Void(String paymentId) {
			string url = BeanstreamUrls.VoidsUrl
				.Replace("{v}", String.IsNullOrEmpty(_configuration.Version) ? "v1" : "v"+_configuration.Version)
				.Replace("{p}", String.IsNullOrEmpty(_configuration.Platform) ? "www" : _configuration.Platform)
				.Replace("{id}", "0");


			HttpsWebRequest req = new HttpsWebRequest () {
				MerchantId = _configuration.MerchantId,
				Passcode = _configuration.ApiPasscode,
				WebCommandExecutor = _webCommandExecuter
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

