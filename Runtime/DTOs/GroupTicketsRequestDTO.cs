using Newtonsoft.Json;

namespace Edgegap.Gen2SDK
{
    public class GroupTicketsRequestDTO<A>
    {
        [JsonProperty("player_tickets")]
        public TicketsRequestDTO<A>[] Tickets;

        public GroupTicketsRequestDTO(TicketsRequestDTO<A>[] tickets)
        {
            Tickets = tickets;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
