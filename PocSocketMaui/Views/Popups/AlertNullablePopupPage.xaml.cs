namespace PocSocketMaui.Views.Popups;

public partial class AlertNullablePopupPage
{
	private readonly TaskCompletionSource<bool?> _taskCompletionSource;

	public AlertNullablePopupPage(TaskCompletionSource<bool?> taskCompletionSource, string message, string validationButtonText, string cancellationButtonText, string backButtonText)
	{
		InitializeComponent();
		_taskCompletionSource = taskCompletionSource;
		MessageLabel.Text = message;

		ValidateButton.Text = validationButtonText;
		CancelButton.Text = cancellationButtonText;
		BackButton.Text = backButtonText;
	}

	private void CancelButton_OnClicked(object sender, EventArgs e)
	{
		Close();
		_taskCompletionSource.TrySetResult(false);
	}

	private void ValidateButton_OnClicked(object sender, EventArgs e)
	{
		Close();
		_taskCompletionSource.TrySetResult(true);
	}

	private void BackButton_OnClicked(object sender, EventArgs e)
	{
		Close();
		_taskCompletionSource.TrySetResult(null);
	}
}