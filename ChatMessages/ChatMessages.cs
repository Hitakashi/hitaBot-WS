using System.Text;

namespace hitaBot.WS.ChatMessages
{
    public partial class ChatMessages
    {
        public static void SendTimeoutUser(HitboxChat client, string channel, string name, string token, int timeout)
        {
            var sb = new StringBuilder(BaseMessage);
            sb.Append("\"method\":\"kickUser\",");
            sb.Append("\"params\":{");
            sb.Append("\"channel\":\"").Append(channel.ToLower()).Append("\",");
            sb.Append("\"name\":\"").Append(name).Append("\",");
            sb.Append("\"token\":\"").Append(token).Append("\",");
            sb.Append("\"timeout\":").Append(timeout);
            sb.Append("\"}}]}");

            //Logger.Info("[Client@" + client.GetChannel() + "] Sending join request...");

            client.Send(sb.ToString());
        }

        public static void SendBanUser(HitboxChat client, string channel, string name)
        {
            var sb = new StringBuilder(BaseMessage);
            sb.Append("\"method\":\"banUser\",");
            sb.Append("\"params\":{");
            sb.Append("\"channel\":\"").Append(channel.ToLower()).Append("\",");
            sb.Append("\"name\":\"").Append(name);
            sb.Append("\"}}]}");

            //Logger.Info("[Client@" + client.GetChannel() + "] Sending join request...");

            client.Send(sb.ToString());
        }

        public static void SendUnBanUser(HitboxChat client, string channel, string name, string token)
        {
            var sb = new StringBuilder(BaseMessage);
            sb.Append("\"method\":\"unbanUser\",");
            sb.Append("\"params\":{");
            sb.Append("\"channel\":\"").Append(channel.ToLower()).Append("\",");
            sb.Append("\"name\":\"").Append(name).Append("\",");
            sb.Append("\"token\":\"").Append(token);
            sb.Append("\"}}]}");

            //Logger.Info("[Client@" + client.GetChannel() + "] Sending join request...");

            client.Send(sb.ToString());
        }

        public static void SendMakeMod(HitboxChat client, string channel, string name, string token)
        {
            var sb = new StringBuilder(BaseMessage);
            sb.Append("\"method\":\"makeMod\",");
            sb.Append("\"params\":{");
            sb.Append("\"channel\":\"").Append(channel.ToLower()).Append("\",");
            sb.Append("\"name\":\"").Append(name).Append("\",");
            sb.Append("\"token\":\"").Append(token);
            sb.Append("\"}}]}");

            //Logger.Info("[Client@" + client.GetChannel() + "] Sending join request...");

            client.Send(sb.ToString());
        }

        public static void SendRemoveMod(HitboxChat client, string channel, string name, string token)
        {
            var sb = new StringBuilder(BaseMessage);
            sb.Append("\"method\":\"removeMod\",");
            sb.Append("\"params\":{");
            sb.Append("\"channel\":\"").Append(channel.ToLower()).Append("\",");
            sb.Append("\"name\":\"").Append(name).Append("\",");
            sb.Append("\"token\":\"").Append(token);
            sb.Append("\"}}]}");

            //Logger.Info("[Client@" + client.GetChannel() + "] Sending join request...");

            client.Send(sb.ToString());
        }

        public static void SendSlowSubMode(HitboxChat client, string channel, int time, bool sub)
        {
            var sb = new StringBuilder(BaseMessage);
            sb.Append("\"method\":\"removeMod\",");
            sb.Append("\"params\":{");
            sb.Append("\"channel\":\"").Append(channel.ToLower()).Append("\",");
            if (!sub)
                sb.Append("\"time\":").Append(time).Append(",");
            sb.Append("\"subscriber\":\"").Append(sub);
            if (sub)
            {
                sb.Append("\",");
                sb.Append("\"sub\":").Append(0);
            }
            sb.Append("\"}}]}");

            //Logger.Info("[Client@" + client.GetChannel() + "] Sending join request...");

            client.Send(sb.ToString());
        }
    }
}