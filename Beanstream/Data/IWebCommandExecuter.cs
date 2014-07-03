using Beanstream.Web;

namespace Beanstream.Data
{
	public interface IWebCommandExecuter
	{
		/// <summary>
		/// Executes the command defined by the specified <paramref name="spec"/>.
		/// </summary>
		/// <param name="spec">The specification of the command to execute.</param>
		/// <returns>The result of the execution.</returns>
		WebCommandResult<T> ExecuteCommand<T>(IWebCommandSpec<T> spec);
	}
}