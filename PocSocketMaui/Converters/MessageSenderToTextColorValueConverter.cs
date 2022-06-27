using System.Globalization;
using PocSocketMaui.Wrappers;

namespace PocSocketMaui.Converters;

public class MessageSenderToTextColorValueConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		var sender = (MessageSender)value;

		return sender == MessageSender.Server ? Colors.Black : Colors.White;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
