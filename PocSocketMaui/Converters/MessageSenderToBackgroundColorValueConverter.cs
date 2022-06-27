using System.Globalization;
using PocSocketMaui.Wrappers;

namespace PocSocketMaui.Converters;

public class MessageSenderToBackgroundColorValueConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		var sender = (MessageSender)value;

		return sender == MessageSender.Server ? Color.FromHex("E5E5EA") : Color.FromHex("41CC3C");
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
