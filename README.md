
# PocSocketMaui

## Presentation
![image](https://user-images.githubusercontent.com/90254070/175909236-5b0a8f81-c6c1-4d1e-91c0-58ccf98f33aa.png)

This POC demonstrates the use of WebSockets in a .NET MAUI mobile application. We can then easily communicate between a server and a client and this live.To send messages on the server side we use the site https://socketsbay.com/test-websockets. 

![image](https://user-images.githubusercontent.com/90254070/175907923-84945673-b4a9-4415-b3f4-1a4fbe74a09d.png)
Through this interface you can send messages and they will appear in the mobile application.

![1656321667607_iphone13blue_portrait](https://user-images.githubusercontent.com/90254070/175908085-960e4c8b-ba3c-4511-94af-b6465a202b59.png)

## How to use

### Constants file

We can easily change the server with which the mobile application communicates by modifying the constants file : 

```C#
public class Constants
{
	//public const string SocketServerUrl = "wss://demo.piesocket.com/v3/channel_1?api_key=VCXCEuvhGcBDP7XhiJJUDvR1e1D3eiVjgZ9VRiaV&notify_self";
	public const string SocketServerUrl = "wss://socketsbay.com/wss/v2/2/demo/";
}
```

### SocketService

```C#
public interface ISocketService
{
	/// <summary>
	/// History of all messages (sended and received)
	/// </summary>
	public ReadOnlyObservableCollection<MessageWrapper> Messages { get; }

	/// <summary>
	/// Connect to server
	/// </summary>
	/// <returns></returns>
	Task ConnectToServerAsync();

	/// <summary>
	/// Disconnect to the server
	/// </summary>
	/// <returns></returns>
	Task DisconnectToServerAsync();

	/// <summary>
	/// Read message
	/// </summary>
	/// <returns></returns>
	Task ReadMessageAsync();

	/// <summary>
	/// Send a message to the server and add it in history list
	/// </summary>
	/// <param name="message"></param>
	/// <returns></returns>
	Task<bool> SendMessageAsync(string message);
}

```



