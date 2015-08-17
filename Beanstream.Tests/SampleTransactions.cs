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
using Beanstream.Api.SDK.Data;
using Beanstream.Api.SDK;
using Newtonsoft.Json.Linq;
using Beanstream.Api.SDK.Requests;
using Beanstream.Api.SDK.Exceptions;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using Beanstream.Api.SDK.Domain;
using System.Collections.Generic;

/**
 * An integration test showing the capabilities of the SDK.
 */
using NUnit.Framework;


namespace Beanstream.Api.SDK.Tests
{
	[TestFixture]
	public class SampleTransactions
	{

		public static void Main(string[] args) {

			Console.WriteLine ("BEGIN running sample transactions");

			// Payments API
			SampleTransactions.ProcessPayment ();
			SampleTransactions.ProcessDeclinedPayment ();
			SampleTransactions.ProcessReturns ();
			SampleTransactions.ProcessPreauthorization ();
			SampleTransactions.ProcessVoids ();
			SampleTransactions.ProcessTokenPayment ();
			SampleTransactions.ProcessPhysicalPayments (); // you need these options (cash and cheque) enabled on your merchant account first
			//SampleTransactions.ProcessInterac ();
			SampleTransactions.GetTransaction ();
			SampleTransactions.QueryTransactions();
			SampleTransactions.CreateAndDeleteProfile ();
			SampleTransactions.CreateProfileWithToken ();
			SampleTransactions.ProfileTakePayment ();
			SampleTransactions.GetProfile ();
			SampleTransactions.UpdateProfile ();
			SampleTransactions.AddAndRemoveCardFromProfile ();
			SampleTransactions.GetAllCardsFromProfile ();
			SampleTransactions.GetCardFromProfile ();
			SampleTransactions.UpdateCardInProfile ();
			Console.WriteLine ("FINISHED running sample transactions");
		}



		static string ProcessPayment() {

			Console.WriteLine ("Processing Payment... ");

			Gateway beanstream = new Gateway () {
				MerchantId = 300200578,
				PaymentsApiKey = "4BaD82D9197b4cc4b70a221911eE9f70",
				ApiVersion = "1"
			};
					
			PaymentResponse response = beanstream.Payments.MakePayment (
				new CardPaymentRequest {
					Amount = 100.00,
					OrderNumber = getRandomOrderId("test"),
					Card = new Card {
						Name = "John Doe",
						Number = "5100000010001004",
						ExpiryMonth = "12",
						ExpiryYear = "18",
						Cvd = "123"
					}
				}
			);

			Console.WriteLine ("Payment id: " + response.TransactionId + ", " + response.Message+"\n");

			Assert.IsNotEmpty (response.TransactionId);
			Assert.AreEqual ("Approved", response.Message);
			Assert.AreEqual ("P", response.TransType);

			return response.TransactionId;
		}

		[Test]
		static void ProcessDeclinedPayment() {

			Console.WriteLine ("Processing Payment... ");

			Gateway beanstream = new Gateway () {
				MerchantId = 300200578,
				PaymentsApiKey = "4BaD82D9197b4cc4b70a221911eE9f70",
				ApiVersion = "1"
			};

			try {
				PaymentResponse response = beanstream.Payments.MakePayment (
					new CardPaymentRequest {
						Amount = 100.00,
						OrderNumber = getRandomOrderId("test"),
						Card = new Card {
							Name = "John Doe",
							Number = "4003050500040005", // a test card that will decline
							ExpiryMonth = "12",
							ExpiryYear = "18",
							Cvd = "123"
						}
					}
				);


			} catch (RedirectionException ex) {
				// Redirect the user to the URL returned in the exception.
				// This is used for Interac and 3d Secure
			} catch (InvalidRequestException ex) {
				// something was wrong with the card info or it was declined. Send a message
				// to the card holder.
				Console.WriteLine(ex.ResponseMessage);
			} catch (BaseApiException ex) {
				// all other errors are caught here.
				// Be careful not to return very detailed error messages to the users. This info
				// can be used maliciously for "carding" (testing a lot of stolen card numbers to see
				// what ones are valid).
				Console.WriteLine(ex.ResponseMessage);
			}
		}

