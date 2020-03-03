using Newtonsoft.Json;

namespace CelanArchitecture.Core.Requests
{
    public class RegistrationRequest
    {
        [JsonRequired]
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonRequired]
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonRequired]
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonRequired]
        [JsonProperty("surname")]
        public string Surname { get; set; }
        [JsonRequired]
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }
    }
}
