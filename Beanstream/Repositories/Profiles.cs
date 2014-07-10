using System;
using Beanstream.Repositories;
using Beanstream.Data;

namespace Beanstream.Repositories
{
	public class Profiles
	{
	
		IRepository _repository;

		public IRepository Repository
		{
			private get
			{
				if (_repository == null)
				{
					_repository = new Repository(
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

			/**
			 * Update a profile
			 */
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

			/*public string UpdateCard(object card)
				{

					string url = Repository.BuildUrl () +
						BeanstreamUrls.CardsUri.Replace ("{id}", _profileId)+;

					return Repository.ProcessTransaction(HttpMethod.Get, url);
				}*/

		}
	}
}

