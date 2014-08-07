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
using Beanstream.Repositories;
using Newtonsoft.Json.Linq;
using Beanstream.Requests;
using Beanstream.Exceptions;

namespace Beanstream.Tests
{

	public class SampleTransactions
	{

		private static int orderNum = 76;

		public static void Main(string[] args) {
			Console.WriteLine ("BEGIN running sample transactions");

			// Payments API
			SampleTransactions.ProcessPayment ();
			SampleTransactions.ProcessReturns ();
			SampleTransactions.ProcessPreauthorization ();
			SampleTransactions.ProcessVoids ();
			SampleTransactions.ProcessInterac ();
			//SampleTransactions.CustomRequest ();

			// use Tokenized profiles to make payments
			//string customerCode = SampleTransactions.TokenizedProfileCreate ();
			//SampleTransactions.TokenizedProfilePayment (customerCode);
			//SampleTransactions.TokenizedProfileAddCard (customerCode);

			Console.WriteLine ("final order number: " + orderNum);
			Console.WriteLine ("FINISHED running sample transactions");
		}





		static void ProcessPayment() {

			Console.WriteLine ("Processing Payment... ");

			Beanstream beanstream = new Beanstream () {
				MerchantId = 300200578,
				ApiKey = "4BaD82D9197b4cc4b70a221911eE9f70",
				ApiVersion = "1"
			};

			PaymentResponse response = beanstream.Payments.MakePayment (
				new CardPaymentRequest {
					amount = "100.00",
					order_number = orderNum.ToString(),
					card = new Card {
						name = "John Doe",
						number = "5100000010001004",
						expiry_month = "12",
						expiry_year = "18",
						cvd = "123"
					}
				}
			);
			Console.WriteLine ("Payment id: " + response.id + ", " + response.message+"\n");

		}


		static void ProcessReturns() {

			Console.WriteLine ("Processing Returns... ");
			orderNum++;

			Beanstream beanstream = new Beanstream () {
				MerchantId = 300200578,
				ApiKey = "4BaD82D9197b4cc4b70a221911eE9f70",
				ApiVersion = "1"
			};

			// we make a payment first so we can return it
			PaymentResponse response = beanstream.Payments.MakePayment (
				new CardPaymentRequest {
					amount = "40.00",
					order_number = orderNum.ToString(),
					card = new Card {
						name = "John Doe",
						number = "5100000010001004",
						expiry_month = "12",
						expiry_year = "18",
						cvd = "123"
					}
				}
			);
			Console.WriteLine ("Return Payment id: " + response.id);


			// return the purchase
			response = beanstream.Payments.Return (
				response.id, // the payment ID
				new ReturnRequest {
					amount = "40.00",
				}
			);

			Console.WriteLine ("Return result: " + response.id + ", " + response.message+"\n" );
		}



		static void ProcessPreauthorization() {

			Console.WriteLine ("Processing Pre-auth payments... ");
			orderNum++;

			Beanstream beanstream = new Beanstream () {
				MerchantId = 300200578,
				ApiKey = "4BaD82D9197b4cc4b70a221911eE9f70",
				ApiVersion = "1"
			};

			CardPaymentRequest paymentRequest = new CardPaymentRequest {
				amount = "100.00",
				order_number = orderNum.ToString (),
				card = new Card {
					name = "John Doe",
					number = "5100000010001004",
					expiry_month = "12",
					expiry_year = "18",
					cvd = "123"
				}
			};

			// pre-authorize the payment for $100
			PaymentResponse response = beanstream.Payments.PreAuth (paymentRequest);

			// In order for Pre-authorizations to work, you must enable them on your account:
			// http://support.beanstream.com/#docs/pre-authorizations-process-transaction-api.htm%3FTocPath%3DDeveloper%2520Resources%7CThe%2520Process%2520Transaction%2520API%7C_____8
			// 
			// 1. Log in to the Online Member Area.
			// 2. Navigate to administration > account admin > order settings in the left menu.
			// 3. Under the heading Restrict Internet Transaction Processing Types, select either of the last two options:
			// 3.a. Select Purchases or Pre-Authorization Only: allows you to process both types of transaction through your web interface
			// 3.b. De-select Restrict Internet Transaction Processing Types: allows you to process all types of transactions including returns, voids and pre-auth completions


			Console.WriteLine ("Pre-auth Payment id: " + response.id + ", " + response.message);


			// complete the pre-auth and get the money from the customer
			response = beanstream.Payments.PreAuthCompletion ( response.id, "60.00" );

			Console.WriteLine ("Pre-auth result: " + response.id + ", " + response.message+"\n" );
		}



		static void ProcessVoids() {

			Console.WriteLine ("Processing Voids... ");
			orderNum++;

			Beanstream beanstream = new Beanstream () {
				MerchantId = 300200578,
				ApiKey = "4BaD82D9197b4cc4b70a221911eE9f70",
				ApiVersion = "1"
			};

			// we make a payment first so we can void it
			PaymentResponse response = beanstream.Payments.MakePayment (
				new CardPaymentRequest {
					amount = "30.00",
					order_number = orderNum.ToString(),
					card = new Card {
						name = "John Doe",
						number = "5100000010001004",
						expiry_month = "12",
						expiry_year = "18",
						cvd = "123"
					}
				}
			);
			Console.WriteLine ("Void Payment id: " + response.id);


			// void the purchase
			response = beanstream.Payments.Void (response.id, 30); // void the $30 amount

			Console.WriteLine ("Void result: " + response.id + ", " + response.message+"\n");
		}


