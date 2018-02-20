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
using Bambora.SDK.Domain;
using System.Collections.Generic;

namespace Bambora.SDK
{
	public class Transaction
	{
		[JsonProperty(PropertyName = "id")]
		public string Id { get; set; }

		[JsonProperty(PropertyName = "approved")]
		public string Approved { get; set; }

		[JsonProperty(PropertyName = "message_id")]
		public string MessageId { get; set; }

		[JsonProperty(PropertyName = "message")]
		public string Message { get; set; }

		[JsonProperty(PropertyName = "auth_code")]
		public string AuthCode { get; set; }

		[JsonProperty(PropertyName = "created")]
		public DateTime Created { get; set; }

		[JsonProperty(PropertyName = "amount")]
		public string Amount { get; set; }

		[JsonProperty(PropertyName = "order_number")]
		public string OrderNumber { get; set; }

		[JsonProperty(PropertyName = "type")]
		public string Type { get; set; }

		[JsonProperty(PropertyName = "comments")]
		public string Comments { get; set; }

		[JsonProperty(PropertyName = "batch_number")]
		public string BatchNumber { get; set; }

		[JsonProperty(PropertyName = "total_refunds")]
		public string TotalRefunds { get; set; }

		[JsonProperty(PropertyName = "total_completions")]
		public string TotalCompletions { get; set; }

		[JsonProperty(PropertyName = "payment_method")]
		public string PaymentMethod { get; set; }

		[JsonProperty(PropertyName = "card")]
		public Card Card { get; set; }

		[JsonProperty(PropertyName = "billing")]
		public Address Billing { get; set; }

		[JsonProperty(PropertyName = "shipping")]
		public Address Shipping { get; set; }

		[JsonProperty(PropertyName = "custom")]
		public CustomFields CustomFields { get; set; }

		[JsonProperty(PropertyName = "adjusted_by")]
		public IList<Adjustment> Adjustments { get; set; }

		[JsonProperty(PropertyName = "links", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
		public IList<Link> Links { get; set; }
	}
}

