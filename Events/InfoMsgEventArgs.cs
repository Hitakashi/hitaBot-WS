using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace hitaBot.WS.Events
{
    [DataContract]
    public class InfoMsgEventArgs : EventArgs
    {

        [DataMember(Name = "text")]
        public string Text { get; set; }

        [DataMember(Name = "channel")]
        public string Channel { get; set; }

        [DataMember(Name = "timestamp")]
        public int Timestamp { get; set; }

        [DataMember(Name = "subscriber")]
        public string Subscriber { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "time")]
        public int Time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Typically user that's being acted upon. Ex: This user was banned.</remarks>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "action")]
        public string Action { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }
    }
}