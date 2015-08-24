using System.Text;

namespace hitaBot.WS.ChatMessages
{
    public static partial class ChatMessages
    {
        private const string BaseMessage = "5:::{\"name\":\"message\",\"args\":[{";

        public static void SendJoinRequest(HitboxChat client, string channel, string name, string token)
        {
            var sb = new StringBuilder(BaseMessage);
            sb.Append("\"method\":\"joinChannel\",");
            sb.Append("\"params\":{");
            sb.Append("\"channel\":\"").Append(channel.ToLower()).Append("\",");
            sb.Append("\"name\":\"").Append(name).Append("\",");
            sb.Append("\"token\":\"").Append(token);
            sb.Append("\"}}]}");

            //Logger.Info("[Client@" + client.GetChannel() + "] Sending join request...");

            client.Send(sb.ToString());
        }

        public static void SendLogout(HitboxChat client, string name)
        {
            var sb = new StringBuilder(BaseMessage);
            sb.Append("\"method\":\"partChannel\",");
            sb.Append("\"params\":{");
            sb.Append("\"name\":\"").Append(name);
            sb.Append("\"}}]}");

            //Logger.Info("[Client@" + client.GetChannel() + "] Sending join request...");

            client.Send(sb.ToString());
        }

        public static void SendChatMessage(HitboxChat client, string channel, string name, string nameColor, string text)
        {
            var sb = new StringBuilder(BaseMessage);
            sb.Append("\"method\":\"chatMsg\",");
            sb.Append("\"params\":{");
            sb.Append("\"channel\":\"").Append(channel).Append("\",");
            sb.Append("\"name\":\"").Append(name).Append("\",");
            sb.Append("\"nameColor\":\"").Append(nameColor).Append("\",");
            sb.Append("\"text\":\"").Append(text);
            sb.Append("\"}}]}");

            //Logger.Info("[Client@" + client.GetChannel() + "] Sending join request...");

            client.Send(sb.ToString());
        }

        public static void SendDirectMessage(HitboxChat client, string channel, string from, string to, string nameColor, string text)
        {
            var sb = new StringBuilder(BaseMessage);
            sb.Append("\"method\":\"directMsg\",");
            sb.Append("\"params\":{");
            sb.Append("\"channel\":\"").Append(channel.ToLower()).Append("\",");
            sb.Append("\"from\":\"").Append(from).Append("\",");
            sb.Append("\"to\":\"").Append(to).Append("\",");
            sb.Append("\"nameColor\":\"").Append(nameColor).Append("\",");
            sb.Append("\"text\":\"").Append(text);
            sb.Append("\"}}]}");

            //Logger.Info("[Client@" + client.GetChannel() + "] Sending join request...");

            client.Send(sb.ToString());
        }

        public static void SendUserList(HitboxChat client, string channel)
        {
            var sb = new StringBuilder(BaseMessage);
            sb.Append("\"method\":\"getChannelUserList\",");
            sb.Append("\"params\":{");
            sb.Append("\"channel\":\"").Append(channel.ToLower());
            sb.Append("\"}}]}");

            //Logger.Info("[Client@" + client.GetChannel() + "] Sending join request...");

            client.Send(sb.ToString());
        }

        public static void SendChannelUserInfo(HitboxChat client, string channel, string name)
        {
            var sb = new StringBuilder(BaseMessage);
            sb.Append("\"method\":\"getChannelUser\",");
            sb.Append("\"params\":{");
            sb.Append("\"channel\":\"").Append(channel.ToLower()).Append("\",");
            sb.Append("\"name\":\"").Append(name);
            sb.Append("\"}}]}");

            //Logger.Info("[Client@" + client.GetChannel() + "] Sending join request...");

            client.Send(sb.ToString());
        }

        public static void SendMediaLog(HitboxChat client, string channel, string type, string name, string token)
        {
            var sb = new StringBuilder(BaseMessage);
            sb.Append("\"method\":\"mediaLog\",");
            sb.Append("\"params\":{");
            sb.Append("\"channel\":\"").Append(channel.ToLower()).Append("\",");
            sb.Append("\"type\":\"").Append(type).Append("\",");
            sb.Append("\"name\":\"").Append(name).Append("\",");
            sb.Append("\"token\":\"").Append(token);
            sb.Append("\"}}]}");

            //Logger.Info("[Client@" + client.GetChannel() + "] Sending join request...");

            client.Send(sb.ToString());
        }
    }
}