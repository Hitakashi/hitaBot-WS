using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace hitaBot.WS.Events
{
    [DataContract]
    public class DirectMsgEventArgs : EventArgs
    {

        [DataMember(Name = "from")]
        public string From { get; set; }

        [DataMember(Name = "nameColor")]
        public string NameColor { get; set; }

        [DataMember(Name = "text")]
        public string Text { get; set; }

        [DataMember(Name = "time")]
        public int Time { get; set; }

        [DataMember(Name = "isStaff")]
        public bool IsStaff { get; set; }

        [DataMember(Name = "isCommunity")]
        public bool IsCommunity { get; set; }

        [DataMember(Name = "channel")]
        public string Channel { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }
    }
}