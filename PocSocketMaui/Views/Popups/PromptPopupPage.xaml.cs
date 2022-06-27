namespace PocSocketMaui.Views.Popups;

public partial class PromptPopupPage
{
	private TaskCompletionSource<(bool DialogValidation, string InputValue)> _taskCompletionSource;

	public PromptPopupPage(string message, string validationButtonText, string promptHint, TaskCompletionSource<(bool DialogValidation, string InputValue)> tcs, string cancelButtonText, string entryText)
	{
		InitializeComponent();
		_taskCompletionSource = tcs;
		MessageLabel.Text = message;

		ValidateButton.Text = validationButtonText;

		if (!string.IsNullOrWhiteSpace(cancelButtonText))
		{
			CancelButton.Text = cancelButtonText;
		}
		else
		{
			Grid.SetColumn(ValidateButton, 0);
			Grid.SetColumnSpan(ValidateButton, 2);

			CancelButton.IsVisible = false;
		}

		if (!string.IsNullOrWhiteSpace(promptHint))
			PromptEntry.Placeholder = promptHint;

		if (!string.IsNullOrWhiteSpace(entryText))
			PromptEntry.Text = entryText;

	}

	private void CancelButton_OnClicked(object sender, EventArgs e)
	{
		Close();
		_taskCompletionSource.TrySetResult((false, string.Empty));
	}

	private void ValidateButton_OnClicked(object sender, EventArgs e)
	{
		Close();
		_taskCompletionSource.TrySetResult((true, PromptEntry.Text));
	}
}