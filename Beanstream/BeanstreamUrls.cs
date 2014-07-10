namespace Beanstream
{
	internal class BeanstreamUrls
	{
		public static string BaseUrl = "https://{p}.beanstream.com/api";
		public static string BasePaymentsUrl = BaseUrl + "/{v}/payments";
		public static string BaseProfilesUrl = BaseUrl + "/{v}/profiles";
		public static string PreAuthCompletionsUri = "/{id}/completions";
		public static string ReturnsUri = "/{id}/returns";
		public static string VoidsUri =  "/{id}/void";
		public static string ContinuationsUri = "/{md}/continue";
		public static string ProfileUri = "/{id}";
		public static string CardsUri = ProfileUri+"/cards";
	}
}
