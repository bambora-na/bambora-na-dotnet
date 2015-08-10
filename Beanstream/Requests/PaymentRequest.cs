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
	public abstract class PaymentRequest
	{
		[JsonProperty(PropertyName = "payment_method")]
		public string PaymentMethod { get; set; }

		//[JsonProperty(PropertyName = "merchant_id")]
		//public int MerchantId { get; set; }

		[JsonProperty(PropertyName = "order_number")]
		public string OrderNumber { get; set; }

		[JsonProperty(PropertyName = "amount")]
		public Double Amount { get; set; }

		[JsonProperty(PropertyName = "language")]
		public string Language { get; set; }

		[JsonProperty(PropertyName = "term_url")]
		public string TerminalUrl { get; set; }

		[JsonProperty(PropertyName = "customer_ip")]
		public string CustomerIp { get; set; }

		[JsonProperty(PropertyName = "comments")]
		public string Comments { get; set; }

		[JsonProperty(PropertyName = "billing")]
		public Address Billing { get; set; }

		[JsonProperty(PropertyName = "shipping")]
		public Address Shipping { get; set; }

		[JsonProperty(PropertyName = "custom")]
		public CustomFields CustomFields { get; set; }

	}
}

