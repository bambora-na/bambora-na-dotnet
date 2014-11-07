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
using Beanstream.Api.SDK.Requests;
using Newtonsoft.Json;
using Beanstream.Api.SDK.Data;

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
///  Gateway beanstream = new Gateway () {
/// 	MerchantId = 300200578,
/// 	ApiKey = "4BaD82D9197b4cc4b70a221911eE9f70",
/// 	ApiVersion = "1"
///  };
/// 
///  PaymentResponse response = beanstream.Payments.MakePayment (
/// 	new CardPaymentRequest {
/// 		Amount = 100.00,
/// 		OrderNumber = orderNum++.ToString(),
/// 		Card = new Card {
/// 			Name = "John Doe",
/// 			Number = "5100000010001004",
/// 			ExpiryMonth = "12",
/// 			ExpiryYear = "18",
/// 			Cvd = "123"
/// 		}
/// 	}
///  );
/// 
/// </summary>

namespace Beanstream.Api.SDK
{
	public class PaymentsAPI
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
		public PaymentResponse MakePayment(PaymentRequest paymentRequest) {

			Gateway.ThrowIfNullArgument (paymentRequest, "paymentRequest");

			string url = BeanstreamUrls.BasePaymentsUrl
							.Replace("{v}", String.IsNullOrEmpty(_configuration.Version) ? "v1" : "v"+_configuration.Version)
	   					    .Replace("{p}", String.IsNullOrEmpty(_configuration.Platform) ? "www" : _configuration.Platform);


			HttpsWebRequest req = new HttpsWebRequest () {
				MerchantId = _configuration.MerchantId,
				Passcode = _configuration.PaymentsApiPasscode,
				WebCommandExecutor = _webCommandExecuter
			};

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

			Gateway.ThrowIfNullArgument (returnRequest, "returnRequest");
			Gateway.ThrowIfNullArgument (paymentId, "paymentId");

			string url = BeanstreamUrls.ReturnsUrl
				.Replace("{v}", String.IsNullOrEmpty(_configuration.Version) ? "v1" : "v"+_configuration.Version)
				.Replace("{p}", String.IsNullOrEmpty(_configuration.Platform) ? "www" : _configuration.Platform)
				.Replace("{id}", String.IsNullOrEmpty(paymentId) ? "" : paymentId);


			HttpsWebRequest req = new HttpsWebRequest () {
				MerchantId = _configuration.MerchantId,
				Passcode = _configuration.PaymentsApiPasscode,
				WebCommandExecutor = _webCommandExecuter
			};
			returnRequest.MerchantId = _configuration.MerchantId.ToString();

			string response = req.ProcessTransaction (HttpMethod.Post, url, returnRequest);
			return JsonConvert.DeserializeObject<PaymentResponse>(response);

		}

		/// <summary>
		/// Return a previous card payment that was not made through Beanstream. Use this if you would like to
		/// return a payment but that payment was performed on another gateway.
		/// 
		/// You must have this capability enabled on your account by calling Beanstream support. It is dangerous to
		/// have it enabled as the API will not check if you have a transaction ID.
		/// </summary>
		/// <returns>The return result</returns>
		/// <param name="returnRequest">Return request.</param>
		/// <param name="adjId">Reference the transaction identification number (trnId) from the original purchase</param>
		public PaymentResponse UnreferencedReturn(UnreferencedCardReturnRequest returnRequest) {
		
			Gateway.ThrowIfNullArgument (returnRequest, "returnRequest");

			string url = BeanstreamUrls.ReturnsUrl
				.Replace("{v}", String.IsNullOrEmpty(_configuration.Version) ? "v1" : "v"+_configuration.Version)
				.Replace("{p}", String.IsNullOrEmpty(_configuration.Platform) ? "www" : _configuration.Platform)
				.Replace("{id}", "0"); // uses ID 0 since there is no existing payment ID for this transaction
				

			HttpsWebRequest req = new HttpsWebRequest () {
				MerchantId = _configuration.MerchantId,
				Passcode = _configuration.PaymentsApiPasscode,
				WebCommandExecutor = _webCommandExecuter
			};
			returnRequest.MerchantId = _configuration.MerchantId.ToString();

			string response = req.ProcessTransaction (HttpMethod.Post, url, returnRequest);
			return JsonConvert.DeserializeObject<PaymentResponse>(response);
		}


