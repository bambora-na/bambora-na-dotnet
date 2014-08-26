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

/// <summary>
/// This API is in progress and will be going through a major refactor
/// </summary>
namespace Beanstream.Api.SDK
{
	public class ProfilesAPI
	{
	
		//IRepository _repository;

		/*public IRepository Repository
		{
			private get
			{
				if (_repository == null)
				{
					_repository = new HttpsWebRequest(
						new WebCommandExecuter()
					);
				}

				_repository.ApiVersion = Beanstream.ApiVersion;
				_repository.MerchantId = Beanstream.MerchantId;
				_repository.Passcode = Beanstream.ApiKey;
				_repository.Username = Beanstream.Username;
				_repository.Password = Beanstream.Password;
				_repository.Platform = Beanstream.Platform;
				_repository.Url = BeanstreamUrls.BaseProfilesUrl;

				return _repository;
			}
			set
			{
				_repository = value;
			}
		}

		/// <summary>
		/// Create a new Tokenized Profile for saving payment information.
		/// </summary>
		/// <param name="data">An object representing a request data</param>
		/// <remarks>For more information on object see http://developer.beanstream.com</remarks>
		/// <returns></returns>
		public string Create(object profile)
		{
			Beanstream.ThrowIfNullArgument(profile, "profile");

			string url = Repository.BuildUrl ();
			return Repository.ProcessTransaction(HttpMethod.Post, url, profile);
		}

		public string Retrieve(string profileId)
		{
			Beanstream.ThrowIfNullArgument(profileId, "profileId");

			string url = Repository.BuildUrl () +
				BeanstreamUrls.ProfileUri.Replace("{id}", profileId);

			return Repository.ProcessTransaction(HttpMethod.Get, url);
		}

		public string Delete(string profileId)
		{
			Beanstream.ThrowIfNullArgument(profileId, "profileId");

			string url = Repository.BuildUrl () +
				BeanstreamUrls.ProfileUri.Replace("{id}", profileId);

			return Repository.ProcessTransaction(HttpMethod.Delete, url);
		}

		public ProfileInfo Profile(string profileId) {
			return new ProfileInfo (profileId);
		}


		public class ProfileInfo
		{
			private string _profileId;

			public ProfileInfo(string profileId) {
				_profileId = profileId;
			}

			 //
			 // Update a profile
			 //
			public string Update(object data)
			{
				Beanstream.ThrowIfNullArgument(data, "data");

				string url = new Profiles().Repository.BuildUrl () +
					BeanstreamUrls.ProfileUri.Replace ("{id}", _profileId);

				return new Profiles().Repository.ProcessTransaction(HttpMethod.Put, url, data);
			}


			public string AddCard(object card)
			{
				Beanstream.ThrowIfNullArgument(card, "card");

				string url = new Profiles().Repository.BuildUrl () +
					BeanstreamUrls.CardsUri.Replace ("{id}", _profileId);

				return new Profiles().Repository.ProcessTransaction(HttpMethod.Post, url, card);
			}

			public string RetrieveCards()
			{
				string url = new Profiles().Repository.BuildUrl () +
					BeanstreamUrls.CardsUri.Replace ("{id}", _profileId);

				return new Profiles().Repository.ProcessTransaction(HttpMethod.Get, url);
			}

			//public string UpdateCard(object card)
			//	{

			//		string url = Repository.BuildUrl () +
			//			BeanstreamUrls.CardsUri.Replace ("{id}", _profileId)+;

			//		return Repository.ProcessTransaction(HttpMethod.Get, url);
			//	}

		}*/
	}
}

