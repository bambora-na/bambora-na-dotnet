bambora-na-dotnet
=================

Integration with Bambora’s payments gateway is a simple, flexible solution.

You can choose between a straightforward payment requiring very few parameters; or, you can customize a feature-rich integration.

To assist as a centralized record of all your sales, we also accept cash and cheque transactions.

For very detailed information on the Payments API, look at the Bambora developer portal's [documentation](https://dev.na.bambora.com/docs/references/payment_SDKs/take_payments/).

## Nuget packages
SDK is available as Nuget package for .NET 4.5, 4.6.1 and .NET standard 2.0

## Setup
Before you begin making purchases, you need to create a Bambora API object. It holds your user credentials and provides access to the various APIs.

```c#
using Bambora.NA.SDK;
...

Gateway bambora = new Gateway () {
	MerchantId = YOUR_MERCHANT_ID,
	PaymentsApiKey = "YOUR_API_KEY",
	ApiVersion = "1"
};
```
For more details, please refer to included **SDKGuide** document and sample application

## Purchase

Below is complete working example how to make credit card purchase.


```c#
using System;
using Bambora.NA.SDK;
using Bambora.NA.SDK.Requests;
using Bambora.NA.SDK.Domain;

namespace Bambora.NA.SDK.Demo
{
    class Program
   
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("BEGIN running sample transactions");
            // Payments API
            ProcessPayment();            
            Console.WriteLine("FINISHED running sample transactions");
        }

        static string ProcessPayment()
        {

            Console.WriteLine("Processing Payment... ");

            Gateway bambora = new Gateway()
            {
                MerchantId = 300200578,
                PaymentsApiKey = "4BaD82D9197b4cc4b70a221911eE9f70",
                ApiVersion = "1"
            };

            PaymentResponse response = bambora.Payments.MakePayment(
                new CardPaymentRequest
                {
                    Amount = 100.00M,
                    OrderNumber = getRandomOrderId("test"),
                    Card = new Card
                    {
                        Name = "John Doe",
                        Number = "5100000010001004",
                        ExpiryMonth = "12",
                        ExpiryYear = "18",
                        Cvd = "123"
                    }
                }
            );

            Console.WriteLine("Payment id: " + response.TransactionId + ", " + response.Message + "\n");
            Console.WriteLine(response.TransType);

            return response.TransactionId;
        }
        private static string getRandomOrderId(string prefix)
        {
            DateTime datetime = DateTime.Now;
            double seconds = (datetime - new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds;            
            string orderId = prefix + "_" + seconds;
            if (orderId.Length > 30)
                orderId = orderId.Substring(0, 29);
            return orderId;
        }
    }
}

```





