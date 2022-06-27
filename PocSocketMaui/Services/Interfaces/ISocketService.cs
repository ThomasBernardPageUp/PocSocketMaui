using System.Collections.ObjectModel;
using System.Net.WebSockets;
using PocSocketMaui.Wrappers;

namespace PocSocketMaui.Services.Interfaces;

public interface ISocketService
{
	/// <summary>
	/// History of all messages (sended and received)
	/// </summary>
	public ReadOnlyObservableCollection<MessageWrapper> Messages { get; }

	/// <summary>
	/// Connect to server
	/// </summary>
	/// <returns></returns>
	Task ConnectToServerAsync();

	/// <summary>
	/// Disconnect to the server
	/// </summary>
	/// <returns></returns>
	Task DisconnectToServerAsync();

	/// <summary>
	/// Read message
	/// </summary>
	/// <returns></returns>
	Task ReadMessageAsync();

	/// <summary>
	/// Send a message to the server and add it in history list
	/// </summary>
	/// <param name="message"></param>
	/// <returns></returns>
	Task<bool> SendMessageAsync(string message);
}
