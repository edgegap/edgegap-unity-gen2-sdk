using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Edgegap.Gen2SDK
{
    public abstract class TicketsRequestDTO<A>
    {
        [JsonProperty("profile")]
        public string Profile;

        [JsonProperty("attributes")]
        public A Attributes;

#nullable enable
        [JsonProperty("player_ip")]
        public string? PlayerIP;

#nullable disable

        public TicketsRequestDTO(string profile)
        {
            Profile = profile;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class SimpleTicketsRequestDTO : TicketsRequestDTO<LatenciesAttributesDTO>
    {
        public SimpleTicketsRequestDTO(Dictionary<string, float> latencyBeacons)
            : base("simple-example")
        {
            Attributes = new LatenciesAttributesDTO(latencyBeacons);
        }
    }

    public class AdvancedTicketsRequestDTO : TicketsRequestDTO<AdvancedTicketsAttributesDTO>
    {
        public AdvancedTicketsRequestDTO(
            Dictionary<string, float> latencyBeacons,
            int eloRating,
            string selectedGameMode,
            string[] selectedMap,
            string selectedRegion
        )
            : base("advanced-example")
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

    public class AdvancedTicketsAttributesDTO : LatenciesAttributesDTO
    {
        [JsonProperty("elo_rating")]
        public int EloRating;

        [JsonProperty("selected_game_mode")]
        public string SelectedGameMode;

        [JsonProperty("selected_map")]
        public string[] SelectedMap;

        [JsonProperty("selected_region")]
        public string SelectedRegion;

        public AdvancedTicketsAttributesDTO(
            Dictionary<string, float> beacons,
            int eloRating,
            string selectedGameMode,
            string[] selectedMap,
            string selectedRegion
        )
            : base(beacons)
        {
            EloRating = eloRating;
            SelectedGameMode = selectedGameMode;
            SelectedMap = selectedMap;
            SelectedRegion = selectedRegion;
        }
    }

    public class LatenciesAttributesDTO
    {
        [JsonProperty("beacons")]
        public Dictionary<string, float> Beacons;

        public LatenciesAttributesDTO(Dictionary<string, float> beacons)
        {
            Beacons = beacons;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