		/// <summary>
		/// Return a previous swipe payment that was not made through Beanstream. Use this if you would like to
		/// return a payment but that payment was performed on another payment service.
		/// 
		/// You must have this capability enabled on your account by calling Beanstream support. It is dangerous to
		/// have it enabled as the API will not check if you have a transaction ID.
		/// </summary>
		/// <returns>The return result</returns>
		/// <param name="returnRequest">Return request.</param>
		public PaymentResponse UnreferencedReturn(UnreferencedSwipeReturnRequest returnRequest) {

			Gateway.ThrowIfNullArgument (returnRequest, "returnRequest");

			string url = BeanstreamUrls.ReturnsUrl
				.Replace("{v}", String.IsNullOrEmpty(_configuration.Version) ? "v1" : "v"+_configuration.Version)
				.Replace("{p}", String.IsNullOrEmpty(_configuration.Platform) ? "www" : _configuration.Platform)
				.Replace("{id}", "0"); // uses ID 0 since there is no existing payment ID for this transaction


			HttpsWebRequest req = new HttpsWebRequest () {
				MerchantId = _configuration.MerchantId,
				Passcode = _configuration.PaymentsApiPasscode,
				WebCommandExecutor = _webCommandExecuter
			};
			returnRequest.MerchantId = _configuration.MerchantId.ToString();

			string response = req.ProcessTransaction (HttpMethod.Post, url, returnRequest);
			return JsonConvert.DeserializeObject<PaymentResponse>(response);
		}
			
		/// <summary>
		/// Void the specified paymentId.
		/// 
		/// Voids generally need to occur before end of business on the same day that the transaction was processed.
		/// 
		/// Voids are used to cancel a transaction before the item is registered against a customer credit card account. 
		/// Cardholders will never see a voided transaction on their credit card statement. As a result, voids can only 
		/// be attempted on the same day as the original transaction. After the end of day (roughly 11:59 pm EST/EDT), 
		/// void requests will be rejected from the API if attempted.
		/// </summary>
		/// <returns>The return result</returns>
		/// <param name="paymentId">Payment identifier from a previous transaction.</param>
		public PaymentResponse Void(string paymentId, double amount) {

			Gateway.ThrowIfNullArgument (paymentId, "paymentId");

			string url = BeanstreamUrls.VoidsUrl
				.Replace("{v}", String.IsNullOrEmpty(_configuration.Version) ? "v1" : "v"+_configuration.Version)
				.Replace("{p}", String.IsNullOrEmpty(_configuration.Platform) ? "www" : _configuration.Platform)
				.Replace("{id}", paymentId);


			HttpsWebRequest req = new HttpsWebRequest () {
				MerchantId = _configuration.MerchantId,
				Passcode = _configuration.PaymentsApiPasscode,
				WebCommandExecutor = _webCommandExecuter
			};

			var VoidPayment = new 
			{
				merchant_id = _configuration.MerchantId,
				amount = amount
			};
			
			string response = req.ProcessTransaction (HttpMethod.Post, url, VoidPayment);
			Console.WriteLine (response);
			return JsonConvert.DeserializeObject<PaymentResponse>(response);
		}

		/// <summary>
		/// Pre-authorize a payment. Use this if you want to know if a customer has sufficient funds
		/// before processing a payment. A real-world example of this is pre-authorizing at the gas pump
		/// for $100 before you fill up, then end up only using $60 of gas; the customer is only charged
		/// $60. The final payment is used with PreAuthCompletion().
		/// </summary>
		/// <returns>The response, in particular the payment ID that is needed to complete the purchase.</returns>
		/// <param name="paymentRequest">Payment request.</param>
		public PaymentResponse PreAuth(CardPaymentRequest paymentRequest) {

			Gateway.ThrowIfNullArgument (paymentRequest, "paymentRequest");

			paymentRequest.Card.Complete = false; // false to make it a pre-auth

			return PreAuthInternal (paymentRequest);
		}

