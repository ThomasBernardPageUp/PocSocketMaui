using System.Reactive.Disposables;
using ReactiveUI;
using Sharpnado.Tasks;

namespace PocSocketMaui.ViewModels.Base;

public class BaseViewModel : ReactiveObject, INavigatedAware, IInitializeAsync, IActivatableViewModel
{
	protected INavigationService NavigationService;

	public BaseViewModel(INavigationService navigationService)
	{
		NavigationService = navigationService;

		Activator = new ViewModelActivator();
	}

	#region Life cycle
	public virtual Task InitializeAsync(INavigationParameters parameters)
	{
		return Task.CompletedTask;
	}

	public void OnNavigatedFrom(INavigationParameters parameters)
	{
		Activator.Deactivate();
		_deactivateWith?.Dispose();
		_deactivateWith = null;

		TaskMonitor.Create(OnNavigatedFromAsync(parameters),
							   whenFaulted: t =>
							   {
								   t.Exception.Handle(ex =>
								   {
									   HandleException(ex, $"Error on {GetType().Name}.OnNavigatedFromAsync");
									   return true;
								   });
							   });

	}

	public void OnNavigatedTo(INavigationParameters parameters)
	{
		TaskMonitor.Create(OnNavigatedToAsync(parameters),
							   whenFaulted: t =>
							   {
								   t.Exception.Handle(ex =>
								   {
									   HandleException(ex, $"Error on {GetType().Name}.OnNavigatedToAsync");
									   return true;
								   });
							   });


		DeactivateWith.Add(Activator.Activate());
	}

	protected virtual Task OnNavigatedFromAsync(INavigationParameters parameters)
	{
		return Task.FromResult(0);
	}

	protected virtual Task OnNavigatedToAsync(INavigationParameters parameters)
	{
		return Task.FromResult(0);
	}

	public virtual void Destroy()
	{
		DestroyWith?.Dispose();
	}
	#endregion

	#region Implementation of IActivableViewModel
	public ViewModelActivator Activator { get; }
	#endregion

	#region CompositeDrawable

	protected CompositeDisposable DestroyWith { get; } = new CompositeDisposable();

	private CompositeDisposable _deactivateWith;

	protected CompositeDisposable DeactivateWith
	{
		get {
			if (_deactivateWith == null)
				_deactivateWith = new CompositeDisposable();

			return _deactivateWith;
		}

	}
	#endregion

	#region Methods

	protected async void HandleException(Exception ex, string infoToLog = null, string message = null)
		{
			//Logger.Error(infoToLog, ex);

			switch (ex)
			{
				default:
					//await AlertDialogService.AlertAsync(message ?? "Un problème est survenu", "Ok")
					//                        .ConfigureAwait(true);

					break;
			}
		}

	#endregion

}