		static void ProcessInterac() {

			Console.WriteLine ("Processing Interac Payment... ");

			Beanstream beanstream = new Beanstream () {
				MerchantId = 300200578,
				ApiKey = "4BaD82D9197b4cc4b70a221911eE9f70",
				ApiVersion = "1"
			};

			// STEP 1:
			// The first step is to tell Beanstream you are making an Interac payment
			// and to get the Transaction ID for the next step
			PaymentResponse response = beanstream.Payments.MakePayment (
				new InteracPaymentRequest {
					amount = "100.00",
					order_number = orderNum.ToString()
				}
			);
			Console.WriteLine ("Interac Payment id: " + response.id + ", " + response.message+"\n");


			// STEP 2:
			// Next you redirect the user to the bank's website for the actual interac processing
		}

		static void CustomRequest() {

			Console.WriteLine ("Processing Custom Payment... ");

			Beanstream beanstream = new Beanstream () {
				MerchantId = 300200578,
				ApiKey = "4BaD82D9197b4cc4b70a221911eE9f70",
				ApiVersion = "1"
			};

			string url = "https://{p}.beanstream.com/api/{v}/payments"
				.Replace("{v}", String.IsNullOrEmpty(beanstream.Configuration.Version) ? "v1" : "v"+beanstream.Configuration.Version)
				.Replace("{p}", String.IsNullOrEmpty(beanstream.Configuration.Platform) ? "www" : beanstream.Configuration.Platform);


			var payment = new
			{
				order_number = "ABC1234567890",
				amount = 100.00,
				payment_method = "interac", 
				billing = new 
				{
					name = "John Doe",
					address_line1 = "2659 Douglas Street",
					address_line2 = "302",
					city = "Victoria",
					province = "BC",
					country = "CA",
					postal_code = "V8T4M3",
					phone_number = "2501231234",
					email_address = "johndoe@beanstream.com"
				},
				comments= "create a payment"
			};


			object result = beanstream.Payments.CustomRequest (HttpMethod.Post, url, payment);
			Console.WriteLine ("Result: "+result);
		}

		/*static string TokenizedProfileCreate() 
		{
			Console.WriteLine ("Creating a Tokenized Profile...");
			var profile = new 
			{
				card = new 
				{
					number = "4030000010001234",
					expiry_month = "02",
					expiry_year = "15",
					name = "Jane Doe"  ,
					cvd = 123
				},
				billing = new 
				{
					name = "Jane Doe",
					address_line1 = "2659 Douglas Street",
					city = "Victoria",
					province = "BC",
					country = "CA",
					postal_code = "V8T 4M3",
					email_address = "janedoe@beanstream.com",
					phone_number = "2502505555"
				},

			};
			Beanstream.MerchantId = 300200578;
			Beanstream.ApiKey = "D97D3BE1EE964A6193D17A571D9FBC80"; // your private key for creating Profiles. Generate it in the Member Area: https://www.beanstream.com/admin/sDefault.asp Configuration -> Payment Profile Configuration -> Security Settings -> API access passcode


			dynamic result = Beanstream.Profiles().Create (profile);
			Console.WriteLine ("Tokenized profile creation result: " + result);

			// parse out the customer code
			JToken response = JObject.Parse (result);
			string customerCode = (string)response.SelectToken("customer_code");
			Console.WriteLine ("Customer code: " + customerCode);
			return customerCode;
		}

		static void TokenizedProfilePayment(string customerCode) 
		{
			Console.WriteLine ("Creating a payment through a Tokenized Profile...");
			var payment = new
			{
				order_number = "ABC1234567896",
				amount = 100.00,
				payment_method = "payment_profile",
				payment_profile = new 
				{
					card_id = 1,
					customer_code = customerCode
				}
			};

			Beanstream.MerchantId = 300200578;
			Beanstream.ApiKey = "4BaD82D9197b4cc4b70a221911eE9f70"; // your private key. Generate it in the Member Area: https://www.beanstream.com/admin/sDefault.asp Administration -> Account -> Order Settings -> API access passcode

			// make the payment
			dynamic result = Beanstream.Payments().Create(payment);

			Console.WriteLine ("Tokenized profile payment result: " + result);
		}

		static void TokenizedProfileAddCard(string customerCode) 
		{
			Console.WriteLine ("Adding a card to a Tokenized Profile...");
			var request = new
			{
				card = new
				{
					number = "5100000010001004",
					expiry_month = "03",
					expiry_year = "16",
					name = "Jane Doe" ,
					cvd = 123
				}
			};

			Beanstream.MerchantId = 300200578;
			Beanstream.ApiKey = "D97D3BE1EE964A6193D17A571D9FBC80"; // your private key for creating Profiles. Generate it in the Member Area: https://www.beanstream.com/admin/sDefault.asp Configuration -> Payment Profile Configuration -> Security Settings -> API access passcode

			// make the payment
			dynamic result = Beanstream.Profiles ().Profile (customerCode).AddCard (request);

			Console.WriteLine ("Added card to Tokenized Profile result: " + result);
		}*/
	}
}

