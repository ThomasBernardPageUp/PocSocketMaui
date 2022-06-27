using System.Globalization;

namespace PocSocketMaui.Converters;

public class ServerConnectedBoolToServerConnectedStringValueConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		bool.TryParse(value.ToString(), out bool isConnected);

		return isConnected ? "Connected" : "Disconnected";
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
