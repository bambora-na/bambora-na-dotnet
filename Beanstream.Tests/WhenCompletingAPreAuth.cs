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
	public class WhenCompletingAPreAuth
	{
		private const int TrnId = 10000001;
		private object _payment;
		private Mock<IWebCommandExecuter> _executer;

		[SetUp]
		public void Setup()
		{
			_payment = new
			{
				order_number = "Test1234",
				amount = 100.00
			};

			_executer = new Mock<IWebCommandExecuter>();
		}

		[Test]
		public void ItShouldHaveATransactionIdForASuccessfulPayment()
		{
			// Arrange
			var webresult = new WebCommandResult<string> { Response = @"{""id"":""10000000""}" };

			_executer.Setup(e => e.ExecuteCommand(It.IsAny<ExecuteWebRequest>())).Returns(webresult);

			Beanstream.MerchantId = 100000000;
			Beanstream.ApiKey = "F6EF00BDB80748358D52D8605CDC7027";
			Payments payments = Beanstream.Payments ();
			payments.Repository = new Repository(_executer.Object);

			// Act
			dynamic result = payments.Complete(TrnId,_payment);
			var id = (int)JObject.Parse(result).id;

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
			payments.Repository = new Repository(_executer.Object);

			// Act
			var ex = (ArgumentNullException)Assert.Throws(typeof(ArgumentNullException),
				() => payments.Complete(TrnId, null));

			// Assert
			Assert.That(ex.ParamName, Is.EqualTo("payment"));
		}

		[Test]
		public void ItShouldThrowArgumentExceptionForInvalidTransactionId()
		{
			// Arrange
			_executer.Setup(e => e.ExecuteCommand(It.IsAny<ExecuteWebRequest>()))
				.Throws(new ArgumentNullException());
			Payments payments = Beanstream.Payments ();
			payments.Repository = new Repository(_executer.Object);

			// Act
			var ex = (ArgumentNullException)Assert.Throws(typeof(ArgumentNullException),
				() => payments.Complete(null, _payment));

			// Assert
			Assert.That(ex.ParamName, Is.EqualTo("TransId"));
		}

		[Test]
		public void ItShouldThrowForbiddenExceptionForInvalidCredentials()
		{
			// Arrange
			_executer.Setup(e => e.ExecuteCommand(It.IsAny<ExecuteWebRequest>()))
				.Throws(new ForbiddenException(HttpStatusCode.Forbidden, ""));
			Payments payments = Beanstream.Payments ();
			payments.Repository = new Repository(_executer.Object);

			// Act
			var ex = (ForbiddenException)Assert.Throws(typeof(ForbiddenException),
				() => payments.Complete(TrnId, _payment));

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
			payments.Repository = new Repository(_executer.Object);

			// Act
			var ex = (UnauthorizedException)Assert.Throws(typeof(UnauthorizedException),
				() => payments.Complete(TrnId, _payment));

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
			payments.Repository = new Repository(_executer.Object);

			// Act
			var ex = (BusinessRuleException)Assert.Throws(typeof(BusinessRuleException),
				() => payments.Complete(TrnId, _payment));

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
			payments.Repository = new Repository(_executer.Object);

			// Act
			var ex = (InvalidRequestException)Assert.Throws(typeof(InvalidRequestException),
				() => payments.Complete(TrnId, _payment));

			// Assert
			Assert.That(ex.StatusCode, Is.EqualTo((int)HttpStatusCode.PaymentRequired));
		}

		[Test]
		public void ItShouldThrowServerExceptionForServerError()
		{
			// Arrange
			_executer.Setup(e => e.ExecuteCommand(It.IsAny<ExecuteWebRequest>()))
				.Throws(new InternalServerException(HttpStatusCode.InternalServerError, ""));
			Payments payments = Beanstream.Payments ();
			payments.Repository = new Repository(_executer.Object);

			// Act
			var ex = (InternalServerException)Assert.Throws(typeof(InternalServerException),
				() => payments.Complete(TrnId, _payment));

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
			payments.Repository = new Repository(_executer.Object);

			// Act
			var ex = (CommunicationException)Assert.Throws(typeof(CommunicationException),
				() => payments.Complete(TrnId, _payment));

			// Assert
			Assert.That(ex.Message, Is.EqualTo("API exception occured"));
		}
	}
}