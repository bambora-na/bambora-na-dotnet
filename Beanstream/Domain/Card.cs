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
	public class Card
	{
		/// <summary>
		/// Set to true to process the payment and flag it to be settled. If set to false, then this will be a pre-authorization. 
		/// A pre-auth will not be sent for settlement and the card holder will not be charged until you run the transaction 
		/// again with complete=true.
		/// </summary>
		[JsonProperty(PropertyName = "complete", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
		public bool? Complete { get; set; }

		/// <summary>
		/// For payment profiles, identified if this card is DEF (default) or SEC (secondary).
		/// This value is optional.
		/// </summary>
		[JsonProperty(PropertyName = "function", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
		public string Function { get; set; }

		/// <summary>
		/// Name of the cardholder. Max 64 a/n characters.
		/// </summary>
		[JsonProperty(PropertyName = "name", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
		public string Name { get; set; }

		/// <summary>
		/// Card number. Max 20 digits, no spaces.
		/// </summary>
		[JsonProperty(PropertyName = "number", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
		public string Number { get; set; }

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
		/// Number on the back of the card. Used for online transactions. 4 digits Amex, 3 digits all other cards.
		/// </summary>
		[JsonProperty(PropertyName = "cvd", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
		public string Cvd { get; set; }

		/// <summary>
		/// Identifies VI (visa) MA (mastercard) etc.
		/// You do not have to set this value, the API will set it for you based on the BIN of the card number.
		/// </summary>
		[JsonProperty(PropertyName = "type")]
		public string Type { get; set; }

		/// <summary>
		/// Card ID. You do not set this. It is set when using Payment Profiles and identified the card ID for that profile. Card IDs range from 1 to 5.
		/// </summary>
		/// 
		[JsonProperty(PropertyName = "card_id")]
		public int Id { get; set; }

		public Card () {
			Complete = true;
		}
	}
}

