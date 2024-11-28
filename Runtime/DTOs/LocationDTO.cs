using Newtonsoft.Json;

namespace Edgegap.Gen2SDK
{
    public class LocationDTO
    {
        [JsonProperty("city")]
        public string City;

        [JsonProperty("country")]
        public string Country;

        [JsonProperty("continent")]
        public string Continent;

        [JsonProperty("administrative_division")]
        public string AdministrativeDivision;

        [JsonProperty("timezone")]
        public string Timezone;

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
