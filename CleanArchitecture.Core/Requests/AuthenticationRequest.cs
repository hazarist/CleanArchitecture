using Newtonsoft.Json;

namespace CelanArchitecture.Core.Requests
{
    public class AuthenticationRequest
    {
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
