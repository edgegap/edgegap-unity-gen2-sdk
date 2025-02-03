using System.Collections.Generic;
using Newtonsoft.Json;

namespace Edgegap.Gen2SDK
{
    public class EnvIntersectionDTO
    {
        [JsonProperty("selected_map")]
        public List<string> SelectedMap;

        [JsonProperty("selected_region")]
        public List<string> SelectedRegion;

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
