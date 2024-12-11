using Newtonsoft.Json;

namespace Edgegap.Gen2SDK
{
    public class GroupTicketsResponseDTO
    {
        [JsonProperty("player_tickets")]
        public TicketsResponseDTO[] Tickets;

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
