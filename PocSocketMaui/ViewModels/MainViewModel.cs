using System.Collections.ObjectModel;
using System.Net.WebSockets;
using System.Reactive;
using PocSocketMaui.Services.Interfaces;
using PocSocketMaui.ViewModels.Base;
using PocSocketMaui.Wrappers;
using ReactiveUI;

namespace PocSocketMaui.ViewModels;

public class MainViewModel : BaseViewModel
{
	private readonly ISocketService _socketService;

	public MainViewModel(INavigationService navigationService, ISocketService socketService) : base(navigationService)
	{
		_socketService = socketService;
		ConnectToServerCommand = ReactiveCommand.Create<Unit, Task>(async _ => await OnConnectToServerCommand());
		DisconnectToServerCommand = ReactiveCommand.Create<Unit, Task>(async _ => await OnDisconnectToServerCommand());
		SendMessageCommand = ReactiveCommand.Create<Unit, Task>(async _ => await OnSendMessageCommand());
	}

	#region Commands

	#region ConnectToServerCommand => OnConnectToServerCommand
	public ReactiveCommand<Unit, Task> ConnectToServerCommand { get; set; }
	private async Task OnConnectToServerCommand()
	{
		await _socketService.ConnectToServerAsync();
	}
	#endregion

	#region DisconnectToServerCommand => DisconnectToServerAsync
	public ReactiveCommand<Unit, Task> DisconnectToServerCommand { get; set; }
	private async Task OnDisconnectToServerCommand()
	{
		await _socketService.DisconnectToServerAsync();
	}
	#endregion

	#region SendMessageCommand => OnSendMessageCommand
	public ReactiveCommand<Unit, Task> SendMessageCommand { get; set; }
	private async Task OnSendMessageCommand()
	{
		await _socketService.SendMessageAsync(EntryText);
		EntryText = string.Empty;
	}
	#endregion



	#endregion

	#region Properties

	#region Messages
	public ReadOnlyObservableCollection<MessageWrapper> Messages => _socketService.Messages;
	#endregion

	#region EntryText
	private string _entryText;
	public string EntryText
	{
		get => _entryText;
		set => this.RaiseAndSetIfChanged(ref _entryText, value);
	}
	#endregion


	#endregion

}
