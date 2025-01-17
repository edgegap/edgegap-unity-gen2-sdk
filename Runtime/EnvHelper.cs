using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Edgegap.Gen2SDK
{
    using L = Logger;

    public class EnvHelper : MonoBehaviour
    {
        public static EnvHelper Instance { get; private set; }

        public List<string> TicketIds { get; private set; }
        public Dictionary<string, EnvCustomTicketsRequestDTO> TicketsData { get; private set; }
        public string MatchId { get; private set; }
        public string MatchProfile { get; private set; }
        public EnvEqualityDTO Equality { get; private set; }
        public EnvIntersectionDTO Intersection { get; private set; }

        public void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        public void Start()
        {
            IDictionary envs = Environment.GetEnvironmentVariables();
            MatchProfile = envs["MM_MATCH_PROFILE"].ToString();
            TicketsData = new Dictionary<string, EnvCustomTicketsRequestDTO>();

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

                            TicketsData[id] =
                                JsonConvert.DeserializeObject<EnvCustomTicketsRequestDTO>(
                                    envEntry.Value.ToString()
                                );
                        }
                        else if (key.Contains("_TICKET_"))
                        {
                            TicketIds = JsonConvert.DeserializeObject<List<string>>(
                                envEntry.Value.ToString()
                            );
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
