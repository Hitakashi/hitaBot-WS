using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace hitaBot.WS.Events
{
    [DataContract]
    public class UserInfoEventArgs : EventArgs
    {

        [DataMember(Name = "channel")]
        public string Channel { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "timestamp")]
        public int Timestamp { get; set; }

        [DataMember(Name = "role")]
        public string Role { get; set; }

        [DataMember(Name = "isFollower")]
        public bool IsFollower { get; set; }

        [DataMember(Name = "isSubscriber")]
        public bool IsSubscriber { get; set; }

        [DataMember(Name = "isOwner")]
        public bool IsOwner { get; set; }

        [DataMember(Name = "isStaff")]
        public bool IsStaff { get; set; }

        [DataMember(Name = "isCommunity")]
        public bool IsCommunity { get; set; }

        [DataMember(Name = "banned")]
        public bool Banned { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }
    }

}