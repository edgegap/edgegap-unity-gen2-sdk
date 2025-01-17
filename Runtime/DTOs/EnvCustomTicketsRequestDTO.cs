using System.Collections.Generic;

namespace Edgegap.Gen2SDK
{
    public class EnvCustomTicketsRequestDTO : TicketsRequestDTO<AdvancedTicketsAttributesDTO>
    {
        public EnvCustomTicketsRequestDTO(
            Dictionary<string, float> latencyBeacons,
            int eloRating,
            string selectedGameMode,
            string[] selectedMap,
            string selectedRegion
        )
            : base(EnvHelper.Instance.MatchProfile)
        {
            Attributes = new AdvancedTicketsAttributesDTO(
                latencyBeacons,
                eloRating,
                selectedGameMode,
                selectedMap,
                selectedRegion
            );
        }
    }
}
