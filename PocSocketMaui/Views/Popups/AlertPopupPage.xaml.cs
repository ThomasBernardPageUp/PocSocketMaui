namespace PocSocketMaui.Views.Popups;

public partial class AlertPopupPage
{
	private readonly TaskCompletionSource<bool> _taskCompletionSource;

	public AlertPopupPage(string message, string validationButtonText, TaskCompletionSource<bool> taskCompletionSource, string title = "", string cancellationButtonText = null)
	{
		InitializeComponent();
		_taskCompletionSource = taskCompletionSource;
		TitleLabel.Text = title;
		MessageLabel.Text = message;

		ValidateButton.Text = validationButtonText;

		if (!string.IsNullOrWhiteSpace(cancellationButtonText))
		{
			CancelButton.Text = cancellationButtonText;
		}
		else
		{
			Grid.SetColumn(ValidateButton, 0);
			Grid.SetColumnSpan(ValidateButton, 2);

			CancelButton.IsVisible = false;

		}
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
}