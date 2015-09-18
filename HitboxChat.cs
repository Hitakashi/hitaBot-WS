using System;
using System.Net.Http;
using System.Runtime.InteropServices;
using hitaBot.Refit;
using hitaBot.Refit.api;
using hitaBot.WS.Enums;
using hitaBot.WS.Events;
using hitaBot.WS.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using WebSocketSharp;
using Logger = NLog.Logger;

namespace hitaBot.WS
{
    public sealed class HitboxChat
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private WebSocket _ws;

        public HitboxChat(bool debug = false)
        {
            if (Environment.GetEnvironmentVariable("Bluemix") == "true") return;
            if (!debug) return;
            WebSocketLogging.CreateLogConfig();
            if (LogConsole.Instantiate())
            {
                Console.Title = "WebSocket Debug";
            }
        }


        public HitboxChat CreateClient()
        {
            // Logic get Websocket HTTP URL
            // Use that URL to get Connection ID
            var server = ServiceGenerator.createService<IChat>().getChatServers().Result[0].ServerIp;

            string connectionId;

            using (var httpClient = new HttpClient())
            {
                connectionId = httpClient.GetStringAsync("http://" + server + "/socket.io/1").Result;
            }

            _ws =
                new WebSocket("ws://" + server + "/socket.io/1/websocket/" +
                              connectionId.Substring(0, connectionId.IndexOf(":", StringComparison.Ordinal)));
            _ws.OnMessage += Ws_OnMessage;
            _ws.OnOpen += Ws_OnOpen;
            _ws.OnClose += _ws_OnClose;
            return this;
        }

        private void _ws_OnClose(object sender, CloseEventArgs e)
        {
            OnRaiseCloseMsg(e);
        }

        private void Ws_OnOpen(object sender, EventArgs e)
        {
            OnRaiseOpenMsg(e);
        }

        private void Ws_OnMessage(object sender, MessageEventArgs e)
        {
            // We got a message.
            var data = e.Data;

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
                if (Enum.TryParse(method.ToString(), out methodName))
                {
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


        private void OnRaiseChatMsg(ChatMsgEventArgs e)
        {
            var handler = OnChatMsg;

            if (handler == null) return;

            Logger.Info("Sending out chat event for " + e.Channel + " \n" + e);
            handler(this, e);
        }

        private void OnRaiseOpenMsg(EventArgs e)
        {
            var handler = OnOpen;

            if (handler == null) return;

            Logger.Info("Sending out open event.");
            handler(this, e);
        }

        private void OnRaiseCloseMsg(EventArgs e)
        {
            var handler = OnClose;

            if (handler == null) return;

            Logger.Info("Sending out close event.");
            handler(this, e);
        }

        private void OnRaiseBanList(BanListEventArgs e)
        {
            var handler = OnBanList;

            if (handler == null) return;

            Logger.Info("Sending out ban list event for " + e.Channel + " \n" + e);
            handler(this, e);
        }

        private void OnRaiseChatLog(ChatLogEventArgs e)
        {
            var handler = OnChatLog;

            if (handler == null) return;

            Logger.Info("Sending out chat log event for " + e.Channel + " \n" + e);
            handler(this, e);
        }

        private void OnRaiseDirectMsg(DirectMsgEventArgs e)
        {
            var handler = OnDirectMsg;

            if (handler == null) return;

            Logger.Info("Sending out direct message event from " + e.From + " sent from " + e.Channel + " channel");
            handler(this, e);
        }

        private void OnRaiseInfoMsg(InfoMsgEventArgs e)
        {
            var handler = OnInfoMsg;

            if (handler == null) return;

            Logger.Info("Sending out info message event for " + e.Channel + "\n" + e);
            handler(this, e);
        }

        private void OnRaiseLoginMsg(LoginMsgEventArgs e)
        {
            var handler = OnLoginMsg;

            if (handler == null) return;

            Logger.Info("Sending out login message event for " + e.Channel + "\n" + e);
            handler(this, e);
        }

        private void OnRaiseMediaLog(MediaLogEventArgs e)
        {
            var handler = OnMediaLog;

            if (handler == null) return;

            Logger.Info("Sending out media log message event for " + e.Channel + "\n" + e);
            handler(this, e);
        }

        private void OnRaiseServerMsg(ServerMsgEventArgs e)
        {
            var handler = OnServerMsg;

            if (handler == null) return;

            Logger.Info("Sending out server message event for " + e.Channel + "\n" + e);
            handler(this, e);
        }

        private void OnRaiseUserInfo(UserInfoEventArgs e)
        {
            var handler = OnUserInfo;

            if (handler == null) return;

            Logger.Info("Sending out user info event for " + e.Channel + "\n" + e);
            handler(this, e);
        }

        private void OnRaiseUserList(UserListEventArgs e)
        {
            var handler = OnUserList;

            if (handler == null) return;

            Logger.Info("Sending out user list event for " + e.Channel + "\n" + e);
            handler(this, e);
        }

        private void OnRaiseSlowMsg(SlowMsgEventArgs e)
        {
            var handler = OnSlowMsg;

            if (handler == null) return;

            Logger.Info("Sending out slow message event for " + e.Channel + "\n" + e);
            handler(this, e);
        }

        private void OnRaiseNotifyMsg(NotifyMsgEventArgs e)
        {
            var handler = OnNotifyMsg;

            if (handler == null) return;

            Logger.Info("Sending out notify message event " + e.Channel + "\n" + e);
            handler(this, e);
        }

        public void Send(string message)
        {
            _ws.Send(message);
        }

        public void Connect()
        {
            _ws.Connect();
        }

        public void Close()
        {
            _ws.Close();
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