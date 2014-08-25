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

namespace Beanstream
{
	public class Card
	{
		[JsonProperty(PropertyName = "complete", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
		public bool? Complete { get; set; }

		[JsonProperty(PropertyName = "card_id", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
		public string CardId { get; set; }

		[JsonProperty(PropertyName = "function", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
		public string Function { get; set; }

		[JsonProperty(PropertyName = "name", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
		public string Name { get; set; }

		[JsonProperty(PropertyName = "number", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
		public string Number { get; set; }

		[JsonProperty(PropertyName = "expiry_month", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
		public string ExpiryMonth { get; set; }

		[JsonProperty(PropertyName = "expiry_year", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
		public string ExpiryYear { get; set; }

		[JsonProperty(PropertyName = "cvd", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
		public string Cvd { get; set; }

		[JsonProperty(PropertyName = "card_type")]
		public string CardType { get; set; }


		public Card () {
			Complete = true;
		}
	}
}