		[Test]
		static void ProcessReturns() {

			Console.WriteLine ("Processing Returns... ");

			Gateway beanstream = new Gateway () {
				MerchantId = 300200578,
				PaymentsApiKey = "4BaD82D9197b4cc4b70a221911eE9f70",
				ApiVersion = "1"
			};

			// we make a payment first so we can return it
			PaymentResponse response = beanstream.Payments.MakePayment (
				new CardPaymentRequest {
					Amount = 40.00,
					OrderNumber = getRandomOrderId("test"),
					Card = new Card {
						Name = "John Doe",
						Number = "5100000010001004",
						ExpiryMonth = "12",
						ExpiryYear = "18",
						Cvd = "123"
					}
				}
			);
			Console.WriteLine ("Return Payment id: " + response.TransactionId);

			Assert.IsNotEmpty (response.TransactionId);
			Assert.AreEqual ("Approved", response.Message);
			Assert.AreEqual ("P", response.TransType);

			// return the purchase
			response = beanstream.Payments.Return (
				response.TransactionId,
				new ReturnRequest {
					Amount = 40.00,
					OrderNumber = getRandomOrderId("test")
				}
			);

			Console.WriteLine ("Return result: " + response.TransactionId + ", " + response.Message+"\n" );

			Assert.IsNotEmpty (response.TransactionId);
			Assert.AreEqual ("Approved", response.Message);
			Assert.AreEqual ("R", response.TransType);

		}


