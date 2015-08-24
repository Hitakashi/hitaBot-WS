using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace hitaBot.WS.Events
{
    [DataContract]
    public class ServerMsgEventArgs : EventArgs
    {

        [DataMember(Name = "channel")]
        public string Channel { get; set; }

        [DataMember(Name = "text")]
        public Text Text { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "time")]
        public int Time { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }
    }

    [DataContract]
    public class Text
    {

        [DataMember(Name = "media")]
        public Media Media { get; set; }
    }

    [DataContract]
    public class Media
    {

        [DataMember(Name = "category_id")]
        public string CategoryId { get; set; }

        [DataMember(Name = "category_name")]
        public string CategoryName { get; set; }

        [DataMember(Name = "category_name_short")]
        public object CategoryNameShort { get; set; }

        [DataMember(Name = "category_seo_key")]
        public string CategorySeoKey { get; set; }

        [DataMember(Name = "category_viewers")]
        public string CategoryViewers { get; set; }

        [DataMember(Name = "category_media_count")]
        public string CategoryMediaCount { get; set; }

        [DataMember(Name = "category_channels")]
        public object CategoryChannels { get; set; }

        [DataMember(Name = "category_logo_small")]
        public object CategoryLogoSmall { get; set; }

        [DataMember(Name = "category_logo_large")]
        public string CategoryLogoLarge { get; set; }

        [DataMember(Name = "category_updated")]
        public string CategoryUpdated { get; set; }

        [DataMember(Name = "media_status")]
        public string MediaStatus { get; set; }

        [DataMember(Name = "media_category_id")]
        public string MediaCategoryId { get; set; }
    }
}