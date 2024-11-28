using Newtonsoft.Json;

namespace Edgegap.Gen2SDK
{
    public class GroupTicketsResponseDTO
    {
        [JsonProperty("player_tickets")]
        public TicketResponseDTO[] Tickets;

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
