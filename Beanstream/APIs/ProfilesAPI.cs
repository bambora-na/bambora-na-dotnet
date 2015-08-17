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
using Beanstream.Api.SDK;
using Beanstream.Api.SDK.Data;
using System.Collections.Generic;
using Beanstream.Api.SDK.Domain;
using Beanstream.Api.SDK.Requests;
using Newtonsoft.Json;

/// <summary>
/// Payment Profiles allow you to store a customer's card number and other information, such as billing address and shipping address.
/// The card number stored on the profile is a multi-use token and is called the ID.
/// 
/// Profiles can be created with a Credit Card or with a single-use Legato token. If using a token then the card information
/// needs to be entered each time the user checks out. However the profile will always save the customer's billing info.
/// </summary>
namespace Beanstream.Api.SDK
{
	public class ProfilesAPI
	{
		private Configuration _configuration;
		private IWebCommandExecuter _webCommandExecuter = new WebCommandExecuter ();

		public Configuration Configuration {
			set { _configuration = value; }
		}

		public IWebCommandExecuter WebCommandExecuter {
			set { _webCommandExecuter = value; }
		}

		/// <summary>
		/// Create a Payment Profile with a Credit Card and billing address
		/// </summary>
		/// <returns>The profile response containing the profile ID</returns>
		/// <param name="card">Card.</param>
		/// <param name="billingAddress">Billing address.</param>
		public ProfileResponse CreateProfile(Card card, Address billingAddress) {
			return CreateProfile (card, billingAddress, null, null, null);
		}

		/// <summary>
		/// Create a Payment Profile with a Credit Card and billing address
		/// </summary>
		/// <returns>The profile response containing the profile ID</returns>
		/// <param name="card">Card.</param>
		/// <param name="billingAddress">Billing address.</param>
		/// <param name="customFields">Custom fields. Optional</param>
		/// <param name="language">Language. Optional</param>
		/// <param name="comment">Comment. Optional</param>
		public ProfileResponse CreateProfile(Card card, Address billingAddress, CustomFields customFields, string language, string comment) {
			return CreateProfile (null, card, billingAddress, customFields, language, comment);
		}

		/// <summary>
		/// Create a Payment Profile with a single-use Legato token.
		/// </summary>
		/// <returns>TThe profile response containing the profile ID</returns>
		/// <param name="token">Token.</param>
		/// <param name="billingAddress">Billing address.</param>
		public ProfileResponse CreateProfile(Token token, Address billingAddress) {
			return CreateProfile (token, billingAddress, null, null, null);
		}

		/// <summary>
		/// Create a Payment Profile with a single-use Legato token.
		/// </summary>
		/// <returns>TThe profile response containing the profile ID</returns>
		/// <param name="token">Token.</param>
		/// <param name="billingAddress">Billing address.</param>
		/// <param name="customFields">Custom fields. Optional</param>
		/// <param name="language">Language. Optional</param>
		/// <param name="comment">Comment. Optional</param>
		public ProfileResponse CreateProfile(Token token, Address billingAddress, CustomFields customFields, string language, string comment) {
			return CreateProfile (token, null, billingAddress, customFields, language, comment);
		}

		private ProfileResponse CreateProfile(Token token, Card card, Address billingAddress, CustomFields customFields, string language, string comment) {

			if (token == null && card == null) {
				Gateway.ThrowIfNullArgument (null, "Card and Token both null!");
			}

			if (token == null) {
				Gateway.ThrowIfNullArgument (card, "card");
				Gateway.ThrowIfNullArgument (card.Number, "card.number");
				Gateway.ThrowIfNullArgument (card.Name, "card.name");
				Gateway.ThrowIfNullArgument (card.ExpiryMonth, "card.expiryMonth");
				Gateway.ThrowIfNullArgument (card.ExpiryYear, "card.expiryYear");
			}
			if (card == null) {
				Gateway.ThrowIfNullArgument (token, "token");
				Gateway.ThrowIfNullArgument (token.Name, "token.name");
				Gateway.ThrowIfNullArgument (token.Code, "token.code");
			}
				

			string url = BeanstreamUrls.BaseProfilesUrl
				.Replace ("{v}", String.IsNullOrEmpty (_configuration.Version) ? "v1" : "v" + _configuration.Version)
				.Replace ("{p}", String.IsNullOrEmpty (_configuration.Platform) ? "www" : _configuration.Platform);;


			HttpsWebRequest req = new HttpsWebRequest () {
				MerchantId = _configuration.MerchantId,
				Passcode = _configuration.ProfilesApiPasscode,
				WebCommandExecutor = _webCommandExecuter
			};

			var profile = new {
				card = card,
				token = token,
				billing = billingAddress,
				custom = customFields
			};

			string response = req.ProcessTransaction (HttpMethod.Post, url, profile);

			return JsonConvert.DeserializeObject<ProfileResponse>(response);
		}

