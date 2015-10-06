using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace hitaBot.WS.Events
{
    [DataContract]
    public class NotifyMsgEventArgs : EventArgs
    {
        [DataMember(Name = "channel")]
        public string Channel { get; set; }

        [DataMember(Name = "join")]
        public JToken Join { get; set; }

        [DataMember(Name = "part")]
        public JToken Part { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }
    }
}