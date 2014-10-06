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
using Beanstream.Api.SDK.Domain;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Beanstream.Api.SDK
{
	public class PaymentProfile
	{
		[JsonProperty(PropertyName = "customer_code")]
		public string Id { get; set; }

		[JsonProperty(PropertyName = "modified_date")]
		public DateTime ModifiedDate { get; set; }

		/// <summary>
		/// The default card on the profile. To get all the cards call paymentProfile.getCards().
		/// Instead you can create a profile with a Token.
		/// </summary>
		/// <value>The card.</value>
		[JsonProperty(PropertyName = "card")]
		public Card Card { get; set; }

		/// <summary>
		/// You can create a Profile with a token OR a credit card (using the Card field).
		/// </summary>
		/// <value>The token.</value>
		[JsonProperty(PropertyName = "token")]
		public Token Token { get; set; }

		[JsonProperty(PropertyName = "billing")]
		public Address Billing { get; set; }

		[JsonProperty(PropertyName = "custom")]
		public CustomFields CustomFields { get; set; }

		[JsonProperty(PropertyName = "language")]
		public string Language { get; set; }

		[JsonProperty(PropertyName = "status")]
		public string Status { get; set; }

		[JsonProperty(PropertyName = "last_transaction")]
		public string LastTransaction { get; set; }

		[JsonProperty(PropertyName = "comment")]
		public string Comment { get; set; }

		/// <summary>
		/// Get all cards on this profile
		/// </summary>
		/// <returns>The cards.</returns>
		/// <param name="gateway">Gateway.</param>
		public IList<Card> getCards(ProfilesAPI api) {
			return api.GetCards (Id);
		}

		/// <summary>
		/// Get a card from a profile. Card intex starts at 1.
		/// For example if you have 3 cards on the profile to get the 2nd card you would pass
		/// in card id = 2
		/// </summary>
		/// <returns>The card for the specified card id</returns>
		/// <param name="api">API.</param>
		/// <param name="cardId">Card identifier.</param>
		public Card getCard(ProfilesAPI api, int cardId) {
			return api.GetCard (Id, cardId);
		}

		/// <summary>
		/// Add a card to this payment profile
		/// </summary>
		/// <returns>The card.</returns>
		/// <param name="card">Card.</param>
		/// <param name="gateway">Gateway.</param>
		public ProfileResponse AddCard(ProfilesAPI api, Card card) {
			return api.AddCard (Id, card);
		}

		public ProfileResponse UpdateCard(ProfilesAPI api, Card card) {
			return api.UpdateCard (Id, card);
		}

		public ProfileResponse RemoveCard(ProfilesAPI api, int cardId) {
			return api.RemoveCard (Id, cardId);
		}
	}
}

