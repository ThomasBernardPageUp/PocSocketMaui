using System.Reactive;
using PocSocketMaui.Services.Interfaces;
using PocSocketMaui.ViewModels.Base;
using ReactiveUI;

namespace PocSocketMaui.ViewModels;

public class MainViewModel : BaseViewModel
{
	private readonly IAlertDialogService _alertDialogService;

	public MainViewModel(INavigationService navigationService, IAlertDialogService alertDialogService) : base(navigationService)
	{
		_alertDialogService = alertDialogService;
		ButtonCommand = ReactiveCommand.Create<string, Task>(async number => await OnButtonCommand(number));
	}


	public ReactiveCommand<string, Task> ButtonCommand { get; set; }
	private async Task OnButtonCommand(string number)
	{
		switch (number)
		{
			case "0":
				await _alertDialogService.AlertAsync("Message of the popup", "Ok", "Cancel", "Title");
				break;
			case "1":
				await _alertDialogService.AlertAsync("Message of the popup", "Ok", "No");
				break;
			case "2":
				await _alertDialogService.AlertAsync("Message of the popup", "Ok");
				break;
			case "3":
				_alertDialogService.DisplayToast("This is a toast");
				break;
			case "4":
				await _alertDialogService.NullableAlertAsync("Message of the popup", "Ok", "Cancel", "Goback");
				break;
			case "5":
				await _alertDialogService.PromptAsync("Message of the popup", "Ok", "Cancel", "Goback", "e");
				break;
			default:
				break;
		}
	}
}
