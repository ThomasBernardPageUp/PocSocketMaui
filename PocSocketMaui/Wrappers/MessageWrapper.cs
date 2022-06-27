using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocSocketMaui.Wrappers;

public class MessageWrapper
{
	public long Id { get; set; }
	public MessageSender Sender { get; set; }
	public DateTime Date { get; set; }
	public string Content { get; set; }

	public MessageWrapper()
	{

	}

	public MessageWrapper(long id, MessageSender sender, DateTime date, string content)
	{
		Id = id;
		Sender = sender;
		Date = date;
		Content = content;
	}
}

public enum MessageSender
{
	Server,
	Client
}
