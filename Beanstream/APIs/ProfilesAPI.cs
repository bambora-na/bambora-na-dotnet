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

		public ProfileResponse CreateProfile(Card card, Address billingAddress, CustomFields customFields) {

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
				billing = billingAddress,
				custom = customFields
			};

			string response = req.ProcessTransaction (HttpMethod.Post, url, profile);
			Console.WriteLine (response);
			return JsonConvert.DeserializeObject<ProfileResponse>(response);
		}

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
			return JsonConvert.DeserializeObject<PaymentProfile>(response);
		}

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

		public ProfileResponse UpdateProfile(PaymentProfile profile) {

			string url = BeanstreamUrls.BaseProfilesUrl
				.Replace("{v}", String.IsNullOrEmpty(_configuration.Version) ? "v1" : "v"+_configuration.Version)
				.Replace("{p}", String.IsNullOrEmpty(_configuration.Platform) ? "www" : _configuration.Platform)
				+"/"+profile.CustomerCode;


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


		public List<Card> GetCards(string profileId) {
			// always get a fresh copy of the profile when retrieving data
			PaymentProfile paymentProfile = GetProfile (profileId);

			paymentProfile.getCards (this);
			return null; // TODO
		}

		public ProfileResponse AddCard(string profileId, Card card) {
			return null; // TODO
		}

		public ProfileResponse UpdateCard(string profileId, Card card) {
			return null; // TODO
		}

		public ProfileResponse RemoveCard(string profileId, Card card) {
			return null; // TODO
		}

	}
}

