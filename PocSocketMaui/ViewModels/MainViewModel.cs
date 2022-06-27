using System.Collections.ObjectModel;
using System.Net.WebSockets;
using System.Reactive;
using PocSocketMaui.Commons;
using PocSocketMaui.Services.Interfaces;
using PocSocketMaui.ViewModels.Base;
using PocSocketMaui.Wrappers;
using ReactiveUI;

namespace PocSocketMaui.ViewModels;

public class MainViewModel : BaseViewModel
{
	private readonly ISocketService _socketService;
	private readonly IAlertDialogService _alertDialogService;

	public MainViewModel(INavigationService navigationService, ISocketService socketService, IAlertDialogService alertDialogService) : base(navigationService)
	{
		_socketService = socketService;
		_alertDialogService = alertDialogService;

		ConnectToServerCommand = ReactiveCommand.Create<Unit, Task>(async _ => await OnConnectToServerCommand());
		DisconnectToServerCommand = ReactiveCommand.Create<Unit, Task>(async _ => await OnDisconnectToServerCommand(), this.WhenAnyValue(vm => vm.IsServerConnected));
		SendMessageCommand = ReactiveCommand.Create<Unit, Task>(async _ => await OnSendMessageCommand());
		InformationCommand = ReactiveCommand.Create<Unit, Task>(async _ => await OnInformationCommand());

		_socketService.ConnectToServerAsync();
		IsServerConnected = true;
	}

	#region Commands

	#region ConnectToServerCommand => OnConnectToServerCommand
	public ReactiveCommand<Unit, Task> ConnectToServerCommand { get; set; }
	private async Task OnConnectToServerCommand()
	{
		await _socketService.ConnectToServerAsync();
		IsServerConnected = true;
	}
	#endregion

	#region DisconnectToServerCommand => DisconnectToServerAsync
	public ReactiveCommand<Unit, Task> DisconnectToServerCommand { get; set; }
	private async Task OnDisconnectToServerCommand()
	{
		IsServerConnected = false;
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

	#region InformationCommand => OnInformationCommand
	public ReactiveCommand<Unit, Task> InformationCommand { get; set; }
	private async Task OnInformationCommand()
	{
		await _alertDialogService.AlertAsync($"Server : {Constants.SocketServerUrl}", "Ok");
	}
	#endregion

	#endregion

	#region Properties

	public ReadOnlyObservableCollection<MessageWrapper> Messages => _socketService.Messages;

	#region IsServerConnected
	private bool _isServerConnected;
	public bool IsServerConnected
	{
		get => _isServerConnected;
		set => this.RaiseAndSetIfChanged(ref _isServerConnected, value);
	}
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
