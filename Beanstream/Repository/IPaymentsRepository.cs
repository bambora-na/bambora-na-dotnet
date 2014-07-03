namespace Beanstream.Repository
{
	public interface IPaymentsRepository
	{
		string ApiVersion { get; set; }
		string Platform { get; set; }
		string Username { get; set; }
		string Password { get; set; }
		int? MerchantId { get; set; }
		string Passcode { get; set; }
		string Create(object payment);
		string Return(int? transId, object payment);
		string Void(int? transId, object payment);
		string Complete(int? transId, object payment);
		string Continue(string merchantdata, object continuation);
	}
}