using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace hitaBot.WS.Events
{
    [DataContract]
    public class SlowMsgEventArgs : EventArgs
    {

        [DataMember(Name = "text", EmitDefaultValue = true)]
        public string Text { get; set; }

        [DataMember(Name = "channel", EmitDefaultValue = true)]
        public string Channel { get; set; }

        [DataMember(Name = "timestamp", EmitDefaultValue = true)]
        public int Timestamp { get; set; }

        [DataMember(Name = "action", EmitDefaultValue = true)]
        public string Action { get; set; }

        [DataMember(Name = "slowTime", EmitDefaultValue = false)]
        public int? SlowTime { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }
    }

}