		/// <summary>
		/// Retrieve a profile using the profile's ID. If you want to modify a profile
		/// you must first retrieve it using this method.
		/// </summary>
		/// <returns>The profile.</returns>
		/// <param name="profileId">Profile identifier.</param>
		public PaymentProfile GetProfile(string profileId) {
		
			string url = BeanstreamUrls.BaseProfilesUrl
				.Replace("{v}", String.IsNullOrEmpty(_configuration.Version) ? "v1" : "v"+_configuration.Version)
				.Replace("{p}", String.IsNullOrEmpty(_configuration.Platform) ? "www" : _configuration.Platform)
				+"/"+profileId;


			HttpsWebRequest req = new HttpsWebRequest () {
				MerchantId = _configuration.MerchantId,
				Passcode = _configuration.ProfilesApiPasscode,
				WebCommandExecutor = _webCommandExecuter
			};

			string response = req.ProcessTransaction (HttpMethod.Get, url);
			PaymentProfile profile = JsonConvert.DeserializeObject<PaymentProfile>(response);
			profile.Id = profileId;
			return profile;
		}

		/// <summary>
		/// Deletes the profile.
		/// </summary>
		/// <returns>The profile response if successful, an error if not.</returns>
		/// <param name="profileId">Profile identifier.</param>
		public ProfileResponse DeleteProfile(string profileId) {

			string url = BeanstreamUrls.BaseProfilesUrl
				.Replace ("{v}", String.IsNullOrEmpty (_configuration.Version) ? "v1" : "v" + _configuration.Version)
				.Replace ("{p}", String.IsNullOrEmpty (_configuration.Platform) ? "www" : _configuration.Platform)
				+"/"+profileId;


			HttpsWebRequest req = new HttpsWebRequest () {
				MerchantId = _configuration.MerchantId,
				Passcode = _configuration.ProfilesApiPasscode,
				WebCommandExecutor = _webCommandExecuter
			};

			string response = req.ProcessTransaction (HttpMethod.Delete, url);
			return JsonConvert.DeserializeObject<ProfileResponse>(response);
		}

		/// <summary>
		/// Updates the profile. You must first retrieve the profile using ProfilesAPI.GetProfile(id)
		/// </summary>
		/// <returns>The profile response.</returns>
		/// <param name="profile">Profile.</param>
		public ProfileResponse UpdateProfile(PaymentProfile profile) {

			string url = BeanstreamUrls.BaseProfilesUrl
				.Replace("{v}", String.IsNullOrEmpty(_configuration.Version) ? "v1" : "v"+_configuration.Version)
				.Replace("{p}", String.IsNullOrEmpty(_configuration.Platform) ? "www" : _configuration.Platform)
				+"/"+profile.Id;


			HttpsWebRequest req = new HttpsWebRequest () {
				MerchantId = _configuration.MerchantId,
				Passcode = _configuration.ProfilesApiPasscode,
				WebCommandExecutor = _webCommandExecuter
			};

			var updateProfile = new {

				billing = profile.Billing,
				custom = profile.CustomFields,
				language = profile.Language,
				comment = profile.Comment
			};

			string response = req.ProcessTransaction (HttpMethod.Put, url, updateProfile);
			return JsonConvert.DeserializeObject<ProfileResponse>(response);
		}

