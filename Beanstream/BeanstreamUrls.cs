namespace Beanstream
{
	internal class BeanstreamUrls
	{
		public static string BaseUrl = "https://{p}.beanstream.com/api";
		public static string BasePaymentsUrl = BaseUrl + "/{v}/payments";
		public static string PreAuthCompletionsUri = "/{id}/completions";
		public static string ReturnsUri = "/{id}/returns";
		public static string VoidsUri =  "/{id}/void";
		public static string ContinuationsUri = "/{md}/continue";
	}
}
