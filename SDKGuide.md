
REST Process Transaction .NET SDK Guide
=======================================


## Table Of Contents

1. [Overview](#Overview)
2. [Prerequesite](#Prerequesite)
3. [Usage and Operations](#Usage-and-Operations)
4. [Purchase](#Purchase)
5. [Return](#Return)
6. [Pre-Authorization (Authorization)](#Pre-Authorization-(Authorization))
7. [Pre-Authorization Complete (Capture)](#Pre-Authorization-Complete-(Capture))
8. [Redirections](#Redirections)

## Overview
This documents is a brief outline of how to use the Bambora .NET SDK to process transactions.

## Prerequesite
* A Bambora test account  
* Visual Studio 2015 and up  
* .NET Framework 4.5 and up. SDK is available as Nuget package for .NET 4.5, 4.6.1 and .NET standard 2.0

## Usage and Operations

```
using Bambora.NA.SDK;

...

Gateway bambora = new Gateway()
{
    MerchantId = 300200578,
    PaymentsApiKey = "4BaD82D9197b4cc4b70a221911eE9f70",
    ApiVersion = "1"
};

```


# Purchase

## Credit Card

```c#
bambora.MerchantId = 276790000;
bambora.Passcode = "6EF5C0Db8E89410E8835433A54f169c2";
var payment = new {
    order_number = "ABC1234567890,
    amount = 100.00,
    payment_method = "card",
    card = new {
        name = "John Doe",
        number = "5100000010001004",
        expiry_month = "12",
        expiry_year = "18",
        cvd = "123"
    },
    billing = new {
        name = "John Doe",
        address_line1 = "2659 Douglas Street",
        address_line2 = "302",
        city = "Victoria",
        province = "BC",
        country = "CA",
        postal_code = "V8T4M3",
        phone_number = "2501231234",
        email_address = "johndoe@bambora.com"
    },
    shipping = new {
        name = "John Doe",
        address_line1 = "2659 Douglas Street",
        address_line2 = "302",
        city = "Victoria",
        province = "BC",
        country = "CA",
        postal_code = "V8T4M3",
        phone_number = "2501231234",
        email_address = "johndoe@bambora.com"
    },
    comments= "create a payment"
};
try
{
    var result = bambora.Payments.Create(payment);
}
catch...

```

## Payment Profile
```c#
bambora.MerchantId = 276790000;
bambora.Passcode = "6EF5C0Db8E89410E8835433A54f169c2";
var payment = new {
    order_number = "ABC1234567890,
    amount = 100.00,
    payment_method = "payment_profile",
    payment_profile = new
    {
        card_id = 1,
        customer_code = "D675855E81b448a7bF0dD682DF74e613"
    },
    comments= "create a payment"
};
try
{
    var result = bambora.Payments.Create(payment);
}
catch...
```

## Token 
```c#
bambora.MerchantId = 276790000;
bambora.Passcode = "6EF5C0Db8E89410E8835433A54f169c2";
var payment = new {
    order_number = "ABC1234567890,
    amount = 100.00,
    payment_method = "token",
    token = new {
        name = "John Doe",
        code = "aaa-91ced41c-a2c0-4b1b-a838-cf62bbfdda02"
    },
    billing = new {
        name = "John Doe",
        address_line1 = "2659 Douglas Street",
        address_line2 = "302",
        city = "Victoria",
        province = "BC",
        country = "CA",
        postal_code = "V8T4M3",
        phone_number = "2501231234",
        email_address = "johndoe@bambora.com"
    },
    shipping = new {
        name = "John Doe",
        address_line1 = "2659 Douglas Street",
        address_line2 = "302",
        city = "Victoria",
        province = "BC",
        country = "CA",
        postal_code = "V8T4M3",
        phone_number = "2501231234",
        email_address = "johndoe@bambora.com"
    },
    comments= "create a payment"
};
try
{
    var result = bambora.Payments.Create(payment);
}
catch...
```

## Cash
``` c#
bambora.MerchantId = 276790000;
bambora.Passcode = "6EF5C0Db8E89410E8835433A54f169c2";
var payment = new
{
order_number = "ABC1234567890,
amount = 100.00,
payment_method = "cash",
comments= "create a payment"
};
try
{
var result = bambora.Payments.Create(payment);
}
catch...

```

## Cheque

```c#
bambora.MerchantId = 276790000;
bambora.Passcode = "6EF5C0Db8E89410E8835433A54f169c2";
var payment = new
{
order_number = "ABC1234567890,
amount = 100.00,
payment_method = "cheque",
comments= "create a payment"
};
try
{
var result = bambora.Payments.Create(payment);
}
catch...
```

## Interac (First Request)
```
bambora.MerchantId = 276790000;
bambora.Passcode = "6EF5C0Db8E89410E8835433A54f169c2";
var payment = new
{
order_number = "ABC1234567890,
amount = 100.00,
payment_method = "interac",
billing = new
{
name = "John Doe",
address_line1 = "2659 Douglas Street",
address_line2 = "302",
city = "Victoria",
province = "BC",
country = "CA",
postal_code = "V8T4M3",
phone_number = "2501231234",
email_address = "johndoe@bambora.com"
},
comments= "create a payment"
};
try
{
var result = bambora.Payments.Create(payment);
}
catch...
```

# Return
Credit Card, Payment Profile, Cash, Cheque, Interac, Token
```
bambora.MerchantId = 276790000;
bambora.Passcode = "6EF5C0Db8E89410E8835433A54f169c2";
var payment = new
{
order_number = "ABC1234567890,
amount = 100.00
};
try
{
var transId = 10000001;
var result = bambora.Payments.Return(transId , payment);
}
catch...
```

# Pre-Authorization (Authorization)
##  Credit Card

```
bambora.MerchantId = 276790000;
bambora.Passcode = "6EF5C0Db8E89410E8835433A54f169c2";
var payment = new {
    order_number = "ABC1234567890,
    amount = 100.00,
    payment_method = "card",
    card = new {
        complete = false,
        name = "John Doe",
        number = "5100000010001004",
        expiry_month = "12",
        expiry_year = "18",
        cvd = "012"
    },
    billing = new {
        name = "John Doe",
        address_line1 = "2659 Douglas Street",
        address_line2 = "302",
        city = "Victoria",
        province = "BC",
        country = "CA",
        postal_code = "V8T4M3",
        phone_number = "2501231234",
        email_address = "johndoe@bambora.com"
    },

    shipping = new {
        name = "John Doe",
        address_line1 = "2659 Douglas Street",
        address_line2 = "302",
        city = "Victoria",
        province = "BC",
        country = "CA",
        postal_code = "V8T4M3",
        phone_number = "2501231234",
        email_address = "johndoe@bambora.com"
    },
    comments= "create a payment"
};
try
{
    var result = bambora.Payments.Create(payment);
}
catch...
```

## Payment Profile

```
bambora.MerchantId = 276790000;
bambora.Passcode = "6EF5C0Db8E89410E8835433A54f169c2";
var payment = new {
    order_number = "ABC1234567890,
    amount = 100.00,
    payment_method = "payment_profile",
    payment_profile = new {
        complete = false,
        card_id = 1,
        customer_code = "D675855E81b448a7bF0dD682DF74e613"
    },
    comments= "create a payment"
};
try
{
    var result = bambora.Payments.Create(payment);
}
catch...
```

## Token
```
bambora.MerchantId = 276790000;
bambora.Passcode = "6EF5C0Db8E89410E8835433A54f169c2";
var payment = new {
    order_number = "ABC1234567890,
    amount = 100.00,
    payment_method = "token",
    token = new {
        complete = false,
        name = "John Doe",
        code = "aaa-91ced41c-a2c0-4b1b-a838-cf62bbfdda02"
        },
    billing = new {
        name = "John Doe",
        address_line1 = "2659 Douglas Street",
        address_line2 = "302",
        city = "Victoria",
        province = "BC",
        country = "CA",
        postal_code = "V8T4M3",
        phone_number = "2501231234",
        email_address = "johndoe@bambora.com"
    },
    shipping = new {
        name = "joh doe",
        address_line1 = "2659 Douglas Street",
        address_line2 = "302",
        city = "Victoria",
        province = "BC",
        country = "CA",
        postal_code = "V8T4M3",
        phone_number = "2501231234",
        email_address = "johndoe@bambora.com"
    },
    comments= "create a payment"
};
try
{
    var result = bambora.Payments.Create(payment);
}
catch...
```

# Pre-Authorization Complete (Capture)
Credit Card, PaymentProfile, Token
```
bambora.MerchantId = 276790000;
bambora.Passcode = "6EF5C0Db8E89410E8835433A54f169c2";
var payment = new
    {
        order_number = "ABC1234567890,
        amount = 100.00
    };
try
{
    var transId = 10000001;
    var result = bambora.Payments.Complete(transId , payment);
}
catch...
```

# Void 
Credit Card, PaymentProfile, Token
```
bambora.MerchantId = 276790000;
bambora.Passcode = "6EF5C0Db8E89410E8835433A54f169c2";
var payment = new
{
    order_number = "ABC1234567890
};
try
{
    var transId = 10000001;
    var result = bambora.Payments.Void(transId , payment);
}
catch...
```

# Redirections
## 3D secure cards
```
bambora.MerchantId = 276790000;
bambora.Passcode = "6EF5C0Db8E89410E8835433A54f169c2";
var payment = new {
        payment_method = "card",
        card_response = new {
                pa_res = "TEST_PaRes"
            }
    };
try
{
    var merchantData = "45AA2840-C435-461A-B014-9AE5EA477BAD";
    var result = bambora.Payments.Continue(merchantData, payment);
}
catch...
```

## Interac
```
bambora.MerchantId = 276790000;
bambora.Passcode = "6EF5C0Db8E89410E8835433A54f169c2";
var payment = new
{
payment_method = "interac",
interac_response = new
{
funded = 1,
idebit_track2 = "3728024906540591214=14010123456789XYZ",
idebit_isslang = "en",
idebit_version = 1,
idebit_issconf = "CONF#TEST",
idebit_issname = "TestBank2",
idebit_amount = 10000,
idebit_invoice = "10000123"
}
};
try
{
var merchantData = "45AA2840-C435-461A-B014-9AE5EA477BAD";
var result = bambora.Payments.Continue(merchantData, payment);
}
catch...
```

# Exceptions
```
catch (InvalidRequestException ex)
{
    //Display message to the user
    var response = ex.StatusCode + " " + ex.Response;
}

catch (BusinessRuleException ex)
{
    //Display a generic error to the user, log and investigate
    var response = ex.Response;
}

catch (UnauthorizedException ex)
{
    //Display a generic error to the user, log and fix
    var response = ex.Response;
}

catch (ForbiddenException ex)
{
    //Display a generic error to the user, log and fix
    var response = ex.Response;
}

catch (InternalServerException ex)
{
    // Display a generic error to the user, log and investigate
    var response = ex.Response;
}

catch (RedirectionException ex)
{
    //Redirect user using the response contents
    var response = ex.Response;
    //Response.Write(HttpUtility.UrlDecode(ex.Response));
}

catch (CommunicationException ex)
{
    var response = ex.Message;
}

catch (Exception ex)
{
    var response = ex.Message;
}
```
