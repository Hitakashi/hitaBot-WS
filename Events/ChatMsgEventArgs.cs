using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace hitaBot.WS.Events
{
    [DataContract]
    public class ChatMsgEventArgs : EventArgs
    {
        [DataMember(Name = "channel", EmitDefaultValue = true)]
        public string Channel { get; internal set; }

        [DataMember(Name = "name", EmitDefaultValue = true)]
        public string Name { get; internal set; }

        [DataMember(Name = "nameColor", EmitDefaultValue = true)]
        public string NameColor { get; internal set; }

        [DataMember(Name = "text", EmitDefaultValue = true)]
        public string Text { get; internal set; }

        [DataMember(Name = "time", EmitDefaultValue = true)]
        public string Time { get; internal set; }

        [DataMember(Name = "role", EmitDefaultValue = true)]
        public string Role { get; internal set; }

        [DataMember(Name = "isFollower", EmitDefaultValue = true)]
        public bool IsFollower { get; internal set; }

        [DataMember(Name = "isSubscriber", EmitDefaultValue = true)]
        public bool IsSubscriber { get; internal set; }

        [DataMember(Name = "isOwner", EmitDefaultValue = true)]
        public bool IsOwner { get; internal set; }

        [DataMember(Name = "isStaff", EmitDefaultValue = true)]
        public bool IsStaff { get; internal set; }

        [DataMember(Name = "isCommunity", EmitDefaultValue = true)]
        public bool IsCommunity { get; internal set; }

        [DataMember(Name = "media", EmitDefaultValue = true)]
        public bool Media { get; internal set; }

        [DataMember(Name = "image", EmitDefaultValue = true)]
        public string Image { get; internal set; }

        [DataMember(Name = "buffer", EmitDefaultValue = false)]
        public bool Buffer { get; internal set; }

        [DataMember(Name = "buffersent", EmitDefaultValue = false)]
        public bool Buffersent { get; internal set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }
    }

}