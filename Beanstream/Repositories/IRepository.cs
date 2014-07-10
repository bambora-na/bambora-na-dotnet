namespace Beanstream.Repositories
{
	public interface IRepository
	{
		string ApiVersion { get; set; }
		string Platform { get; set; }
		string Username { get; set; }
		string Password { get; set; }
		int? MerchantId { get; set; }
		string Passcode { get; set; }
		string Url 		{ get; set; }
		string BuildUrl ();
		string ProcessTransaction (HttpMethod method, string url);
		string ProcessTransaction (HttpMethod method, string url, object data);

	}
}