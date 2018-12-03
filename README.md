bambora-na-dotnet
=================

Integration with Bambora’s payments gateway is a simple, flexible solution.

You can choose between a straightforward payment requiring very few parameters; or, you can customize a feature-rich integration.

To assist as a centralized record of all your sales, we also accept cash and cheque transactions.

For very detailed information on the Payments API, look at the Bambora developer portal's [documentation](https://dev.na.bambora.com/docs/references/payment_SDKs/take_payments/).

## Nuget packages
SDK is available as Nuget package for .NET 4.5, 4.6.1 and .NET standard 2.0. 

Latest version of SDK is 2.1.0 and you can get it [here](https://www.nuget.org/packages/Bambora.NA.SDK/)

## Version history

New in 2.1.0
* Merged community pull request #26 to allow TLS 1.0/1.1 so that calling application can use other services which do not support TLS 1.2
* PaymentResponse.Approved property is made public (community pull request #17)
* Bambora.NA.SDK.BamboraUrls.BaseUrl made public so you can change APi adderss in order to test your integration (see sample below for usage)

New in 2.0.0
* Updated to support TLS 1.2
* Requires .NET 4.5 or later
* Rebranded from Beanstream to Bambora. This is a breaking change (all namespaces renamed from Beanstream to Bambora)


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
            
            //To point this sample application to TLS 1.2 ONLY server, uncomment line below
            //Bambora.NA.SDK.BamboraUrls.BaseUrl = "https://tls12-api.na.bambora.com";

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
