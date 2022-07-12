using System.Collections.ObjectModel;
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

		ChangeConnectionStateCommand = ReactiveCommand.Create<Unit, Task>(async _ => await OnChangeConnectionStateCommand());
		SendMessageCommand = ReactiveCommand.Create<Unit, Task>(async _ => await OnSendMessageCommand());
		InformationCommand = ReactiveCommand.Create<Unit, Task>(async _ => await OnInformationCommand());

		
		IsServerConnected = true;
	}

	protected override async Task OnNavigatedToAsync(INavigationParameters parameters)
	{
		await base.OnNavigatedToAsync(parameters);
		await _socketService.ConnectToServerAsync();
		await _socketService.ReadMessageAsync();
	}

	#region Commands

	#region ChangeConnectionStateCommand => OnChangeConnectionStateCommand
	public ReactiveCommand<Unit, Task> ChangeConnectionStateCommand { get; set; }
	private async Task OnChangeConnectionStateCommand()
	{
		if (!IsServerConnected) // If server not connected
			await _socketService.ConnectToServerAsync();
		else
			await _socketService.DisconnectToServerAsync();
		IsServerConnected = !IsServerConnected;
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
