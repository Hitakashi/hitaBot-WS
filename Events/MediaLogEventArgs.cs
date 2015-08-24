using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace hitaBot.WS.Events
{
    [DataContract]
    public class MediaLogEventArgs : EventArgs
    {

        [DataMember(Name = "channel")]
        public string Channel { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "data")]
        public IList<Datum> Data { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }
    }

    [DataContract]
    public class Datum
    {

        [DataMember(Name = "channel")]
        public string Channel { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "nameColor")]
        public string NameColor { get; set; }

        [DataMember(Name = "time")]
        public int Time { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "size")]
        public Size Size { get; set; }

        [DataMember(Name = "proxyUrl")]
        public string ProxyUrl { get; set; }
    }

    [DataContract]
    public class Size
    {

        [DataMember(Name = "x")]
        public int X { get; set; }

        [DataMember(Name = "y")]
        public int Y { get; set; }
    }

}