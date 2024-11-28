using Newtonsoft.Json;

namespace Edgegap.Gen2SDK
{
    public class MonitorResponseDTO
    {
        [JsonProperty("status")]
        public string Status;

        [JsonProperty("message")]
        public string Message;

        [JsonProperty("healthy_components")]
        public int HealthyComponents;

        [JsonProperty("unhealthy_components")]
        public int UnhealthyComponents;

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
