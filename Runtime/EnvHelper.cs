using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Edgegap.Gen2SDK
{
    using EnvTicketsRequestDTO = AdvancedTicketsRequestDTO;
    using L = Logger;

    public class EnvHelper
    {
        private List<string> _ticketIds;
        private Dictionary<string, EnvTicketsRequestDTO> _ticketData =
            new Dictionary<string, EnvTicketsRequestDTO>();
        private string _matchId;
        private string _matchProfile;
        private EnvEqualityDTO _equality;
        private EnvIntersectionDTO _intersection;

        public void StoreEnvs()
        {
            IDictionary envs = Environment.GetEnvironmentVariables();

            foreach (DictionaryEntry envEntry in envs)
            {
                string key = envEntry.Key.ToString();

                if (key.Contains("MM_"))
                {
                    if (key.Contains("_TICKET_") && !key.Contains("_IDS"))
                    {
                        string id = key.Split("MM_TICKET_", StringSplitOptions.RemoveEmptyEntries)[
                            0
                        ];
                        _ticketData[id] = JsonConvert.DeserializeObject<EnvTicketsRequestDTO>(
                            envEntry.Value.ToString()
                        );
                    }
                    else if (key.Contains("_TICKET_"))
                    {
                        _ticketIds = JsonConvert.DeserializeObject<List<string>>(
                            envEntry.Value.ToString()
                        );
                    }
                    else if (key.Contains("_MATCH_PROFILE"))
                    {
                        _matchProfile = envEntry.Value.ToString();
                    }
                    else if (key.Contains("_MATCH_ID"))
                    {
                        _matchId = envEntry.Value.ToString();
                    }
                    else if (key.Contains("_EQUALITY"))
                    {
                        _equality = JsonConvert.DeserializeObject<EnvEqualityDTO>(
                            envEntry.Value.ToString()
                        );
                    }
                    else if (key.Contains("_INTERSECTION"))
                    {
                        _intersection = JsonConvert.DeserializeObject<EnvIntersectionDTO>(
                            envEntry.Value.ToString()
                        );
                    }
                }
            }
        }
    }
}