		[Test]
		static void ProcessPreauthorization() {

			Console.WriteLine ("Processing Pre-auth payments... ");

			Gateway beanstream = new Gateway () {
				MerchantId = 300200578,
				PaymentsApiKey = "4BaD82D9197b4cc4b70a221911eE9f70",
				ApiVersion = "1"
			};

			CardPaymentRequest paymentRequest = new CardPaymentRequest {
				Amount = 100.00,
				OrderNumber = getRandomOrderId("test"),
				Card = new Card {
					Name = "John Doe",
					Number = "5100000010001004",
					ExpiryMonth = "12",
					ExpiryYear = "18",
					Cvd = "123"
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


			Console.WriteLine ("Pre-auth Payment id: " + response.TransactionId + ", " + response.Message);

			Assert.IsNotEmpty (response.TransactionId);
			Assert.AreEqual ("Approved", response.Message);
			Assert.AreEqual ("PA", response.TransType);

			// complete the pre-auth and get the money from the customer
			response = beanstream.Payments.PreAuthCompletion ( response.TransactionId, 60.00 );

			Console.WriteLine ("Pre-auth result: " + response.TransactionId + ", " + response.Message+"\n" );

			Assert.IsNotEmpty (response.TransactionId);
			Assert.AreEqual ("Approved", response.Message);
			Assert.AreEqual ("PAC", response.TransType);
		}



		static void ProcessVoids() {

			Console.WriteLine ("Processing Voids... ");

			Gateway beanstream = new Gateway () {
				MerchantId = 300200578,
				PaymentsApiKey = "4BaD82D9197b4cc4b70a221911eE9f70",
				ApiVersion = "1"
			};

			// we make a payment first so we can void it
			PaymentResponse response = beanstream.Payments.MakePayment (
				new CardPaymentRequest {
					Amount = 30.00,
					OrderNumber = getRandomOrderId("test"),
					Card = new Card {
						Name = "John Doe",
						Number = "5100000010001004",
						ExpiryMonth = "12",
						ExpiryYear = "18",
						Cvd = "123"
					}
				}
			);
			Console.WriteLine ("Void Payment id: " + response.TransactionId);

			Assert.IsNotEmpty (response.TransactionId);
			Assert.AreEqual ("Approved", response.Message);
			Assert.AreEqual ("P", response.TransType);


			// void the purchase
			response = beanstream.Payments.Void (response.TransactionId, 30); // void the $30 amount

			Console.WriteLine ("Void result: " + response.TransactionId + ", " + response.Message+"\n");

			Assert.IsNotEmpty (response.TransactionId);
			Assert.AreEqual ("Approved", response.Message);
			Assert.AreEqual ("VP", response.TransType);
		}

		//TODO this is being implemented
		/*static void ProcessInterac() {

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
					order_number = getRandomOrderId("test")
				}
			);
			Console.WriteLine ("Interac Payment id: " + response.id + ", " + response.message+"\n");


			// STEP 2:
			// Next you redirect the user to the bank's website for the actual interac processing
		}*/


		static void ProcessTokenPayment ()
		{

			// The first step is to call the Legato service to get a token.
			// This is normally performed on the client machine, and not on the server.
			// The goal with tokens is to not have credit card information move through your server,
			// thus lowering your scope for PCI compliance

			string url = "https://www.beanstream.com/scripts/tokenization/tokens";
			var data = new {
				number = "5100000010001004",
				expiry_month = "12",
				expiry_year = "18",
				cvd = "123"
			};

			var requestInfo = new RequestObject(HttpMethod.Post, url, null, data);
			var command = new ExecuteWebRequest (requestInfo);
			WebCommandExecuter executer = new WebCommandExecuter ();
			var result = executer.ExecuteCommand (command);

			LegatoTokenResponse token = JsonConvert.DeserializeObject<LegatoTokenResponse>(result.Response);
			Console.WriteLine ("legato token: " + token.Token);

			// Now that we have a token that represents our credit card info, we can process
			// the payment with that token

			Gateway beanstream = new Gateway () {
				MerchantId = 300200578,
				PaymentsApiKey = "4BaD82D9197b4cc4b70a221911eE9f70",
				ApiVersion = "1"
			};

			PaymentResponse response = beanstream.Payments.MakePayment (
				new TokenPaymentRequest () 
				{
					Amount = 30,
					OrderNumber = getRandomOrderId("test"),
					Token = new Token {
						Code = token.Token,
						Name = "John Doe"
					}
				}
			);

			Console.WriteLine ("Token payment result: " + response.TransactionId + ", " + response.Message+"\n");

			Assert.IsNotEmpty (response.TransactionId);
			Assert.AreEqual ("Approved", response.Message);
			Assert.AreEqual ("P", response.TransType);
		}


		/// <summary>
		/// Process Cash and Cheque payments. This is a useful way to record a payment that
		/// you physically took.
		/// NOTE: You will need to have these payment options ACTIVATED by calling Beanstream 
		/// support at 1-888-472-0811
		/// 
		/// </summary>
		static void ProcessPhysicalPayments() {

			Console.WriteLine ("Processing Cash Payment... ");

			Gateway beanstream = new Gateway () {
				MerchantId = 300200578,
				PaymentsApiKey = "4BaD82D9197b4cc4b70a221911eE9f70",
				ApiVersion = "1"
			};


			// process cash payment
			PaymentResponse response = beanstream.Payments.MakePayment (
				new CashPaymentRequest () {
					Amount = 50.00,
					OrderNumber = getRandomOrderId("test")
				}
			);
			Console.WriteLine ("Cash Payment id: " + response.TransactionId + ", " + response.Message+"\n");

			Assert.IsNotEmpty (response.TransactionId);
			Assert.AreEqual ("Approved", response.Message);
			Assert.AreEqual ("P", response.TransType);


			Console.WriteLine ("Processing Cheque Payment... ");
			// process cheque payment
			response = beanstream.Payments.MakePayment (
				new ChequePaymentRequest () {
					Amount = 30.00,
					OrderNumber = getRandomOrderId("test")
				}
			);
			Console.WriteLine ("Cheque Payment id: " + response.TransactionId + ", " + response.Message+"\n");

			Assert.IsNotEmpty (response.TransactionId);
			Assert.AreEqual ("Approved", response.Message);
			Assert.AreEqual ("P", response.TransType);
		}


		public static void GetTransaction() {
			Console.WriteLine ("Getting Transaction... ");

			Gateway beanstream = new Gateway () {
				MerchantId = 300200578,
				PaymentsApiKey = "4BaD82D9197b4cc4b70a221911eE9f70",
				ReportingApiKey = "4e6Ff318bee64EA391609de89aD4CF5d",
				ApiVersion = "1"
			};

			PaymentResponse response = beanstream.Payments.MakePayment (
				new CardPaymentRequest {
					Amount = 100.00,
					OrderNumber = getRandomOrderId("test"),
					Card = new Card {
						Name = "John Doe",
						Number = "5100000010001004",
						ExpiryMonth = "12",
						ExpiryYear = "18",
						Cvd = "123"
					}
				}
			);

			beanstream.Payments.Void (response.TransactionId, 100);
			Transaction trans = beanstream.Reporting.GetTransaction (response.TransactionId);

			Console.WriteLine ("Payment id: " + response.TransactionId + ", " + response.Message+"\n");

			Assert.IsNotEmpty (response.TransactionId);
			Assert.AreEqual ("Approved", response.Message);
			Assert.AreEqual ("P", response.TransType);
			Assert.NotNull (trans.Adjustments);
			Assert.AreEqual (1, trans.Adjustments.Count);

			// look for the void payment, there should only be one adjustment
			foreach (Adjustment adj in trans.Adjustments) {
				Assert.AreEqual ("VP", adj.Type);
			}

		}


		private static void QueryTransactions() {

			Console.WriteLine ("Query Transaction... ");

			// create a payment so we have something to query for
			string transId = ProcessPayment ();

			Gateway beanstream = new Gateway () {
				MerchantId = 300200578,
				PaymentsApiKey = "4BaD82D9197b4cc4b70a221911eE9f70",
				ReportingApiKey = "4e6Ff318bee64EA391609de89aD4CF5d",
				ApiVersion = "1"
			};

			List<TransactionRecord> records = beanstream.Reporting.Query (  
				DateTime.Now.Subtract(TimeSpan.FromMinutes(1)), 
				DateTime.Now.Add(TimeSpan.FromMinutes(5)), 
				1, 
				1000, 
				new Criteria[]{
					new Criteria() {
						Field = QueryFields.TransactionId, 
						Operator = Operators.GreaterThanEqual, 
						Value = "1"
					},
					new Criteria() {
						Field = QueryFields.TransactionId, 
						Operator = Operators.LessThanEqual, 
						Value = "99999999"
					}
				}
			);

			Console.WriteLine ("Num records: " + records.Count);

			Assert.IsNotEmpty (records);
			Assert.NotNull (transId);

			bool found = false;
			foreach (TransactionRecord record in records) {
				if (record.TransactionId.ToString().Equals (transId))
					found = true;
			}
			Assert.True (found); // we need to make sure we found our transaction
		}




		private static void CreateAndDeleteProfile() {
			Console.WriteLine ("Creating Payment Profile... ");

			Gateway beanstream = new Gateway () {
				MerchantId = 300200578,
				PaymentsApiKey = "4BaD82D9197b4cc4b70a221911eE9f70",
				ReportingApiKey = "4e6Ff318bee64EA391609de89aD4CF5d",
				ProfilesApiKey = "D97D3BE1EE964A6193D17A571D9FBC80",
				ApiVersion = "1"
			};

			ProfileResponse response = beanstream.Profiles.CreateProfile (
				new Card() {
					Name = "Jane Doe",
					Number = "5100000010001004",
					ExpiryMonth = "12",
					ExpiryYear = "18",
					Cvd = "123"
				},
				new Address() {
					Name = "Jane Doe",
					AddressLine1 = "123 Fake St.",
					City = "victoria",
					Province = "bc",
					Country = "ca",
					PostalCode = "v9t2g6",
					PhoneNumber = "12501234567",
					EmailAddress = "test@beanstream.com"
				});
			Console.WriteLine ("Created profile with ID: " + response.Id);

			Assert.IsNotNull (response);
			Assert.AreEqual ("Operation Successful", response.Message);


			// delete it so when we create a profile again with the same card we won't get an error
			response = beanstream.Profiles.DeleteProfile (response.Id);
			Assert.IsNotNull (response);
			Assert.AreEqual ("Operation Successful", response.Message);
		}

		private static void CreateProfileWithToken() {
			Console.WriteLine ("Creating Payment Profile with a Legato Token... ");

			Gateway beanstream = new Gateway () {
				MerchantId = 300200578,
				PaymentsApiKey = "4BaD82D9197b4cc4b70a221911eE9f70",
				ReportingApiKey = "4e6Ff318bee64EA391609de89aD4CF5d",
				ProfilesApiKey = "D97D3BE1EE964A6193D17A571D9FBC80",
				ApiVersion = "1"
			};

			string url = "https://www.beanstream.com/scripts/tokenization/tokens";
			var data = new {
				number = "5100000010001004",
				expiry_month = "12",
				expiry_year = "18",
				cvd = "123"
			};

			var requestInfo = new RequestObject(HttpMethod.Post, url, null, data);
			var command = new ExecuteWebRequest (requestInfo);
			WebCommandExecuter executer = new WebCommandExecuter ();
			var result = executer.ExecuteCommand (command);

			LegatoTokenResponse token = JsonConvert.DeserializeObject<LegatoTokenResponse>(result.Response);
			Console.WriteLine ("Retrieved Legato Token: "+token.Token);

			// You can create a profile with a token instead of a card.
			// It will save the billing information, but the token is still single-use
			ProfileResponse response = beanstream.Profiles.CreateProfile (
				new Token() {
					Name = "Jane Doe",
					Code = token.Token
				}, 
				new Address() {
					Name = "Jane Doe",
					AddressLine1 = "123 Fake St.",
					City = "victoria",
					Province = "bc",
					Country = "ca",
					PostalCode = "v9t2g6",
					PhoneNumber = "12501234567",
					EmailAddress = "test@beanstream.com"
				});
			Console.WriteLine ("Created profile with ID: " + response.Id);

			Assert.IsNotNull (response);
			Assert.AreEqual ("Operation Successful", response.Message);

			// delete it so when we create a profile again with the same card we won't get an error
			beanstream.Profiles.DeleteProfile (response.Id);
		}

		private static void ProfileTakePayment() {
			Console.WriteLine ("Take Payment with Profile... ");

			Gateway beanstream = new Gateway () {
				MerchantId = 300200578,
				PaymentsApiKey = "4BaD82D9197b4cc4b70a221911eE9f70",
				ReportingApiKey = "4e6Ff318bee64EA391609de89aD4CF5d",
				ProfilesApiKey = "D97D3BE1EE964A6193D17A571D9FBC80",
				ApiVersion = "1"
			};

			ProfileResponse response = beanstream.Profiles.CreateProfile (
				new Card() {
					Name = "Jane Doe",
					Number = "5100000010001004",
					ExpiryMonth = "12",
					ExpiryYear = "18",
					Cvd = "123"
				}, 
				new Address() {
					Name = "Jane Doe",
					AddressLine1 = "123 Fake St.",
					City = "victoria",
					Province = "bc",
					Country = "ca",
					PostalCode = "v9t2g6",
					PhoneNumber = "12501234567",
					EmailAddress = "test@beanstream.com"
				});
			Console.WriteLine ("Created profile with ID: " + response.Id);

			// add a 2nd card
			response = beanstream.Profiles.AddCard (response.Id, new Card () {
				Name = "Jane Doe",
				Number = "4030000010001234",
				ExpiryMonth = "04",
				ExpiryYear = "19",
				Cvd = "123"
			});
				
			Assert.IsNotNull (response);
			Assert.AreEqual ("Operation Successful", response.Message);

			PaymentResponse payment = beanstream.Payments.MakePayment (new ProfilePaymentRequest() {
				Amount = 40.95,
				OrderNumber = getRandomOrderId("profile"),
				PaymentProfile = new PaymentProfileField() {
					CardId = 2,
					CustomerCode = response.Id
				}
			});
			Console.WriteLine (payment.Message);
			Assert.IsNotNull (payment);
			Assert.AreEqual ("Approved", payment.Message);
			Assert.AreEqual ("P", payment.TransType);
			Assert.AreEqual ("VI", payment.Card.CardType);

			// delete it so when we create a profile again with the same card we won't get an error
			beanstream.Profiles.DeleteProfile (response.Id);
		}


		private static void GetProfile() {
			Console.WriteLine ("Creating Payment Profile... ");

			Gateway beanstream = new Gateway () {
				MerchantId = 300200578,
				PaymentsApiKey = "4BaD82D9197b4cc4b70a221911eE9f70",
				ReportingApiKey = "4e6Ff318bee64EA391609de89aD4CF5d",
				ProfilesApiKey = "D97D3BE1EE964A6193D17A571D9FBC80",
				ApiVersion = "1"
			};

			ProfileResponse response = beanstream.Profiles.CreateProfile (
				new Card() {
					Name = "Jane Doe",
					Number = "5100000010001004",
					ExpiryMonth = "12",
					ExpiryYear = "18",
					Cvd = "123"
				}, 
				new Address() {
					Name = "Jane Doe",
					AddressLine1 = "123 Fake St.",
					City = "victoria",
					Province = "bc",
					Country = "ca",
					PostalCode = "v9t2g6",
					PhoneNumber = "12501234567",
					EmailAddress = "test@beanstream.com"
				});
			Console.WriteLine ("Created profile with ID: " + response.Id);

			Assert.IsNotNull (response);
			Assert.AreEqual ("Operation Successful", response.Message);

			PaymentProfile profile = beanstream.Profiles.GetProfile (response.Id);
			Assert.IsNotNull (profile);
			Assert.AreEqual (response.Id, profile.Id);
			Assert.AreEqual ("Jane Doe", profile.Billing.Name);
			Console.WriteLine ("got profile: " + profile.Id);

			// delete it so when we create a profile again with the same card we won't get an error
			beanstream.Profiles.DeleteProfile (response.Id);
		}

		private static void UpdateProfile() {
			Console.WriteLine ("Creating Payment Profile... ");

			Gateway beanstream = new Gateway () {
				MerchantId = 300200578,
				PaymentsApiKey = "4BaD82D9197b4cc4b70a221911eE9f70",
				ReportingApiKey = "4e6Ff318bee64EA391609de89aD4CF5d",
				ProfilesApiKey = "D97D3BE1EE964A6193D17A571D9FBC80",
				ApiVersion = "1"
			};

			ProfileResponse response = beanstream.Profiles.CreateProfile (
				new Card() {
					Name = "Jane Doe",
					Number = "5100000010001004",
					ExpiryMonth = "12",
					ExpiryYear = "18",
					Cvd = "123"
				}, 
				new Address() {
					Name = "Jane Doe",
					AddressLine1 = "123 Fake St.",
					City = "victoria",
					Province = "bc",
					Country = "ca",
					PostalCode = "v9t2g6",
					PhoneNumber = "12501234567",
					EmailAddress = "test@beanstream.com"
				});
			Console.WriteLine ("Created profile with ID: " + response.Id);
			Assert.IsNotNull (response);
			Assert.AreEqual ("Operation Successful", response.Message);

			PaymentProfile profile = beanstream.Profiles.GetProfile (response.Id);
			Console.WriteLine ("Profile.billing.city: " + profile.Billing.City);


			Console.WriteLine ("Updating profile's billing address: city");
			profile.Billing.City = "penticton";
			response = beanstream.Profiles.UpdateProfile (profile);
			Assert.IsNotNull (response);
			Assert.AreEqual ("Operation Successful", response.Message);

			profile = beanstream.Profiles.GetProfile (response.Id);
			Assert.IsNotNull (profile);
			Assert.AreEqual (response.Id, profile.Id);
			Assert.AreEqual ("penticton", profile.Billing.City);
			Console.WriteLine ("Profile.billing.city: " + profile.Billing.City);
			// delete it so when we create a profile again with the same card we won't get an error
			beanstream.Profiles.DeleteProfile (response.Id);
		}


		private static void AddAndRemoveCardFromProfile() {
			Console.WriteLine ("Adding Card to Profile... ");

			Gateway beanstream = new Gateway () {
				MerchantId = 300200578,
				PaymentsApiKey = "4BaD82D9197b4cc4b70a221911eE9f70",
				ReportingApiKey = "4e6Ff318bee64EA391609de89aD4CF5d",
				ProfilesApiKey = "D97D3BE1EE964A6193D17A571D9FBC80",
				ApiVersion = "1"
			};

			ProfileResponse response = beanstream.Profiles.CreateProfile (
				new Card() {
					Name = "Jane Doe",
					Number = "5100000010001004",
					ExpiryMonth = "12",
					ExpiryYear = "18",
					Cvd = "123"
				}, 
				new Address() {
					Name = "Jane Doe",
					AddressLine1 = "123 Fake St.",
					City = "victoria",
					Province = "bc",
					Country = "ca",
					PostalCode = "v9t2g6",
					PhoneNumber = "12501234567",
					EmailAddress = "test@beanstream.com"
				}); 
			Console.WriteLine ("Created profile with ID: " + response.Id);
			Assert.IsNotNull (response);
			Assert.AreEqual ("Operation Successful", response.Message);


			PaymentProfile profile = beanstream.Profiles.GetProfile (response.Id);

			response = profile.AddCard (beanstream.Profiles, new Card {
				Name = "Jane Doe",
				Number = "4030000010001234",
				ExpiryMonth = "03",
				ExpiryYear = "22",
				Cvd = "123"
			});
			Console.WriteLine ("Added card to profile");
			Assert.IsNotNull (response);
			Assert.AreEqual ("Operation Successful", response.Message);

			// delete the card
			response = profile.RemoveCard (beanstream.Profiles, 2); // delete card #2
			Assert.IsNotNull (response);
			Assert.AreEqual ("Operation Successful", response.Message);

			Console.WriteLine ("Removed card from profile");

			// delete it so when we create a profile again with the same card we won't get an error
			beanstream.Profiles.DeleteProfile (response.Id);
		}

		private static void AddTokenizedCardToProfileAndMakePayment() {
			Console.WriteLine ("Adding Tokenized Card to Profile... ");

			Gateway beanstream = new Gateway () {
				MerchantId = 300200578,
				PaymentsApiKey = "4BaD82D9197b4cc4b70a221911eE9f70",
				ReportingApiKey = "4e6Ff318bee64EA391609de89aD4CF5d",
				ProfilesApiKey = "D97D3BE1EE964A6193D17A571D9FBC80",
				ApiVersion = "1"
			};

			ProfileResponse response = beanstream.Profiles.CreateProfile (
				new Card() {
					Name = "Jane Doe",
					Number = "5100000010001004",
					ExpiryMonth = "12",
					ExpiryYear = "18",
					Cvd = "123"
				}, 
				new Address() {
					Name = "Jane Doe",
					AddressLine1 = "123 Fake St.",
					City = "victoria",
					Province = "bc",
					Country = "ca",
					PostalCode = "v9t2g6",
					PhoneNumber = "12501234567",
					EmailAddress = "test@beanstream.com"
				}); 
			Console.WriteLine ("Created profile with ID: " + response.Id);
			Assert.IsNotNull (response);
			Assert.AreEqual ("Operation Successful", response.Message);

			PaymentProfile profile = beanstream.Profiles.GetProfile (response.Id);

			// get a legato token representing the credit card
			string url = "https://www.beanstream.com/scripts/tokenization/tokens";
			var data = new {
				number = "4030000010001234",
				expiry_month = "12",
				expiry_year = "18",
				cvd = "123"
			};

			var requestInfo = new RequestObject(HttpMethod.Post, url, null, data);
			var command = new ExecuteWebRequest (requestInfo);
			WebCommandExecuter executer = new WebCommandExecuter ();
			var result = executer.ExecuteCommand (command);

			LegatoTokenResponse token = JsonConvert.DeserializeObject<LegatoTokenResponse>(result.Response);

			response = profile.AddCard (beanstream.Profiles, new Token {
				Name = "Jane Doe",
				Code = token.Token
			});
			Console.WriteLine ("Added tokenized card to profile");
			Assert.IsNotNull (response);
			Assert.AreEqual ("Operation Successful", response.Message);

			PaymentResponse pResp = beanstream.Payments.MakePayment (new ProfilePaymentRequest {
				Amount = 7.91,
				OrderNumber = getRandomOrderId("profile"),
				PaymentProfile = new PaymentProfileField() {
					CardId = 2,
					CustomerCode = response.Id
				}
			});
			Assert.IsNotNull (pResp);

			// delete it so when we create a profile again with the same card we won't get an error
			beanstream.Profiles.DeleteProfile (response.Id);
		}


		private static void GetAllCardsFromProfile() {
			Console.WriteLine ("Get all Cards from Profile... ");

			Gateway beanstream = new Gateway () {
				MerchantId = 300200578,
				PaymentsApiKey = "4BaD82D9197b4cc4b70a221911eE9f70",
				ReportingApiKey = "4e6Ff318bee64EA391609de89aD4CF5d",
				ProfilesApiKey = "D97D3BE1EE964A6193D17A571D9FBC80",
				ApiVersion = "1"
			};

			ProfileResponse response = beanstream.Profiles.CreateProfile (
				new Card() {
					Name = "Jane Doe",
					Number = "5100000010001004",
					ExpiryMonth = "12",
					ExpiryYear = "18",
					Cvd = "123"
				}, 
				new Address() {
					Name = "Jane Doe",
					AddressLine1 = "123 Fake St.",
					City = "victoria",
					Province = "bc",
					Country = "ca",
					PostalCode = "v9t2g6",
					PhoneNumber = "12501234567",
					EmailAddress = "test@beanstream.com"
				}); 
			Console.WriteLine ("Created profile with ID: " + response.Id);
			Assert.IsNotNull (response);
			Assert.AreEqual ("Operation Successful", response.Message);

			PaymentProfile profile = beanstream.Profiles.GetProfile (response.Id);

			response = profile.AddCard (beanstream.Profiles, new Card {
				Name = "Jane Doe",
				Number = "4030000010001234",
				ExpiryMonth = "03",
				ExpiryYear = "22",
				Cvd = "123"
			});
			Console.WriteLine ("Added card to profile");
			Assert.IsNotNull (response);
			Assert.AreEqual ("Operation Successful", response.Message);

			// get all cards
			IList<Card> cards = profile.getCards (beanstream.Profiles);
			Assert.NotNull (cards);
			Assert.AreEqual (2, cards.Count);
			Console.WriteLine ("Retrieved " + cards.Count + " cards from profile.");
			Console.WriteLine ("Card 1 expiry year: " + cards[0].ExpiryYear);
			Console.WriteLine ("Card 2 expiry year: " + cards[1].ExpiryYear);

			// delete it so when we create a profile again with the same card we won't get an error
			beanstream.Profiles.DeleteProfile (response.Id);
		}

		private static void GetCardFromProfile() {
			Console.WriteLine ("Get a Card from a Profile... ");

			Gateway beanstream = new Gateway () {
				MerchantId = 300200578,
				PaymentsApiKey = "4BaD82D9197b4cc4b70a221911eE9f70",
				ReportingApiKey = "4e6Ff318bee64EA391609de89aD4CF5d",
				ProfilesApiKey = "D97D3BE1EE964A6193D17A571D9FBC80",
				ApiVersion = "1"
			};

			ProfileResponse response = beanstream.Profiles.CreateProfile (
				new Card() {
					Name = "Jane Doe",
					Number = "5100000010001004",
					ExpiryMonth = "12",
					ExpiryYear = "18",
					Cvd = "123"
				}, 
				new Address() {
					Name = "Jane Doe",
					AddressLine1 = "123 Fake St.",
					City = "victoria",
					Province = "bc",
					Country = "ca",
					PostalCode = "v9t2g6",
					PhoneNumber = "12501234567",
					EmailAddress = "test@beanstream.com"
				}); 
			Console.WriteLine ("Created profile with ID: " + response.Id);
			Assert.IsNotNull (response);
			Assert.AreEqual ("Operation Successful", response.Message);

			PaymentProfile profile = beanstream.Profiles.GetProfile (response.Id);

			response = profile.AddCard (beanstream.Profiles, new Card {
				Name = "Jane Doe",
				Number = "4030000010001234",
				ExpiryMonth = "03",
				ExpiryYear = "22",
				Cvd = "123"
			});
			Console.WriteLine ("Added card to profile");
			Assert.IsNotNull (response);
			Assert.AreEqual ("Operation Successful", response.Message);

			// get card
			Card card = profile.getCard (beanstream.Profiles, 2);
			Console.WriteLine ("Retrieved card with expiry year: " + card.ExpiryYear);
			Assert.NotNull (card);
			Assert.AreEqual ("403000XXXXXX1234", card.Number);

			// delete it so when we create a profile again with the same card we won't get an error
			beanstream.Profiles.DeleteProfile (response.Id);
		}


		private static void UpdateCardInProfile() {
			Console.WriteLine ("Update a Card in a Profile... ");

			Gateway beanstream = new Gateway () {
				MerchantId = 300200578,
				PaymentsApiKey = "4BaD82D9197b4cc4b70a221911eE9f70",
				ReportingApiKey = "4e6Ff318bee64EA391609de89aD4CF5d",
				ProfilesApiKey = "D97D3BE1EE964A6193D17A571D9FBC80",
				ApiVersion = "1"
			};

			ProfileResponse response = beanstream.Profiles.CreateProfile (
				new Card() {
					Name = "Jane Doe",
					Number = "5100000010001004",
					ExpiryMonth = "12",
					ExpiryYear = "18",
					Cvd = "123"
				}, 
				new Address() {
					Name = "Jane Doe",
					AddressLine1 = "123 Fake St.",
					City = "victoria",
					Province = "bc",
					Country = "ca",
					PostalCode = "v9t2g6",
					PhoneNumber = "12501234567",
					EmailAddress = "test@beanstream.com"
				}); 
			Console.WriteLine ("Created profile with ID: " + response.Id);
			Assert.IsNotNull (response);
			Assert.AreEqual ("Operation Successful", response.Message);

			PaymentProfile profile = beanstream.Profiles.GetProfile (response.Id);
			Assert.IsNotNull (profile);

			// get card
			Card card = profile.getCard (beanstream.Profiles, 1);
			Console.WriteLine ("Retrieved card with expiry year: " + card.ExpiryYear);
			Assert.IsNotNull (card);
			Assert.AreEqual ("18", card.ExpiryYear);
			card.ExpiryYear = "20";
			profile.UpdateCard (beanstream.Profiles, card);
			Console.WriteLine ("Updated card expiry");
			card = profile.getCard (beanstream.Profiles, 1);
			Assert.IsNotNull (card);
			Assert.AreEqual ("20", card.ExpiryYear);
			Console.WriteLine ("Retrieved updated card with expiry year: " + card.ExpiryYear);

			// delete it so when we create a profile again with the same card we won't get an error
			beanstream.Profiles.DeleteProfile (response.Id);
		}



		private static int counter = 0;

		private static string getRandomOrderId(string prefix) {
			DateTime datetime = DateTime.Now;
			double seconds = (datetime - new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds;
			counter++;
			string orderId = "" + prefix +"_"+counter+"_"+seconds ;
			if (orderId.Length > 30)
				orderId = orderId.Substring (0, 29);
			return orderId;
		}
	}
}