		/// <summary>
		/// Gets the cards contained on this profile.
		/// It is possible for a profile not to contain any cards if it was created using a Legato token (single-use token)
		/// </summary>
		/// <returns>The cards.</returns>
		/// <param name="profileId">Profile identifier.</param>
		public IList<Card> GetCards(string profileId) {

			string url = BeanstreamUrls.CardsUrl
				.Replace ("{v}", String.IsNullOrEmpty (_configuration.Version) ? "v1" : "v" + _configuration.Version)
				.Replace ("{p}", String.IsNullOrEmpty (_configuration.Platform) ? "www" : _configuration.Platform)
				.Replace ("{id}", profileId);

			HttpsWebRequest req = new HttpsWebRequest () {
				MerchantId = _configuration.MerchantId,
				Passcode = _configuration.ProfilesApiPasscode,
				WebCommandExecutor = _webCommandExecuter
			};

			string response = req.ProcessTransaction (HttpMethod.Get, url);
			ProfileCardsResponse cardResponse = JsonConvert.DeserializeObject<ProfileCardsResponse>(response);

			if (cardResponse.Cards != null) {
				int id = 1;
				foreach (Card c in cardResponse.Cards) {
					c.id = id++; // give it the ID since that is not persisted nor retrieved
				}
				return cardResponse.Cards;
			} else 
				return new List<Card>(); // empty list with no cards
		}

		/// <summary>
		/// Get a particular card on a profile.
		/// Card IDs are their index in getCards(), starting a 1 and going up: 1, 2, 3, 4...
		/// </summary>
		/// <returns>The card.</returns>
		/// <param name="profileId">Profile identifier.</param>
		/// <param name="cardId">Card identifier.</param>
		public Card GetCard(string profileId, int cardId) {

			string url = BeanstreamUrls.CardsUrl
				.Replace ("{v}", String.IsNullOrEmpty (_configuration.Version) ? "v1" : "v" + _configuration.Version)
				.Replace ("{p}", String.IsNullOrEmpty (_configuration.Platform) ? "www" : _configuration.Platform)
				.Replace ("{id}", profileId)
				+"/"+cardId;

			HttpsWebRequest req = new HttpsWebRequest () {
				MerchantId = _configuration.MerchantId,
				Passcode = _configuration.ProfilesApiPasscode,
				WebCommandExecutor = _webCommandExecuter
			};

			string response = req.ProcessTransaction (HttpMethod.Get, url);

			ProfileCardsResponse cardResponse = JsonConvert.DeserializeObject<ProfileCardsResponse>(response);
			if (cardResponse.Cards == null)
				return null;
			cardResponse.Cards [0].id = cardId; // give it the ID since that is not persisted nor retrieved
			return cardResponse.Cards[0];
		}

		/// <summary>
		/// Updates the card on the profile.
		/// </summary>
		/// <returns>The result of the update</returns>
		/// <param name="profileId">Profile identifier.</param>
		/// <param name="card">Card.</param>
		public ProfileResponse UpdateCard(string profileId, Card card) {

			string url = BeanstreamUrls.CardsUrl
				.Replace ("{v}", String.IsNullOrEmpty (_configuration.Version) ? "v1" : "v" + _configuration.Version)
				.Replace ("{p}", String.IsNullOrEmpty (_configuration.Platform) ? "www" : _configuration.Platform)
				.Replace ("{id}", profileId)
			             + "/" + card.id;

			HttpsWebRequest req = new HttpsWebRequest () {
				MerchantId = _configuration.MerchantId,
				Passcode = _configuration.ProfilesApiPasscode,
				WebCommandExecutor = _webCommandExecuter
			};

			if (card.Number.Contains ("X") )
				card.Number = null; // when a card is returned from the server the number will be masked with X's. We don't want to submit that back.

			// the json wants to be wrapped in a 'card' group
			var c = new {
				card
			};

			string response = req.ProcessTransaction (HttpMethod.Put, url, c);
			return JsonConvert.DeserializeObject<ProfileResponse>(response);
		}

