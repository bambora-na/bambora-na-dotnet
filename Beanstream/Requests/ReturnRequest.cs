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

/// <summary>
/// Return a payment for a partial or full amount.
/// </summary>
namespace Beanstream.Api.SDK.Requests
{
	public class ReturnRequest
	{
		[JsonProperty(PropertyName = "payment_id")]
		public string PaymentId { get; set; }

		[JsonProperty(PropertyName = "merchant_id")]
		public string MerchantId { get; set; }

		[JsonProperty(PropertyName = "order_number")]
		public string OrderNumber { get; set; }

		[JsonProperty(PropertyName = "amount")]
		public decimal Amount { get; set; }
	}
}

