using System.Globalization;

namespace PocSocketMaui.Converters;

public class ServerConnectedBoolToColorValueConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		bool.TryParse(value.ToString(), out bool isServerConnected);

		return isServerConnected ? Colors.Green : Colors.Red;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
