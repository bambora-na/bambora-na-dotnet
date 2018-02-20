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

namespace Bambora.NA.SDK
{
	/// <summary>
	/// The result of storing a card on a payment profile or from a credit card purchase.
	/// </summary>
	public class CardResponse
	{

		[JsonProperty(PropertyName = "last_four", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
		public string LastFour { get; set; }

		[JsonProperty(PropertyName = "cvd_match", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
		public int? CvdMatch { get; set; }

		[JsonProperty(PropertyName = "address_match", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
		public int? AddressMatch { get; set; }

		[JsonProperty(PropertyName = "postal_result", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
		public int? PostalMatch { get; set; }

		[JsonProperty(PropertyName = "avs_result", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
		public string AvsResult { get; set; }

		[JsonProperty(PropertyName = "cvd_result", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
		public string CvdResult { get; set; }

		[JsonProperty(PropertyName = "card_type", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
		public string CardType { get; set; }

	}
}

