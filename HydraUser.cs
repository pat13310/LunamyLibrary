using Newtonsoft.Json;

namespace ApiPlateform.ApiPlateForm
{

    public class HydraUser
    {
        //[JsonProperty("@context")]
        //public required string Context { get; set; }
        //[JsonProperty("@id")]
        public required string Id { get; set; }

        [JsonProperty("@type")]
        public required string Type { get; set; }

        [JsonProperty("id")]
        public Guid UserId { get; set; }

        [JsonProperty("roles")]
        public required List<string> Roles { get; set; }

        [JsonProperty("username")]
        public required string Username { get; set; }

        [JsonProperty("email")]
        public required string Email { get; set; }

        [JsonProperty("lastName")]
        public required string LastName { get; set; }

        [JsonProperty("firstName")]
        public required string FirstName { get; set; }

        [JsonProperty("token")]
        public required string Token { get; set; }

        [JsonProperty("isVerified")]
        public bool IsVerified { get; set; }

        [JsonProperty("enabled")]
        public bool Enabled { get; set; }
    }

}
