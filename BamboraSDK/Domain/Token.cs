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
/// Tokens are a representation of a credit card. Usually you would convert a card to a token in the client-side app, then send the
/// token to your server. This allows you to not have credit card information touch your code or network, thus lowering your
/// PCI scope. (ie. less paperwork and less risk).
/// </summary>
namespace Bambora.NA.SDK.Domain
{
	public class Token
	{
		/// <summary>
		/// Set to False to make a purchase with the token be a pre-authorization.
		/// Required
		/// </summary>
		/// <value><c>true</c> if complete; otherwise, <c>false</c>.</value>
		[JsonProperty(PropertyName = "complete")]
		public bool Complete { get; set; }

		/// <summary>
		/// Cardholder name. 32 characters
		/// Required
		/// </summary>
		/// <value>The name.</value>
		[JsonProperty(PropertyName = "name")]
		public string  Name { get; set; }

		/// <summary>
		/// The actual token string. 32 to 64 characters.
		/// Required
		/// </summary>
		/// <value>The code.</value>
		[JsonProperty(PropertyName = "code")]
		public string  Code { get; set; }

		/// <summary>
		/// Set by the API
		/// </summary>
		/// <value>The function.</value>
		[JsonProperty(PropertyName = "function", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
		public string Function { get; set; }

		public Token() {
			Complete = true;
		}
	}
}

