using Edgegap.Gen2SDK;
using UnityEngine;
using MyTicketsAttributes = Edgegap.Gen2SDK.LatenciesAttributesDTO;
using MyTicketsRequestDTO = Edgegap.Gen2SDK.SimpleTicketsRequestDTO;

// usage of Server Handler is optional, only required if you use Backfill
// todo replace SimpleTicketsRequestDTO with CustomTicketsRequestDTO
// todo replace LatenciesAttributesDTO with CustomTicketsAttributes
public class Gen2ServerHandlerExample : MonoBehaviour
{
    public static Gen2ServerHandlerExample Instance { get; private set; }

    #region Gen2Server Configuration
    public string BaseUrl;
    public string AuthToken;
    public int TargetPlayerCount = -1;

    public string AgentVersion = "1.0.0";

    public int RequestTimeoutSeconds = 3;
    public float PollingBackoffSeconds = 1f;
    public int MaxConsecutivePollingErrors = 10;

    public float RemoveAssignmentSeconds = 30f;
    public bool DeleteBackfillsOnQuit = true;

    public bool LogBackfillUpdates = true;
    public bool LogAssignmentUpdates = true;
    public bool LogPollingUpdates = false;
    #endregion

    public ServerAgent<MyTicketsRequestDTO, MyTicketsAttributes> Gen2Server;

    public void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

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
        // configure Gen2
        Gen2Server = new ServerAgent<MyTicketsRequestDTO, MyTicketsAttributes>(
            this,
            BaseUrl,
            AuthToken,
            TargetPlayerCount,
            AgentVersion,
            RequestTimeoutSeconds,
            PollingBackoffSeconds,
            MaxConsecutivePollingErrors,
            RemoveAssignmentSeconds,
            LogBackfillUpdates,
            LogAssignmentUpdates,
            LogPollingUpdates
        );

        // @todo Gen2Server.Initialize();
    }

    public void Update()
    {
        // @todo check server TargetPlayerCount and TicketIDs.Count and manage backfills
    }

    public void OnApplicationQuit()
    {
        if (!DeleteBackfillsOnQuit)
            return;
        StopBackfill();
    }

    public void ResumeBackfill()
    {
        // @todo check target > 0 && current < target, then create backfills
    }

    public void StopBackfill()
    {
        Gen2Server.RemoveAllBackfills();
    }
}
