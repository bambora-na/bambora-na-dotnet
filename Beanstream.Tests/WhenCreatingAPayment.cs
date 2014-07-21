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
using System.Net;
using Beanstream.Data;
using Beanstream.Exceptions;
using Beanstream.Repositories;
using Moq;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Beanstream.Tests
{
	[TestFixture]
	public class WhenCreatingAPayment
	{
		private object _payment;
		private Mock<IWebCommandExecuter> _executer;

		/// <summary>
		/// Alternative payment methods
		/// payment_method = "payment_profile",
		///	payment_profile = new {
		///		complete = true,
		///		customer_code = "D675855E81b448a7bF0dD682DF74e613"
		///	},
		/// //The code is from legato
		///		payment_method = "token",
		///	token = new 
		///	{
		///		name = "john doe",
		///		code = "gt6-91ced41c-a2c0-4b1b-a838-cf62bbfdda02"
		///	},
		/// </summary>
		[SetUp]
		public void Setup()
		{
			_payment = new
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

			_executer = new Mock<IWebCommandExecuter>();
		}

		[Test]
		public void ItShouldHaveATransactionIdForASuccessfulPayment()
		{
			// Arrange
			var webresult = new WebCommandResult<string>{Response = @"{""id"":""10000000""}"};
			
			_executer.Setup(e => e.ExecuteCommand(It.IsAny<ExecuteWebRequest>())).Returns(webresult);
			
			Beanstream.MerchantId = 100000000;
			Beanstream.ApiKey = "F6EF00BDB80748358D52D8605CDC7027";
			Payments payments = Beanstream.Payments ();
			payments.Repository = new HttpWebRequest(_executer.Object);

			// Act
			dynamic result = payments.Create(_payment);
			var id = (int) JObject.Parse(result).id;

			// Assert
			Assert.AreEqual(id, 10000000);
		}

		[Test]
		public void ItShouldThrowArgumentExceptionForInvalidPayment()
		{
			// Arrange
			_executer.Setup(e => e.ExecuteCommand(It.IsAny<ExecuteWebRequest>()))
				.Throws(new ArgumentNullException());
			Payments payments = Beanstream.Payments ();
			payments.Repository = new HttpWebRequest(_executer.Object);

			// Act
			var ex = (ArgumentNullException)Assert.Throws(typeof(ArgumentNullException),
				() => payments.Create(null));

			// Assert
			Assert.That(ex.ParamName, Is.EqualTo("payment"));
		}

		[Test]
		public void ItShouldThrowForbiddenExceptionForInvalidCredentials()
		{
			// Arrange
			_executer.Setup(e => e.ExecuteCommand(It.IsAny<ExecuteWebRequest>()))
				.Throws(new ForbiddenException(HttpStatusCode.Forbidden, ""));
			Payments payments = Beanstream.Payments ();
			payments.Repository = new HttpWebRequest(_executer.Object);

			// Act
			var ex = (ForbiddenException)Assert.Throws(typeof(ForbiddenException),
				() => payments.Create(_payment));

			// Assert
			Assert.That(ex.StatusCode, Is.EqualTo((int)HttpStatusCode.Forbidden));
		}

		[Test]
		public void ItShouldThrowUnothorizedExceptionForInvalidPermissions()
		{
			// Arrange
			_executer.Setup(e => e.ExecuteCommand(It.IsAny<ExecuteWebRequest>()))
				.Throws(new UnauthorizedException(HttpStatusCode.Unauthorized, ""));
			Payments payments = Beanstream.Payments ();
			payments.Repository = new HttpWebRequest(_executer.Object);

			// Act
			var ex = (UnauthorizedException)Assert.Throws(typeof(UnauthorizedException),
				() => payments.Create(_payment));

			// Assert
			Assert.That(ex.StatusCode, Is.EqualTo((int)HttpStatusCode.Unauthorized));
		}

		[Test]
		public void ItShouldThrowRuleExceptionForBusinessRuleViolation()
		{
			// Arrange
			_executer.Setup(e => e.ExecuteCommand(It.IsAny<ExecuteWebRequest>()))
				.Throws(new BusinessRuleException(HttpStatusCode.PaymentRequired, ""));
			Payments payments = Beanstream.Payments ();
			payments.Repository = new HttpWebRequest(_executer.Object);

			// Act
			var ex = (BusinessRuleException)Assert.Throws(typeof(BusinessRuleException),
				() => payments.Create(_payment));

			// Assert
			Assert.That(ex.StatusCode, Is.EqualTo((int)HttpStatusCode.PaymentRequired));
		}

		[Test]
		public void ItShouldThrowBadRequestExceptionForInvalidRequest()
		{
			// Arrange
			_executer.Setup(e => e.ExecuteCommand(It.IsAny<ExecuteWebRequest>()))
				.Throws(new InvalidRequestException(HttpStatusCode.PaymentRequired, ""));
			Payments payments = Beanstream.Payments ();
			payments.Repository = new HttpWebRequest(_executer.Object);

			// Act
			var ex = (InvalidRequestException)Assert.Throws(typeof(InvalidRequestException),
				() => payments.Create(_payment));

			// Assert
			Assert.That(ex.StatusCode, Is.EqualTo((int)HttpStatusCode.PaymentRequired));
		}

		[Test]
		public void ItShouldThrowRedirectExceptionFor3DsecureOrIOnline()
		{
			// Arrange
			_executer.Setup(e => e.ExecuteCommand(It.IsAny<ExecuteWebRequest>()))
				.Throws(new RedirectionException(HttpStatusCode.Redirect, ""));
			Payments payments = Beanstream.Payments ();
			payments.Repository = new HttpWebRequest(_executer.Object);

			// Act
			var ex = (RedirectionException)Assert.Throws(typeof(RedirectionException),
				() => payments.Create(_payment));

			// Assert
			Assert.That(ex.StatusCode, Is.EqualTo((int)HttpStatusCode.Redirect));
		}

		[Test]
		public void ItShouldThrowServerExceptionForServerError()
		{
			// Arrange
			_executer.Setup(e => e.ExecuteCommand(It.IsAny<ExecuteWebRequest>()))
				.Throws(new InternalServerException(HttpStatusCode.InternalServerError, ""));
			Payments payments = Beanstream.Payments ();
			payments.Repository = new HttpWebRequest(_executer.Object);

			// Act
			var ex = (InternalServerException)Assert.Throws(typeof(InternalServerException),
				() => payments.Create(_payment));

			// Assert
			Assert.That(ex.StatusCode, Is.EqualTo((int)HttpStatusCode.InternalServerError));
		}

		[Test]
		public void ItShouldThrowCommunicationExceptionForCommunicationError()
		{
			// Arrange
			_executer.Setup(e => e.ExecuteCommand(It.IsAny<ExecuteWebRequest>()))
				.Throws(new CommunicationException("API exception occured", null));
			Payments payments = Beanstream.Payments ();
			payments.Repository = new HttpWebRequest(_executer.Object);

			// Act
			var ex = (CommunicationException)Assert.Throws(typeof(CommunicationException),
				() => payments.Create(_payment));

			// Assert
			Assert.That(ex.Message, Is.EqualTo("API exception occured"));
		}
	}
}
