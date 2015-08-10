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
using Beanstream.Api.SDK.Data;
using Beanstream.Api.SDK.Exceptions;
using Beanstream.Api.SDK;
using Moq;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Beanstream.Api.SDK.Requests;

namespace Beanstream.Api.SDK.Tests
{
	[TestFixture]
	public class WhenVoidingAPayment
	{
		private const string TrnId = "10000001";
		private object _payment;
		private Mock<IWebCommandExecuter> _executer;

		[SetUp]
		public void Setup()
		{
			_payment = new
			{
				order_number = "Test1234"
			};

			_executer = new Mock<IWebCommandExecuter>();
		}

		[Test]
		public void ItShouldHaveATransactionIdForASuccessfulVoid()
		{
			// Arrange
			var webresult = new WebCommandResult<string> { Response = @"{""id"":""10000000""}" };

			_executer.Setup(e => e.ExecuteCommand(It.IsAny<ExecuteWebRequest>())).Returns(webresult);

			Gateway beanstream = new Gateway () {
				MerchantId = 300200578,
				PaymentsApiKey = "4BaD82D9197b4cc4b70a221911eE9f70",
				ApiVersion = "1"
			};
			beanstream.WebCommandExecuter = _executer.Object;

			// Act
			dynamic result = beanstream.Payments.Void("10000000", 10);
			// Assert
			Assert.AreEqual(result.TransactionId, "10000000");
		}

		[Test]
		public void ItShouldThrowArgumentExceptionForInvalidTransactionId()
		{
			// Arrange
			_executer.Setup(e => e.ExecuteCommand(It.IsAny<ExecuteWebRequest>()))
				.Throws(new ArgumentNullException());

			Gateway beanstream = new Gateway () {
				MerchantId = 300200578,
				PaymentsApiKey = "4BaD82D9197b4cc4b70a221911eE9f70",
				ApiVersion = "1"
			};
			beanstream.WebCommandExecuter = _executer.Object;

			// Act
			var ex = (ArgumentNullException)Assert.Throws(typeof(ArgumentNullException),
				() => beanstream.Payments.Void(null, 10));

			// Assert
			Assert.That(ex.ParamName, Is.EqualTo("paymentId"));
		}

		[Test]
		public void ItShouldThrowForbiddenExceptionForInvalidCredentials()
		{
			// Arrange
			_executer.Setup(e => e.ExecuteCommand(It.IsAny<ExecuteWebRequest>()))
				.Throws(new ForbiddenException(HttpStatusCode.Forbidden, "", "", 1, 0));

			Gateway beanstream = new Gateway () {
				MerchantId = 300200578,
				PaymentsApiKey = "4BaD82D9197b4cc4b70a221911eE9f70",
				ApiVersion = "1"
			};
			beanstream.WebCommandExecuter = _executer.Object;

			// Act
			var ex = (ForbiddenException)Assert.Throws(typeof(ForbiddenException),
				() => beanstream.Payments.Void(TrnId, 10));

			// Assert
			Assert.That(ex.StatusCode, Is.EqualTo((int)HttpStatusCode.Forbidden));
		}

		[Test]
		public void ItShouldThrowUnauthorizedExceptionForInvalidPermissions()
		{
			// Arrange
			_executer.Setup(e => e.ExecuteCommand(It.IsAny<ExecuteWebRequest>()))
				.Throws(new UnauthorizedException(HttpStatusCode.Unauthorized, "", "", 1, 0));

			Gateway beanstream = new Gateway () {
				MerchantId = 300200578,
				PaymentsApiKey = "4BaD82D9197b4cc4b70a221911eE9f70",
				ApiVersion = "1"
			};
			beanstream.WebCommandExecuter = _executer.Object;

			// Act
			var ex = (UnauthorizedException)Assert.Throws(typeof(UnauthorizedException),
				() => beanstream.Payments.Void(TrnId, 10));

			// Assert
			Assert.That(ex.StatusCode, Is.EqualTo((int)HttpStatusCode.Unauthorized));
		}

		[Test]
		public void ItShouldThrowRuleExceptionForBusinessRuleViolation()
		{
			// Arrange
			_executer.Setup(e => e.ExecuteCommand(It.IsAny<ExecuteWebRequest>()))
				.Throws(new BusinessRuleException(HttpStatusCode.PaymentRequired, "", "", 1, 0));

			Gateway beanstream = new Gateway () {
				MerchantId = 300200578,
				PaymentsApiKey = "4BaD82D9197b4cc4b70a221911eE9f70",
				ApiVersion = "1"
			};
			beanstream.WebCommandExecuter = _executer.Object;

			// Act
			var ex = (BusinessRuleException)Assert.Throws(typeof(BusinessRuleException),
				() => beanstream.Payments.Void(TrnId, 10));

			// Assert
			Assert.That(ex.StatusCode, Is.EqualTo((int)HttpStatusCode.PaymentRequired));
		}

		[Test]
		public void ItShouldThrowBadRequestExceptionForInvalidRequest()
		{
			// Arrange
			_executer.Setup(e => e.ExecuteCommand(It.IsAny<ExecuteWebRequest>()))
				.Throws(new InvalidRequestException(HttpStatusCode.PaymentRequired, "", "", 1, 0));

			Gateway beanstream = new Gateway () {
				MerchantId = 300200578,
				PaymentsApiKey = "4BaD82D9197b4cc4b70a221911eE9f70",
				ApiVersion = "1"
			};
			beanstream.WebCommandExecuter = _executer.Object;

			// Act
			var ex = (InvalidRequestException)Assert.Throws(typeof(InvalidRequestException),
				() => beanstream.Payments.Void(TrnId, 10));

			// Assert
			Assert.That(ex.StatusCode, Is.EqualTo((int)HttpStatusCode.PaymentRequired));
		}

		[Test]
		public void ItShouldThrowServerExceptionForServerError()
		{
			// Arrange
			_executer.Setup(e => e.ExecuteCommand(It.IsAny<ExecuteWebRequest>()))
				.Throws(new InternalServerException(HttpStatusCode.InternalServerError, "", "", 1, 0));

			Gateway beanstream = new Gateway () {
				MerchantId = 300200578,
				PaymentsApiKey = "4BaD82D9197b4cc4b70a221911eE9f70",
				ApiVersion = "1"
			};
			beanstream.WebCommandExecuter = _executer.Object;

			// Act
			var ex = (InternalServerException)Assert.Throws(typeof(InternalServerException),
				() => beanstream.Payments.Void(TrnId, 10));

			// Assert
			Assert.That(ex.StatusCode, Is.EqualTo((int)HttpStatusCode.InternalServerError));
		}

		[Test]
		public void ItShouldThrowCommunicationExceptionForCommunicationError()
		{
			// Arrange
			_executer.Setup(e => e.ExecuteCommand(It.IsAny<ExecuteWebRequest>()))
				.Throws(new CommunicationException("API exception occured", null));

			Gateway beanstream = new Gateway () {
				MerchantId = 300200578,
				PaymentsApiKey = "4BaD82D9197b4cc4b70a221911eE9f70",
				ApiVersion = "1"
			};
			beanstream.WebCommandExecuter = _executer.Object;

			// Act
			var ex = (CommunicationException)Assert.Throws(typeof(CommunicationException),
				() => beanstream.Payments.Void(TrnId, 10));

			// Assert
			Assert.That(ex.Message, Is.EqualTo("API exception occured"));
		}
	}
}