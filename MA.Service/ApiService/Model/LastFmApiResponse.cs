namespace MA.Service.ApiService
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;

    public class LastFmApiResponse
    {
        public partial class LastFmApi
        {
            [JsonProperty("toptracks")]
            public Toptracks Toptracks { get; set; }
        }

        public partial class Toptracks
        {
            [JsonProperty("track")]
            public List<Track> Track { get; set; }

            [JsonProperty("@attr")]
            public ToptracksAttr Attr { get; set; }
        }

        public partial class ToptracksAttr
        {
            

            [JsonProperty("page")]
            [JsonConverter(typeof(ParseStringConverter))]
            public long Page { get; set; }

            [JsonProperty("perPage")]
            [JsonConverter(typeof(ParseStringConverter))]
            public long PerPage { get; set; }

            [JsonProperty("totalPages")]
            [JsonConverter(typeof(ParseStringConverter))]
            public long TotalPages { get; set; }

            [JsonProperty("total")]
            [JsonConverter(typeof(ParseStringConverter))]
            public long Total { get; set; }
        }

        public partial class Track
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("playcount")]
            [JsonConverter(typeof(ParseStringConverter))]
            public long Playcount { get; set; }

            [JsonProperty("listeners")]
            [JsonConverter(typeof(ParseStringConverter))]
            public long Listeners { get; set; }

            [JsonProperty("mbid", NullValueHandling = NullValueHandling.Ignore)]
            public Guid? Mbid { get; set; }

            [JsonProperty("url")]
            public Uri Url { get; set; }

            [JsonProperty("streamable")]
            [JsonConverter(typeof(ParseStringConverter))]
            public long Streamable { get; set; }

            [JsonProperty("artist")]
            public ArtistClass Artist { get; set; }

            [JsonProperty("image")]
            public List<Image> Image { get; set; }

            [JsonProperty("@attr")]
            public TrackAttr Attr { get; set; }
        }

        public partial class ArtistClass
        {
            

            [JsonProperty("mbid")]
            public Guid Mbid { get; set; }

            [JsonProperty("url")]
            public Uri Url { get; set; }
        }

        public partial class TrackAttr
        {
            [JsonProperty("rank")]
            [JsonConverter(typeof(ParseStringConverter))]
            public long Rank { get; set; }
        }

        public partial class Image
        {
            [JsonProperty("#text")]
            public Uri Text { get; set; }

            [JsonProperty("size")]
            public Size Size { get; set; }
        }


        public enum Size { Extralarge, Large, Medium, Small };

        
        

       

        internal class ParseStringConverter : JsonConverter
        {
            public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

            public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null) return null;
                var value = serializer.Deserialize<string>(reader);
                long l;
                if (Int64.TryParse(value, out l))
                {
                    return l;
                }
                throw new Exception("Cannot unmarshal type long");
            }

            public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
            {
                if (untypedValue == null)
                {
                    serializer.Serialize(writer, null);
                    return;
                }
                var value = (long)untypedValue;
                serializer.Serialize(writer, value.ToString());
                return;
            }

            public static readonly ParseStringConverter Singleton = new ParseStringConverter();
        }

        internal class SizeConverter : JsonConverter
        {
            public override bool CanConvert(Type t) => t == typeof(Size) || t == typeof(Size?);

            public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null) return null;
                var value = serializer.Deserialize<string>(reader);
                switch (value)
                {
                    case "extralarge":
                        return Size.Extralarge;
                    case "large":
                        return Size.Large;
                    case "medium":
                        return Size.Medium;
                    case "small":
                        return Size.Small;
                }
                throw new Exception("Cannot unmarshal type Size");
            }

            public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
            {
                if (untypedValue == null)
                {
                    serializer.Serialize(writer, null);
                    return;
                }
                var value = (Size)untypedValue;
                switch (value)
                {
                    case Size.Extralarge:
                        serializer.Serialize(writer, "extralarge");
                        return;
                    case Size.Large:
                        serializer.Serialize(writer, "large");
                        return;
                    case Size.Medium:
                        serializer.Serialize(writer, "medium");
                        return;
                    case Size.Small:
                        serializer.Serialize(writer, "small");
                        return;
                }
                throw new Exception("Cannot marshal type Size");
            }
        }
    }
}
