using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Edgegap.Gen2SDK
{
    using EnvTicketsRequestDTO = AdvancedTicketsRequestDTO;
    using L = Logger;

    public class EnvHelper : MonoBehaviour
    {
        public List<string> TicketIds;
        public Dictionary<string, EnvTicketsRequestDTO> TicketsData =
            new Dictionary<string, EnvTicketsRequestDTO>();
        public string MatchId;
        public string MatchProfile;
        public EnvEqualityDTO Equality;
        public EnvIntersectionDTO Intersection;

        public void Awake()
        {
            IDictionary envs = Environment.GetEnvironmentVariables();

            foreach (DictionaryEntry envEntry in envs)
            {
                string key = envEntry.Key.ToString();

                if (key.Contains("MM_"))
                {
                    try
                    {
                        if (key.Contains("_TICKET_") && !key.Contains("_IDS"))
                        {
                            string id = key.Split(
                                "MM_TICKET_",
                                StringSplitOptions.RemoveEmptyEntries
                            )[0];

                            TicketsData[id] = JsonConvert.DeserializeObject<EnvTicketsRequestDTO>(
                                envEntry.Value.ToString()
                            );
                        }
                        else if (key.Contains("_TICKET_"))
                        {
                            TicketIds = JsonConvert.DeserializeObject<List<string>>(
                                envEntry.Value.ToString()
                            );
                        }
                        else if (key.Contains("_MATCH_PROFILE"))
                        {
                            MatchProfile = envEntry.Value.ToString();
                        }
                        else if (key.Contains("_MATCH_ID"))
                        {
                            MatchId = envEntry.Value.ToString();
                        }
                        else if (key.Contains("_EQUALITY"))
                        {
                            Equality = JsonConvert.DeserializeObject<EnvEqualityDTO>(
                                envEntry.Value.ToString()
                            );
                        }
                        else if (key.Contains("_INTERSECTION"))
                        {
                            Intersection = JsonConvert.DeserializeObject<EnvIntersectionDTO>(
                                envEntry.Value.ToString()
                            );
                        }
                    }
                    catch (Exception e)
                    {
                        L._Error($"Couldn't parse env, consider updating Gen2 SDK. {e.Message}");
                        throw;
                    }
                }
            }
        }
    }
}
