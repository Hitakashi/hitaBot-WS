using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace hitaBot.WS.Events
{
    [DataContract]
    public class LoginMsgEventArgs : EventArgs
    {

        [DataMember(Name = "channel")]
        public string Channel { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "role")]
        public string Role { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }
    }
}