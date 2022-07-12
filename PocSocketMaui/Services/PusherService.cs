using System.Collections.ObjectModel;
using System.Reactive.Linq;
using DryIoc.ImTools;
using DynamicData;
using PocSocketMaui.Commons;
using PocSocketMaui.Services.Interfaces;
using PocSocketMaui.Wrappers;
using PusherClient;
using ReactiveUI;

namespace PocSocketMaui.Services
{
	public class PusherService : ISocketService
	{
		private readonly Pusher _pusher;

		#region Dynamic List Messages
		private SourceCache<MessageWrapper, long> _messagesCache = new SourceCache<MessageWrapper, long>(m => m.Id);
		public readonly ReadOnlyObservableCollection<MessageWrapper> _messages;
		public ReadOnlyObservableCollection<MessageWrapper> Messages => _messages;
		#endregion


		public PusherService()
		{
			try
			{
				_messagesCache.Connect()
					.Bind(out _messages)
					.ObserveOn(RxApp.MainThreadScheduler)
					.Subscribe();

				_pusher = new Pusher("key", new PusherOptions {
					Host = Constants.PusherServerUrl,
					ClientTimeout = TimeSpan.FromSeconds(60),
					Encrypted = true,
				});

				_pusher.Error += ErrorHandler;
				_pusher.ConnectionStateChanged += StateChanged;
			} 
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}

		}

		private void StateChanged(object sender, ConnectionState state)
		{
			Console.WriteLine("Connection state: " + state.ToString());
		}
		private void ErrorHandler(object sender, PusherException error)
		{
			Console.WriteLine("Pusher error: " + error.ToString());
		}

		public async Task ConnectToServerAsync()
		{
			await _pusher.ConnectAsync().ConfigureAwait(false);
		}

		public async Task DisconnectToServerAsync()
		{
			await _pusher.DisconnectAsync();
		}

		public async Task ReadMessageAsync()
		{
			var chanel = await _pusher.SubscribeAsync("consignes");
			chanel.Bind("consigne", ChannelListener);
		}

		void ChannelListener(PusherEvent eventData)
		{
			_messagesCache.AddOrUpdate(new MessageWrapper(_messagesCache.Count, MessageSender.Server, DateTime.Now, eventData?.Data));
		}

		public Task<bool> SendMessageAsync(string message)
		{
			throw new NotImplementedException();
		}

	}


}
