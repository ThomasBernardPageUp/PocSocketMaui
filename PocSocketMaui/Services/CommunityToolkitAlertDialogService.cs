using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using PocSocketMaui.Services.Interfaces;
using PocSocketMaui.Views.Popups;

namespace PocSocketMaui.Services;

public class CommunityToolkitAlertDialogService : IAlertDialogService
{
	public async Task<bool> AlertAsync(string message, string validationButtonText, string cancelButtonText, string title)
	{
		var tcs = new TaskCompletionSource<bool>();
		var alertPopupPage = new AlertPopupPage(message, validationButtonText, tcs, title, cancelButtonText);

		await App.Current.MainPage.ShowPopupAsync(alertPopupPage).ConfigureAwait(true);

		var result = await tcs.Task.ConfigureAwait(true);
		return result;
	}

	public async Task<bool> AlertAsync(string message, string validationButtonText, string cancelButtonText)
	{
		var tcs = new TaskCompletionSource<bool>();
		var alertPopupPage = new AlertPopupPage(message, validationButtonText, tcs, cancellationButtonText:cancelButtonText);

		await App.Current.MainPage.ShowPopupAsync(alertPopupPage).ConfigureAwait(true);

		var result = await tcs.Task.ConfigureAwait(true);
		return result;
	}

	public async Task<bool> AlertAsync(string message, string validationButtonText)
	{
		var tcs = new TaskCompletionSource<bool>();
		var alertPopupPage = new AlertPopupPage(message, validationButtonText, tcs);

		await App.Current.MainPage.ShowPopupAsync(alertPopupPage).ConfigureAwait(true);

		var result = await tcs.Task.ConfigureAwait(true);
		return result;
	}

	public void DisplayToast(string message)
	{
		var cancellationTokenSource = new CancellationTokenSource();

		var toast = Toast.Make(message);
		toast.Show(cancellationTokenSource.Token).GetAwaiter().GetResult();
	}

	public async Task<bool?> NullableAlertAsync(string message, string validationButtonText, string cancelButtonText, string backButtonText)
	{
		var tcs = new TaskCompletionSource<bool?>();
		var alertPopupPage = new AlertNullablePopupPage(tcs, message, validationButtonText, cancelButtonText, backButtonText);

		await App.Current.MainPage.ShowPopupAsync(alertPopupPage).ConfigureAwait(true);

		var result = await tcs.Task.ConfigureAwait(true);
		return result;
	}

	public async Task<(bool DialogValidation, string InputValue)> PromptAsync(string message, string validationButtonText, string promptHint, string cancelButtonText = null, string entryText = null)
	{
		var tcs = new TaskCompletionSource<(bool DialogValidation, string InputValue)>();
		var promptPopupPage = new PromptPopupPage(message, validationButtonText, promptHint, tcs, cancelButtonText, entryText);

		await App.Current.MainPage.ShowPopupAsync(promptPopupPage).ConfigureAwait(true);

		var result = await tcs.Task.ConfigureAwait(true);
		return result;
	}
}

