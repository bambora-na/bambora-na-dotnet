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
using Newtonsoft.Json;

/// <summary>
/// An address, used for Billing or Shipping which are the non-abstract implementations.
/// The only required field is the email address.
/// </summary>
namespace Bambora.NA.SDK.Domain
{
	public class Address
	{
		/// <summary>
		/// The person's name for this address. Max 64 a/n characters.
		/// Required if setting an address.
		/// </summary>
		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }

		/// <summary>
		/// Unique street address for billing purposes. Max 64 a/n characters
		/// Required if setting an address.
		/// </summary>
		[JsonProperty(PropertyName = "address_line1")]
		public string AddressLine1 { get; set; }

		/// <summary>
		/// Optional variable for longer addresses. Max 64 a/n characters.
		/// Required if setting an address.
		/// </summary>
		[JsonProperty(PropertyName = "address_line2")]
		public string AddressLine2 { get; set; }

		/// <summary>
		/// Customer's billing city. Max 32 a/n characters.
		/// Required if setting an address.
		/// </summary>
		[JsonProperty(PropertyName = "city")]
		public string City { get; set; }

		/// <summary>
		/// The province code. 2 characters. Province codes must match the standards in ISO 3166-2:CA. State ID codes must match the standards in ISO 3166-2:US.
		/// Required if setting an address.
		/// </summary>
		[JsonProperty(PropertyName = "province")]
		public string Province { get; set; }

		/// <summary>
		/// Country. 2 characters. Country codes must match the standards in ISO 3166-2.
		/// Required if setting an address.
		/// </summary>
		[JsonProperty(PropertyName = "country")]
		public string Country { get; set; }

		/// <summary>
		/// Customer’s postal code for billing purposes. 16 a/n characters. Spaces allowed. Case-insensitive.
		/// Required if setting an address.
		/// </summary>
		[JsonProperty(PropertyName = "postal_code")]
		public string PostalCode { get; set; }

		/// <summary>
		/// Customer phone number for order follow-up. Min 7 a/n characters, Max 32 a/n characters.
		/// Optional
		/// </summary>
		[JsonProperty(PropertyName = "phone_number")]
		public string PhoneNumber { get; set; }

		/// <summary>
		/// Specified email address is used to send automated email receipts.
		/// Max 64 a/n characters in the format a@b.com.
		/// Required
		/// </summary>
		/// <value>The email address.</value>
		[JsonProperty(PropertyName = "email_address")]
		public string EmailAddress { get; set; }
	}
}

