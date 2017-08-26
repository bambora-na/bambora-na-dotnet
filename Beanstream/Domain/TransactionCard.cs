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

/// <summary>
/// A Credit Card. Some of the values on this object are set by the API and do not need to be set by the implementor.
/// </summary>
namespace Beanstream.Api.SDK.Domain
{
	public class TransactionCard
	{
		/// <summary>
		/// Name of the cardholder. Max 64 a/n characters.
		/// </summary>
		[JsonProperty(PropertyName = "name", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
		public string Name { get; set; }

		/// <summary>
		/// Masked Card number. Last 4 digits of card number.
		/// </summary>
		[JsonProperty(PropertyName = "last_four", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
		public string LastFour { get; set; }

		/// <summary>
		/// 2 digit expiry month. (January = 01)
		/// </summary>
		[JsonProperty(PropertyName = "expiry_month", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
		public string ExpiryMonth { get; set; }

		/// <summary>
		/// 2 digit expiry year. (2016=16)
		/// </summary>
		[JsonProperty(PropertyName = "expiry_year", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
		public string ExpiryYear { get; set; }


		/// <summary>
		/// Identifies VI (visa) MA (mastercard) etc.
		/// You do not have to set this value, the API will set it for you based on the BIN of the card number.
		/// </summary>
		[JsonProperty(PropertyName = "card_type")]
		public string Type { get; set; }

		[JsonProperty(PropertyName = "avs_result", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
		public string AvsResult { get; set; }

		[JsonProperty(PropertyName = "cvd_result", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
		public string CvdResult { get; set; }

		public TransactionCard()
		{
		}
	}
}

