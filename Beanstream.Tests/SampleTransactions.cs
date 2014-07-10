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

namespace Beanstream.Tests
{

	public class SampleTransactions
	{

		public static void Main(string[] args) {
			Console.WriteLine ("Running sample transactions...");

			//SampleTransactions.ProcessPayment ();

			// use Tokenized profiles to make payments
			string customerCode = SampleTransactions.TokenizedProfileCreate ();
			//SampleTransactions.TokenizedProfilePayment (customerCode);
			SampleTransactions.TokenizedProfileAddCard (customerCode);
		}

		static void ProcessPayment ()
		{
			Console.WriteLine ("Creating a payment...");
			var payment = new
			{
				order_number = "ABC1234567890993",
				amount = 100.00,
				payment_method = "card",
				card = new
				{
					name = "John Doe",
					number = "5100000010001004",
					expiry_month = "12",
					expiry_year = "18",
					cvd = "123"
				}
			};
					
			Beanstream.MerchantId = 300200578;
			Beanstream.ApiKey = "4BaD82D9197b4cc4b70a221911eE9f70"; // your private key. Generate it in the Member Area: https://www.beanstream.com/admin/sDefault.asp Administration -> Account -> Order Settings -> API access passcode

			// make the payment
			dynamic result = Beanstream.Payments().Create(payment);

			Console.WriteLine ("Transaction result: " + result);
		}

		static string TokenizedProfileCreate() 
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
		}
	}
}

