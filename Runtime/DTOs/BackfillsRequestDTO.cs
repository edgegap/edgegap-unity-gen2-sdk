using Newtonsoft.Json;

namespace Edgegap.Gen2SDK
{
    public class BackfillsRequestDTO<A>
    {
        [JsonProperty("profile")]
        public string Profile;

        [JsonProperty("attributes")]
        public BackfillsAttributesDTO Attributes;

        [JsonProperty("player_tickets")]
        public TicketsRequestDTO<A>[] Tickets;

        public BackfillsRequestDTO(TicketsRequestDTO<A>[] tickets)
        {
            Tickets = tickets;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class BackfillsAttributesDTO : AssignmentDTO
    {
        [JsonProperty("request_id")]
        public string DeploymentID;

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
