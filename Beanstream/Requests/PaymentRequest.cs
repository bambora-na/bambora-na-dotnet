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
using Beanstream.Api.SDK.Requests;
using Newtonsoft.Json;
using Beanstream.Api.SDK.Domain;

namespace Beanstream.Api.SDK.Requests
{
	public class PaymentRequest
	{
		/// <summary>
		/// This is set automatically by the PaymentsAPI class. You do not need to set it.
		/// </summary>
		[JsonProperty(PropertyName = "payment_method")]
		public string PaymentMethod { get; set; }

		/// <summary>
		/// Include a unique order reference number. 30 alphanumeric (a/n) characters.
		/// Optional
		/// </summary>
		[JsonProperty(PropertyName = "order_number")]
		public string OrderNumber { get; set; }

		/// <summary>
		/// In the format 0.00. Max 2 decimal places. Max 9 digits total.
		/// </summary>
		[JsonProperty(PropertyName = "amount")]
		public decimal Amount { get; set; }

		/// <summary>
		/// 3 characters, either ENG or FRE.
		/// Optional
		/// </summary>
		[JsonProperty(PropertyName = "language")]
		public string Language { get; set; }

		/// <summary>
		/// Optional field for terminal payments
		/// </summary>
		/// <value>The terminal URL.</value>
		[JsonProperty(PropertyName = "term_url")]
		public string TerminalUrl { get; set; }

		/// <summary>
		/// IP address of the customer.
		/// Optional.
		/// </summary>
		/// <value>The customer ip.</value>
		[JsonProperty(PropertyName = "customer_ip")]
		public string CustomerIp { get; set; }

		/// <summary>
		/// Gets or sets the comments. 256 characters
		/// Optional.
		/// </summary>
		/// <value>The comments.</value>
		[JsonProperty(PropertyName = "comments")]
		public string Comments { get; set; }

		/// <summary>
		/// Billing address.
		/// Optional
		/// </summary>
		/// <value>The billing.</value>
		[JsonProperty(PropertyName = "billing")]
		public Address Billing { get; set; }

		/// <summary>
		/// Shipping address of the customer.
		/// Optional
		/// </summary>
		/// <value>The shipping.</value>
		[JsonProperty(PropertyName = "shipping")]
		public Address Shipping { get; set; }

		/// <summary>
		/// Optional
		/// </summary>
		/// <value>The custom fields.</value>
		[JsonProperty(PropertyName = "custom")]
		public CustomFields CustomFields { get; set; }

	}
}

