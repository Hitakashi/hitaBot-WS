## How to use

You first create the WebSocket client by calling:

```C#
HitboxChat htbxChat = new HitboxChat().CreateClient();
```

You can enable debug mode by passing 'true' to the HitboxChat contructor. If you're using this in a GUI application, it will create a console and print the debug there. If you're using this in a console application it will print inside your console.

You can then subscribe to any event you want to use by calling:

```C#
htbxChat.OnOpen += htbxChat_OnOpen;

private void htbxChat_OnOpen(object sender, EventArgs e)
{
  // Your code, You should be joining a channel here anyways.
}
```

Finally, You can tell it to connect to Hitbox Chat Server:

```C#
htbxChat.Connect();
```

You can then use methods inside 'ChatMessages' and pass context of HitboxChat to send messages. You can also use HitboxChat.Send(String); and pass the correct JSON messages to send to the server.

Why do we connect after the events? Because if you connect before you won't recieve the event for connection.

In my opinion, I suggest a 'bot' class to handle creating and storing 'client' objects which then start the above process, which contains properties on the channel you're joining.

### Valid Events

* OnChatMsg
* OnBanList
* OnChatLog
* OnDirectMsg
* OnInfoMsg
* OnLoginMsg
* OnMediaLog
* OnServerMsg
* OnUserInfo
* OnUserList
* OnSlowMsg
* OnNotifyMsg
* OnClose
* OnOpen
