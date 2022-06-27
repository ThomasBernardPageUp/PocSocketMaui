using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using PageUpX.Core.Log;

namespace PocSocketMaui.Commons.Extensions
{
	public static class ObservableExtension
	{
		public static IDisposable SubscribeSafe<TT>(
			this IObservable<TT> @this,
			IPuxLogger logger,
			[CallerMemberName] string callerMemberName = null,
			[CallerFilePath] string callerFilePath = null,
			[CallerLineNumber] int callerLineNumber = 0)
		{
			return @this
				.Subscribe(_ => { },
						   ex => {
							   logger.Error($"An exception went unhandled on the observable {@this}", ex, callerMemberName, callerFilePath, callerLineNumber);

							   Debugger.Break();
						   });
		}

		public static IDisposable SubscribeSafe<TT>(
			this IObservable<TT> @this, Action<TT> action,
			IPuxLogger logger,
			[CallerMemberName] string callerMemberName = null,
			[CallerFilePath] string callerFilePath = null,
			[CallerLineNumber] int callerLineNumber = 0)
		{
			return @this
				.Subscribe(action,
						   ex => {
							   logger.Error($"An exception went unhandled on the observable {@this}", ex, callerMemberName, callerFilePath, callerLineNumber);

							   Debugger.Break();
						   });
		}
	}
}

