using System;
using System.Runtime.InteropServices;
using hitaBot.WS.Enums;
using hitaBot.WS.Events;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using SuperSocket.ClientEngine;
using WebSocket4Net;

namespace hitaBot.WS
{
    public sealed class HitboxChat
    {
        private static JArray wsUrl;
        public WebSocket _ws;

        public HitboxChat(bool debug = false)
        {
            PreFetchServers();
            if (Environment.GetEnvironmentVariable("wsDisableDebug") == "true") return;
            if (!debug) return;
            if (LogConsole.Instantiate())
            {
                Console.Title = "WebSocket Debug";
            }
        }


        public HitboxChat CreateClient()
        {
            // The following might crash when there's less than 5 servers. Try Check this.
            string localws;
            try
            {
                var address = new Random().Next(0, 5);
                localws = wsUrl[address].SelectToken("server_ip").ToString();
            }
            catch (Exception)
            {
                localws = wsUrl[0].SelectToken("server_ip").ToString();
            }
            Console.WriteLine(localws);
            var clientConId = new RestClient("http://" + localws);
            var requestConId = new RestRequest("/socket.io/1", Method.GET);
            var responseConId = clientConId.Execute(requestConId);
            var connectionId = responseConId.Content;

            _ws =
                new WebSocket("ws://" + localws + "/socket.io/1/websocket/" +
                              connectionId.Substring(0, connectionId.IndexOf(":", StringComparison.Ordinal)), "", WebSocketVersion.Rfc6455)
                {
                    EnableAutoSendPing = false,
                    Proxy = null
                };

            _ws.MessageReceived += Ws_OnMessage;
            _ws.Opened += Ws_OnOpen;
            _ws.Closed += _ws_OnClose;
            _ws.Error += _ws_OnError;
            return this;
        }

        private void _ws_OnClose(object sender, EventArgs e)
        {
            OnRaiseCloseMsg(e);
        }

        private void Ws_OnOpen(object sender, EventArgs e)
        {
            OnRaiseOpenMsg(e);
        }


        private void _ws_OnError(object sender, ErrorEventArgs e)
        {
            OnRaiseErrorMsg(e);
        }

        private void Ws_OnMessage(object sender, MessageReceivedEventArgs e)
        {
            // We got a message.
            var data = e.Message;

            if (data.Equals("2::"))
            {
                Send(data);
            }
            else if (data.StartsWith("5:::"))
            {
                var jsonObj = JObject.Parse(data.Substring(4));
                var args = JObject.Parse(jsonObj.GetValue("args").First.ToString());
                var method = args.GetValue("method");
                var paramsObject = args.GetValue("params");

                ChatMethod methodName;
                if (!Enum.TryParse(method.ToString(), out methodName)) return;
                switch (methodName)
                {
                    case ChatMethod.chatMsg:
                        OnRaiseChatMsg(JsonConvert.DeserializeObject<ChatMsgEventArgs>(paramsObject.ToString()));
                        break;
                    case ChatMethod.directMsg:
                        OnRaiseDirectMsg(JsonConvert.DeserializeObject<DirectMsgEventArgs>(paramsObject.ToString()));
                        break;
                    case ChatMethod.infoMsg:
                        OnRaiseInfoMsg(JsonConvert.DeserializeObject<InfoMsgEventArgs>(paramsObject.ToString()));
                        break;
                    case ChatMethod.loginMsg:
                        OnRaiseLoginMsg(JsonConvert.DeserializeObject<LoginMsgEventArgs>(paramsObject.ToString()));
                        break;
                    case ChatMethod.chatLog:
                        OnRaiseChatLog(JsonConvert.DeserializeObject<ChatLogEventArgs>(paramsObject.ToString()));
                        break;
                    case ChatMethod.userList:
                        OnRaiseUserList(JsonConvert.DeserializeObject<UserListEventArgs>(paramsObject.ToString()));
                        break;
                    case ChatMethod.userInfo:
                        OnRaiseUserInfo(JsonConvert.DeserializeObject<UserInfoEventArgs>(paramsObject.ToString()));
                        break;
                    case ChatMethod.mediaLog:
                        OnRaiseMediaLog(JsonConvert.DeserializeObject<MediaLogEventArgs>(paramsObject.ToString()));
                        break;
                    case ChatMethod.banList:
                        OnRaiseBanList(JsonConvert.DeserializeObject<BanListEventArgs>(paramsObject.ToString()));
                        break;
                    case ChatMethod.slowMsg:
                        OnRaiseSlowMsg(JsonConvert.DeserializeObject<SlowMsgEventArgs>(paramsObject.ToString()));
                        break;
                    case ChatMethod.serverMsg:
                        OnRaiseServerMsg(JsonConvert.DeserializeObject<ServerMsgEventArgs>(paramsObject.ToString()));
                        break;
                    case ChatMethod.notifyMsg:
                        OnRaiseNotifyMsg(JsonConvert.DeserializeObject<NotifyMsgEventArgs>(paramsObject.ToString()));
                        break;
                }
            }
        }