		/// <summary>
		/// Add a new card to the profile. It gets appended to the end of the list of cards.
		/// Make sure your Merchant account can support more cards. The default amount is 1.
		/// You can change this limit in the online Members area for Merchants located at:
		/// https://www.beanstream.com/admin/sDefault.asp
		/// and heading to Configuration -> Payment Profile Configuration
		/// </summary>
		/// <returns>The response</returns>
		/// <param name="profileId">Profile identifier.</param>
		/// <param name="card">Card.</param>
		public ProfileResponse AddCard(string profileId, Card card) {

			string url = BeanstreamUrls.CardsUrl
				.Replace ("{v}", String.IsNullOrEmpty (_configuration.Version) ? "v1" : "v" + _configuration.Version)
				.Replace ("{p}", String.IsNullOrEmpty (_configuration.Platform) ? "www" : _configuration.Platform)
				.Replace ("{id}", profileId);

			HttpsWebRequest req = new HttpsWebRequest () {
				MerchantId = _configuration.MerchantId,
				Passcode = _configuration.ProfilesApiPasscode,
				WebCommandExecutor = _webCommandExecuter
			};

			// the json wants to be wrapped in a 'card' group
			var c = new {
				card
			};

			string response = req.ProcessTransaction (HttpMethod.Post, url, c);
			return JsonConvert.DeserializeObject<ProfileResponse>(response);
		}

		/// <summary>
		/// Add a new tokenized card to the profile. It gets appended to the end of the list of cards.
		/// Make sure your Merchant account can support more cards. The default amount is 1.
		/// You can change this limit in the online Members area for Merchants located at:
		/// https://www.beanstream.com/admin/sDefault.asp
		/// and heading to Configuration -> Payment Profile Configuration
		/// </summary>
		/// <returns>The response</returns>
		/// <param name="profileId">Profile identifier.</param>
		/// <param name="card">Card.</param>
		public ProfileResponse AddCard(string profileId, Token token) {

			string url = BeanstreamUrls.CardsUrl
				.Replace ("{v}", String.IsNullOrEmpty (_configuration.Version) ? "v1" : "v" + _configuration.Version)
				.Replace ("{p}", String.IsNullOrEmpty (_configuration.Platform) ? "www" : _configuration.Platform)
				.Replace ("{id}", profileId);

			HttpsWebRequest req = new HttpsWebRequest () {
				MerchantId = _configuration.MerchantId,
				Passcode = _configuration.ProfilesApiPasscode,
				WebCommandExecutor = _webCommandExecuter
			};

			// the json wants to be wrapped in a 'token' group
			var c = new {
				token
			};

			string response = req.ProcessTransaction (HttpMethod.Post, url, c);
			return JsonConvert.DeserializeObject<ProfileResponse>(response);
		}

		/// <summary>
		/// Removes the card from the profile.
		/// Card IDs are their index in getCards(), starting a 1 and going up: 1, 2, 3, 4...
		/// </summary>
		/// <returns>The card.</returns>
		/// <param name="profileId">Profile identifier.</param>
		/// <param name="cardId">Card identifier.</param>
		public ProfileResponse RemoveCard(string profileId, int cardId) {

			string url = BeanstreamUrls.CardsUrl
				.Replace ("{v}", String.IsNullOrEmpty (_configuration.Version) ? "v1" : "v" + _configuration.Version)
				.Replace ("{p}", String.IsNullOrEmpty (_configuration.Platform) ? "www" : _configuration.Platform)
				.Replace ("{id}", profileId)
				+"/"+cardId;

			HttpsWebRequest req = new HttpsWebRequest () {
				MerchantId = _configuration.MerchantId,
				Passcode = _configuration.ProfilesApiPasscode,
				WebCommandExecutor = _webCommandExecuter
			};

			string response = req.ProcessTransaction (HttpMethod.Delete, url);
			return JsonConvert.DeserializeObject<ProfileResponse>(response);
		}


	}
}

