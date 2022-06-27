using System.Collections.ObjectModel;
using System.Net.WebSockets;
using PocSocketMaui.Wrappers;

namespace PocSocketMaui.Services.Interfaces;

public interface ISocketService
{
	public ReadOnlyObservableCollection<MessageWrapper> Messages { get; }
	Task ConnectToServerAsync();
	Task DisconnectToServerAsync();
	Task ReadMessageAsync();
	Task<bool> SendMessageAsync(string message);
}
