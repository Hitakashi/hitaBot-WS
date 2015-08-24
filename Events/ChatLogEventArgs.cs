using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace hitaBot.WS.Events
{
    [DataContract]
    public class ChatLogEventArgs : EventArgs
    {

        [DataMember(Name = "text", EmitDefaultValue = true)]
        public string Text { get; set; }

        [DataMember(Name = "timestamp", EmitDefaultValue = true)]
        public int Timestamp { get; set; }

        [DataMember(Name = "channel", EmitDefaultValue = true)]
        public string Channel { get; set; }

        [DataMember(Name = "popover", EmitDefaultValue = false)]
        public string Popover { get; set; }

        [DataMember(Name = "buffer", EmitDefaultValue = false)]
        public bool Buffer { get; set; }

        [DataMember(Name = "buffersent", EmitDefaultValue = false)]
        public bool Buffersent { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }
    }
}