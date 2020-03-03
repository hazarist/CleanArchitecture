using Newtonsoft.Json;

namespace CelanArchitecture.Core.DTOs
{
    public class UserDto
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("guid")]
        public string Guid { get; set; }

        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }

    }
}