		/// <summary>
		/// Pre-authorize a payment. Use this if you want to know if a customer has sufficient funds
		/// before processing a payment. A real-world example of this is pre-authorizing at the gas pump
		/// for $100 before you fill up, then end up only using $60 of gas; the customer is only charged
		/// $60. The final payment is used with PreAuthCompletion().
		/// 
		/// The PreAuth is used with tokenized payments with a token generated from the Legato Javascript service.
		/// </summary>
		/// <returns>The response, in particular the payment ID that is needed to complete the purchase.</returns>
		/// <param name="paymentRequest">Payment request.</param>
		public PaymentResponse PreAuth(TokenPaymentRequest paymentRequest) {

			Gateway.ThrowIfNullArgument (paymentRequest, "paymentRequest");

			return PreAuthInternal (paymentRequest);
		}

		/// <summary>
		/// Internal handling of the Pre-auth requests after the 'complete' parameter
		/// has been modified on the various PaymentRequest objects.
		/// </summary>
		/// <returns>The auth.</returns>
		/// <param name="paymentRequest">Payment request.</param>
		private PaymentResponse PreAuthInternal(PaymentRequest paymentRequest) {
		
			string url = BeanstreamUrls.BasePaymentsUrl
				.Replace ("{v}", String.IsNullOrEmpty (_configuration.Version) ? "v1" : "v" + _configuration.Version)
				.Replace ("{p}", String.IsNullOrEmpty (_configuration.Platform) ? "www" : _configuration.Platform);


			HttpsWebRequest req = new HttpsWebRequest () {
				MerchantId = _configuration.MerchantId,
				Passcode = _configuration.PaymentsApiPasscode,
				WebCommandExecutor = _webCommandExecuter
			};


			string response = req.ProcessTransaction (HttpMethod.Post, url, paymentRequest);
			return JsonConvert.DeserializeObject<PaymentResponse>(response);
		}
	

		/// <summary>
		/// Push the actual payment through after a pre-authorization.
		/// 
		/// Example:
		/// Pre-authorize at the gas pump for $100 using Pre-Authorization request. Card is approved for $100 but not charged.
		/// Consumer fills up with $60 worth of gas. Run the Pre-Auth-Completion request to process the actual
		/// payment for $60.
		/// 
		/// </summary>
		/// <returns>Response to the payment</returns>
		/// <param name="paymentId">Payment identifier obtained from the Pre-Auth request.</param>
		/// <param name="amount">Amount to process</param>
		public PaymentResponse PreAuthCompletion(string paymentId, double amount) {

			return PreAuthCompletion (paymentId, amount, null);
		}

		/// <summary>
		/// Push the actual payment through after a pre-authorization.
		/// 
		/// Example:
		/// Pre-authorize at the gas pump for $100 using Pre-Authorization request. Card is approved for $100 but not charged.
		/// Consumer fills up with $60 worth of gas. Run the Pre-Auth-Completion request to process the actual
		/// payment for $60.
		/// 
		/// </summary>
		/// <returns>Response to the payment</returns>
		/// <param name="paymentId">Payment identifier obtained from the Pre-Auth request.</param>
		/// <param name="amount">Amount to process</param>
		/// <param name="orderNumber">Optional order number</param>
		public PaymentResponse PreAuthCompletion(string paymentId, double amount, string orderNumber) {

			Gateway.ThrowIfNullArgument (paymentId, "paymentId");

			string url = BeanstreamUrls.PreAuthCompletionsUrl
				.Replace("{v}", String.IsNullOrEmpty(_configuration.Version) ? "v1" : "v"+_configuration.Version)
				.Replace("{p}", String.IsNullOrEmpty(_configuration.Platform) ? "www" : _configuration.Platform)
				.Replace("{id}", paymentId);


			HttpsWebRequest req = new HttpsWebRequest () {
				MerchantId = _configuration.MerchantId,
				Passcode = _configuration.PaymentsApiPasscode,
				WebCommandExecutor = _webCommandExecuter
			};

			var Completion = new 
			{
				merchant_id = _configuration.MerchantId,
				amount = amount,
				order_number = orderNumber
			};

			string response = req.ProcessTransaction (HttpMethod.Post, url, Completion);
			return JsonConvert.DeserializeObject<PaymentResponse>(response);
		}

		//TODO being implemented
		/*public PaymentResponse InteracRedirect(String idebit_merchantdata, InteracRedirectRequest redirectRequest) {
			return null;
		}*/

	}
}

