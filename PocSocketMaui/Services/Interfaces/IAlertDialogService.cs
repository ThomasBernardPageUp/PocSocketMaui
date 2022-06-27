namespace PocSocketMaui.Services.Interfaces;

public interface IAlertDialogService
{
	Task<(bool DialogValidation, string InputValue)> PromptAsync(string message, string validationButtonText,
		   string promptHint, string cancelButtonText = null, string entryText = null);

	Task<bool> AlertAsync(string message, string validationButtonText, string cancelButtonText, string title);

	Task<bool> AlertAsync(string message, string validationButtonText, string cancelButtonText);

	Task<bool> AlertAsync(string message, string validationButtonText);


	Task<bool?> NullableAlertAsync(string message, string validationButtonText, string cancelButtonText, string backButtonText);

	void DisplayToast(string message);
}
