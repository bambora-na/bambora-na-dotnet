// The MIT License (MIT)
//
// Copyright (c) 2018 Bambora, Inc.
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
using Bambora.NA.SDK.Data;
using Bambora.NA.SDK.Exceptions;
using Bambora.NA.SDK;
using Moq;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Bambora.NA.SDK.Requests;
using Bambora.NA.SDK.Domain;

namespace Bambora.NA.SDK.Tests
{
	[TestFixture]
	public class WhenCreatingAPayment
	{
		private CardPaymentRequest _cardPaymentRequest;
		private Mock<IWebCommandExecuter> _executer;
        private Gateway _bambora;
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
			_cardPaymentRequest = new CardPaymentRequest {
				Amount = 40.00M,
				OrderNumber = "asdfghjkl00001",
				Card = new Card {
					Name = "John Doe",
					Number = "5100000010001004",
					ExpiryMonth = "12",
					ExpiryYear = "18",
					Cvd = "123"
				}
			};

			_executer = new Mock<IWebCommandExecuter>();

            _bambora = new Gateway()
            {
                MerchantId = 300200578,
                PaymentsApiKey = "4BaD82D9197b4cc4b70a221911eE9f70",
                ReportingApiKey = "4e6Ff318bee64EA391609de89aD4CF5d",
                ProfilesApiKey = "D97D3BE1EE964A6193D17A571D9FBC80",
                ApiVersion = "1"
            };
        }

		[Test]
		public void ItShouldHaveATransactionIdForASuccessfulPayment()
		{
			// Arrange
			var webresult = new WebCommandResult<string>{Response = @"{""id"":""10000000""}"};
			
			_executer.Setup(e => e.ExecuteCommand(It.IsAny<ExecuteWebRequest>())).Returns(webresult);

			_bambora.WebCommandExecuter = _executer.Object;

			// Act
			PaymentResponse response = _bambora.Payments.MakePayment (_cardPaymentRequest);


			// Assert
			Assert.AreEqual(response.TransactionId, "10000000");
		}

		[Test]
		public void ItShouldThrowArgumentExceptionForInvalidPayment()
		{
			// Arrange
			_executer.Setup(e => e.ExecuteCommand(It.IsAny<ExecuteWebRequest>()))
				.Throws(new ArgumentNullException());

			_bambora.WebCommandExecuter = _executer.Object;

			// Act
			var ex = (ArgumentNullException)Assert.Throws(typeof(ArgumentNullException),
				() => _bambora.Payments.MakePayment(null));

			// Assert
			Assert.That(ex.ParamName, Is.EqualTo("paymentRequest"));
		}

		[Test]
		public void ItShouldThrowForbiddenExceptionForInvalidCredentials()
		{
			// Arrange
			_executer.Setup(e => e.ExecuteCommand(It.IsAny<ExecuteWebRequest>()))
				.Throws(new ForbiddenException(HttpStatusCode.Forbidden, "", "", 1, 0));

			_bambora.WebCommandExecuter = _executer.Object;

			// Act
			var ex = (ForbiddenException)Assert.Throws(typeof(ForbiddenException),
				() => _bambora.Payments.MakePayment(_cardPaymentRequest));

			// Assert
			Assert.That(ex.StatusCode, Is.EqualTo((int)HttpStatusCode.Forbidden));
		}

		[Test]
		public void ItShouldThrowUnauthorizedExceptionForInvalidPermissions()
		{
			// Arrange
			_executer.Setup(e => e.ExecuteCommand(It.IsAny<ExecuteWebRequest>()))
				.Throws(new UnauthorizedException(HttpStatusCode.Unauthorized, "", "", 1, 0));

			_bambora.WebCommandExecuter = _executer.Object;

			// Act
			var ex = (UnauthorizedException)Assert.Throws(typeof(UnauthorizedException),
				() => _bambora.Payments.MakePayment(_cardPaymentRequest));

			// Assert
			Assert.That(ex.StatusCode, Is.EqualTo((int)HttpStatusCode.Unauthorized));
		}

		[Test]
		public void ItShouldThrowRuleExceptionForBusinessRuleViolation()
		{
			// Arrange
			_executer.Setup(e => e.ExecuteCommand(It.IsAny<ExecuteWebRequest>()))
				.Throws(new BusinessRuleException(HttpStatusCode.PaymentRequired, "", "", 1, 0));

			_bambora.WebCommandExecuter = _executer.Object;

			// Act
			var ex = (BusinessRuleException)Assert.Throws(typeof(BusinessRuleException),
				() => _bambora.Payments.MakePayment(_cardPaymentRequest));

			// Assert
			Assert.That(ex.StatusCode, Is.EqualTo((int)HttpStatusCode.PaymentRequired));
		}

		[Test]
		public void ItShouldThrowBadRequestExceptionForInvalidRequest()
		{
			// Arrange
			_executer.Setup(e => e.ExecuteCommand(It.IsAny<ExecuteWebRequest>()))
				.Throws(new InvalidRequestException(HttpStatusCode.PaymentRequired, "", "", 1, 0));

			_bambora.WebCommandExecuter = _executer.Object;

			// Act
			var ex = (InvalidRequestException)Assert.Throws(typeof(InvalidRequestException),
				() => _bambora.Payments.MakePayment(_cardPaymentRequest));

			// Assert
			Assert.That(ex.StatusCode, Is.EqualTo((int)HttpStatusCode.PaymentRequired));
		}

		[Test]
		public void ItShouldThrowRedirectExceptionFor3DsecureOrIOnline()
		{
			// Arrange
			_executer.Setup(e => e.ExecuteCommand(It.IsAny<ExecuteWebRequest>()))
				.Throws(new RedirectionException(HttpStatusCode.Redirect, "", "", 1, 0));

			_bambora.WebCommandExecuter = _executer.Object;


			// Act
			var ex = (RedirectionException)Assert.Throws(typeof(RedirectionException),
				() => _bambora.Payments.MakePayment(_cardPaymentRequest));

			// Assert
			Assert.That(ex.StatusCode, Is.EqualTo((int)HttpStatusCode.Redirect));
		}

		[Test]
		public void ItShouldThrowServerExceptionForServerError()
		{
			// Arrange
			_executer.Setup(e => e.ExecuteCommand(It.IsAny<ExecuteWebRequest>()))
				.Throws(new InternalServerException(HttpStatusCode.InternalServerError, "", "", 1, 0));

			_bambora.WebCommandExecuter = _executer.Object;


			// Act
			var ex = (InternalServerException)Assert.Throws(typeof(InternalServerException),
				() => _bambora.Payments.MakePayment(_cardPaymentRequest));

			// Assert
			Assert.That(ex.StatusCode, Is.EqualTo((int)HttpStatusCode.InternalServerError));
		}

		[Test]
		public void ItShouldThrowCommunicationExceptionForCommunicationError()
		{
			// Arrange
			_executer.Setup(e => e.ExecuteCommand(It.IsAny<ExecuteWebRequest>()))
				.Throws(new CommunicationException("API exception occured", null));

			_bambora.WebCommandExecuter = _executer.Object;

			// Act
			var ex = (CommunicationException)Assert.Throws(typeof(CommunicationException),
				() => _bambora.Payments.MakePayment(_cardPaymentRequest));

			// Assert
			Assert.That(ex.Message, Is.EqualTo("API exception occured"));
		}
	}
}
