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
using Newtonsoft.Json;
using System.Collections.Generic;
using Beanstream.Api.SDK.Domain;

namespace Beanstream.Api.SDK
{
	/// <summary>
	/// Response information when a payment is processed.
	/// </summary>
	public class PaymentResponse : IResponse
	{
		[JsonProperty(PropertyName = "id")]
		public string TransactionId { get; set; }


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

		
		[JsonProperty(PropertyName = "order_number")]
		public string OrderNumber { get; set; }

		
		[JsonProperty(PropertyName = "type")]
		public string TransType { get; set; }

		
		[JsonProperty(PropertyName = "payment_method")]
		public string PaymentMethod { get; set; }

		
		[JsonProperty(PropertyName = "card", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
		public CardResponse Card { get; set; }

		//TODO being implemented
		//[JsonProperty(PropertyName = "swipe", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
		//public Swipe Swipe { get; set; }

		
		[JsonProperty(PropertyName = "links", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
		public IList<Link> Links { get; set; }


	}
}

