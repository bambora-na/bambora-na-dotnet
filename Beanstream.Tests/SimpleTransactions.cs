using System;
using NUnit.Framework;
using Beanstream.Data;
using Moq;
using Beanstream.Repository;
using Newtonsoft.Json.Linq;

namespace Beanstream.Tests
{

	public class SimpleTransactions
	{

		public static void Main(string[] args) {
			Console.WriteLine ("Running sample transactions...");

			SimpleTransactions.ProcessPayment ();
		}

		static void ProcessPayment ()
		{
			object payment = new
			{
				order_number = "Test1234",
				amount = 100.00,
				customer_ip = "10.240.13.10",
				comments ="First transaction",
				payment_method = "card",
				card = new
				{
					name = "John Doe",
					number = "5100000010001004",
					expiry_month = "12",
					expiry_year = "23",
					cvd = "123"
				},
				billing = new 
				{
					name = "John Doe",
					address_line1 = "2659 Douglas Street",
					address_line2 = "302",
					city = "Victoria",
					province = "BC",
					country = "CA",
					postal_code = "V8T 4M3",
					phone_number = "2501231234",
					email_address = "johndoe@beanstream.com"
				},
				shipping = new {
					name = "John Doe",
					address_line1 = "2659 Douglas Street",
					address_line2 = "302",
					city = "Victoria",
					province = "BC",
					country = "CA",
					postal_code = "V8T 4M3",
					phone_number = "2501231234",
					email_address = "johndoe@beanstream.com"
				}
			};
					

			Beanstream.MerchantId = 300200554;
			Beanstream.ApiKey = "8AE1629370674AD4BC769237ADC541E6"; // your private key. Access it in the Member Area: https://www.beanstream.com/admin/sDefault.asp Configuration -> Payment Profile Configuration -> Security Settings -> API access passcode
			Beanstream.Payments.Repository = new PaymentsRepository( );

			// Act
			dynamic result = Beanstream.Payments.Create(payment);
			Console.WriteLine ("Transaction result: " + result);
		}
	}
}

