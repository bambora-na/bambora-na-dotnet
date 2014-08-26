beanstream-dotnet
=================

Integration with Beanstream’s payments gateway is a simple, flexible solution.

You can choose between a straightforward payment requiring very few parameters; or, you can customize a feature-rich integration.

In addition to credit card transactions, Canadian merchants can process INTERAC payments. To assist as a centralized record of all your sales, we also accept cash and cheque transactions.

For very detailed information on the Payments API, look at the Beanstream developer portal's [documentation](http://developer.beanstream.com/documentation/take-payments/purchases-pre-authorizations/).

# Setup
Before you begin making purchases, you need to create a Beanstream API object. It holds your user credentials and provides access to the various APIs.

```c#
Beanstream beanstream = new Beanstream () {
	MerchantId = YOUR_MERCHANT_ID,
	ApiKey = "YOUR_API_KEY",
	ApiVersion = "1"
};
```

# Purchase

Perform a credit card purchase.

```c#
beanstream.Payments.MakeCardPayment (
	new CardPaymentRequest {
		Amount = 100.00,
		OrderNumber = orderNum++.ToString(),
		Card = new Card {
			Name = "John Doe",
			Number = "5100000010001004",
			ExpiryMonth = "12",
			ExpiryYear = "18",
			Cvd = "123"
		}
	}
);
```


