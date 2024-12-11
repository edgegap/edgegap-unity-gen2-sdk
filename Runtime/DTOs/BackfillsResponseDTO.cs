using System.Collections.Generic;
using Newtonsoft.Json;

namespace Edgegap.Gen2SDK
{
    public class BackfillsResponseDTO
    {
        [JsonProperty("id")]
        public string ID;

        [JsonProperty("profile")]
        public string Profile;

        [JsonProperty("status")]
        public string Status;

        [JsonProperty("tickets")]
        public Dictionary<string, TicketsResponseDTO> Tickets;

#nullable enable
        [JsonProperty("assigned_ticket")]
        public TicketsResponseDTO? Assignment;

#nullable disable
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
