using Newtonsoft.Json;

namespace Edgegap.Gen2SDK
{
    public class EnvEqualityDTO
    {
        [JsonProperty("selected_game_mode")]
        public string SelectedGameMode;

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