        public event EventHandler<ChatMsgEventArgs> OnChatMsg;
        public event EventHandler<BanListEventArgs> OnBanList;
        public event EventHandler<ChatLogEventArgs> OnChatLog;
        public event EventHandler<DirectMsgEventArgs> OnDirectMsg;
        public event EventHandler<InfoMsgEventArgs> OnInfoMsg;
        public event EventHandler<LoginMsgEventArgs> OnLoginMsg;
        public event EventHandler<MediaLogEventArgs> OnMediaLog;
        public event EventHandler<ServerMsgEventArgs> OnServerMsg;
        public event EventHandler<UserInfoEventArgs> OnUserInfo;
        public event EventHandler<UserListEventArgs> OnUserList;
        public event EventHandler<SlowMsgEventArgs> OnSlowMsg;
        public event EventHandler<NotifyMsgEventArgs> OnNotifyMsg;
        public event EventHandler<EventArgs> OnClose;
        public event EventHandler<EventArgs> OnOpen;
        public event EventHandler<ErrorEventArgs> OnError;


        private void OnRaiseChatMsg(ChatMsgEventArgs e)
        {
            var handler = OnChatMsg;

            if (handler == null) return;

            Console.WriteLine("Sending out chat event for " + e.Channel + " \n" + e);
            handler(this, e);
        }

        private void OnRaiseOpenMsg(EventArgs e)
        {
            var handler = OnOpen;

            if (handler == null) return;

            Console.WriteLine("Sending out open event.");
            handler(this, e);
        }

        private void OnRaiseCloseMsg(EventArgs e)
        {
            var handler = OnClose;

            if (handler == null) return;

            Console.WriteLine("Sending out close event.");
            handler(this, e);
        }

        private void OnRaiseErrorMsg(ErrorEventArgs e)
        {
            var handler = OnError;

            if (handler == null) return;

            Console.WriteLine("Sending out error event.");
            handler(this, e);
        }

        private void OnRaiseBanList(BanListEventArgs e)
        {
            var handler = OnBanList;

            if (handler == null) return;

            Console.WriteLine("Sending out ban list event for " + e.Channel + " \n" + e);
            handler(this, e);
        }

        private void OnRaiseChatLog(ChatLogEventArgs e)
        {
            var handler = OnChatLog;

            if (handler == null) return;

            Console.WriteLine("Sending out chat log event for " + e.Channel + " \n" + e);
            handler(this, e);
        }

        private void OnRaiseDirectMsg(DirectMsgEventArgs e)
        {
            var handler = OnDirectMsg;

            if (handler == null) return;

            Console.WriteLine("Sending out direct message event from " + e.From + " sent from " + e.Channel + " channel");
            handler(this, e);
        }

        private void OnRaiseInfoMsg(InfoMsgEventArgs e)
        {
            var handler = OnInfoMsg;

            if (handler == null) return;

            Console.WriteLine("Sending out info message event for " + e.Channel + "\n" + e);
            handler(this, e);
        }

        private void OnRaiseLoginMsg(LoginMsgEventArgs e)
        {
            var handler = OnLoginMsg;

            if (handler == null) return;

            Console.WriteLine("Sending out login message event for " + e.Channel + "\n" + e);
            handler(this, e);
        }

        private void OnRaiseMediaLog(MediaLogEventArgs e)
        {
            var handler = OnMediaLog;

            if (handler == null) return;

            Console.WriteLine("Sending out media log message event for " + e.Channel + "\n" + e);
            handler(this, e);
        }

        private void OnRaiseServerMsg(ServerMsgEventArgs e)
        {
            var handler = OnServerMsg;

            if (handler == null) return;

            Console.WriteLine("Sending out server message event for " + e.Channel + "\n" + e);
            handler(this, e);
        }

        private void OnRaiseUserInfo(UserInfoEventArgs e)
        {
            var handler = OnUserInfo;

            if (handler == null) return;

            Console.WriteLine("Sending out user info event for " + e.Channel + "\n" + e);
            handler(this, e);
        }

        private void OnRaiseUserList(UserListEventArgs e)
        {
            var handler = OnUserList;

            if (handler == null) return;

            Console.WriteLine("Sending out user list event for " + e.Channel + "\n" + e);
            handler(this, e);
        }

        private void OnRaiseSlowMsg(SlowMsgEventArgs e)
        {
            var handler = OnSlowMsg;

            if (handler == null) return;

            Console.WriteLine("Sending out slow message event for " + e.Channel + "\n" + e);
            handler(this, e);
        }

        private void OnRaiseNotifyMsg(NotifyMsgEventArgs e)
        {
            var handler = OnNotifyMsg;

            if (handler == null) return;

            Console.WriteLine("Sending out notify message event " + e.Channel + "\n" + e);
            handler(this, e);
        }

        public void Send(string message)
        {
            _ws.Send(message);
        }

        public void Connect()
        {
            _ws.Open();
        }

        public void Close()
        {
            _ws.Close();
        }

        public static void PreFetchServers()
        {
            var client = new RestClient("https://api.hitbox.tv");
            var request = new RestRequest("/chat/servers", Method.GET);
            var response = client.Execute(request);
            var jsonWsUrl = response.Content;
            Console.WriteLine("Content: " + jsonWsUrl);
            wsUrl = JArray.Parse(jsonWsUrl);
        }

        //private string channel;

        private static class LogConsole
        {
            public static bool Instantiate()
            {
                return AllocConsole();
            }

            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool AllocConsole();
        }
    }
}