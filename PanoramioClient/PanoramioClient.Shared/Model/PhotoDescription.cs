using System;
using Newtonsoft.Json;

namespace PanoramioClient.Model
{
    public class PhotoDescription
    {
        [JsonProperty("photo_id")]
        public int PhotoId { get; set; }

        [JsonProperty("photo_title")]
        public string Title { get; set; }

        [JsonProperty("photo_url")]
        public string PhotoUrl { get; set; }

        [JsonProperty("photo_file_url")]
        public string PhotoFileUrl { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("width")]
        public double Width { get; set; }

        [JsonProperty("height")]
        public double Height { get; set; }

        [JsonProperty("upload_date")]
        public DateTime UploadDate { get; set; }

        [JsonProperty("owner_id")]
        public double OwnerId { get; set; }

        [JsonProperty("owner_name")]
        public string OwnerName { get; set; }

        [JsonProperty("owner_url")]
        public string OwnerUrl { get; set; }
    }
}