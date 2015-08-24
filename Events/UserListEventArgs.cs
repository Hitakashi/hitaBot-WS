using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace hitaBot.WS.Events
{
    [DataContract]
    public class Data
    {

        [DataMember(Name = "Guests")]
        public object Guests { get; set; }

        [DataMember(Name = "admin")]
        public IList<string> Admin { get; set; }

        [DataMember(Name = "user")]
        public IList<string> User { get; set; }

        [DataMember(Name = "anon")]
        public IList<string> Anon { get; set; }

        [DataMember(Name = "isFollower")]
        public IList<string> IsFollower { get; set; }

        [DataMember(Name = "isSubscriber")]
        public IList<string> IsSubscriber { get; set; }

        [DataMember(Name = "isStaff")]
        public IList<string> IsStaff { get; set; }

        [DataMember(Name = "isCommunity")]
        public IList<string> IsCommunity { get; set; }
    }

    [DataContract]
    public class UserListEventArgs : EventArgs
    {

        [DataMember(Name = "channel")]
        public string Channel { get; set; }

        [DataMember(Name = "data")]
        public Data Data { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }
    }

}