using System;
using System.Net.WebSockets;
using System.Text;
using System.Linq;
using PocSocketMaui.Commons;
using PocSocketMaui.Services.Interfaces;
using DynamicData;
using PocSocketMaui.Wrappers;
using System.Collections.ObjectModel;
using ReactiveUI;
using System.Reactive.Linq;

namespace PocSocketMaui.Services;

public class SocketService : ISocketService
{
	private readonly ClientWebSocket _clientWebSocket = new ClientWebSocket();
	private readonly Uri _serverUri = new Uri(Constants.SocketServerUrl);
	private readonly CancellationToken _cancellationToken = new CancellationToken();

	#region Dynamic List Messages
	private SourceCache<MessageWrapper, long> _messagesCache = new SourceCache<MessageWrapper, long>(m => m.Id);
	public readonly ReadOnlyObservableCollection<MessageWrapper> _messages;
	public ReadOnlyObservableCollection<MessageWrapper> Messages => _messages;
	#endregion

	public SocketService()
	{
		_messagesCache.Connect()
			.Bind(out _messages)
			.ObserveOn(RxApp.MainThreadScheduler)
			.Subscribe();
	}

	public async Task ConnectToServerAsync()
	{
		await _clientWebSocket.ConnectAsync(_serverUri, _cancellationToken);

		await Task.Factory.StartNew(async () =>
		{
			while (true)
			{
				await ReadMessageAsync();
			}
		}, _cancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);
	}

	public async Task ReadMessageAsync()
	{
		WebSocketReceiveResult receiveResult;
		var message = new ArraySegment<byte>(new byte[4096]);

		do
		{
			receiveResult = await _clientWebSocket.ReceiveAsync(message, _cancellationToken);
			if (receiveResult.MessageType != WebSocketMessageType.Text)
				break;
			var messageBytes = message.Skip(message.Offset).Take(receiveResult.Count).ToArray();
			string receivedMessage = Encoding.UTF8.GetString(messageBytes);
			_messagesCache.AddOrUpdate(new MessageWrapper(_messagesCache.Count, MessageSender.Server, DateTime.Now, receivedMessage));
		}
		while (!receiveResult.EndOfMessage);
	}

	public async Task<bool> SendMessageAsync(string message)
	{
		var byteMessage = Encoding.UTF8.GetBytes(message);
		var segmnet = new ArraySegment<byte>(byteMessage);

		await _clientWebSocket.SendAsync(segmnet, WebSocketMessageType.Text, true, _cancellationToken);

		_messagesCache.AddOrUpdate(new MessageWrapper(_messagesCache.Count, MessageSender.Client, DateTime.Now, message));


		return true;
	}

	public async Task DisconnectToServerAsync()
	{
		_clientWebSocket.Dispose();
	}
}
