using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace Edgegap.Gen2SDK
{
    using L = Logger;

    public class ServerAgent<T, A>
        where T : TicketsRequestDTO<A>
    {
        private Api<T, A> Gen2Api;

        public MonoBehaviour Handler;

        #region ServerAgent Configuration
        // BaseUrl may only be set with constructor
        public string BaseUrl { get; }
        public string AuthToken { private get; set; }
        public int TargetPlayerCount;

        public string AgentVersion;

        public int RequestTimeoutSeconds;
        public float PollingBackoffSeconds;
        public int MaxConsecutivePollingErrors;

        public float RemoveTicketSeconds;
        public bool DeleteBackfillsOnQuit;

        public bool LogBackfillUpdates;
        public bool LogAssignmentUpdates;
        #endregion

        #region ServerAgent State
        public string MatchProfile { get; private set; }
        public string MatchId { get; private set; }
        public Dictionary<string, string> EqualityRuleValues { get; private set; }
        public Dictionary<string, List<string>> IntersectionRuleValues { get; private set; }
        public string[] TicketIDs { get; private set; }
        public Dictionary<string, TicketsResponseDTO> Tickets { get; private set; }
        #endregion

        public Observable<BackfillsResponseDTO> Assignment { get; private set; } =
            new Observable<BackfillsResponseDTO> { };
        public Observable<MonitorResponseDTO> Monitor { get; private set; } =
            new Observable<MonitorResponseDTO> { };
        public Observable<BackfillsRequestDTO<T>> Backfill { get; private set; } =
            new Observable<BackfillsRequestDTO<T>> { };
        private protected bool Polling = false;

        public ServerAgent(
            MonoBehaviour handler,
            string baseUrl,
            string authToken,
            int targetPlayerCount = -1,
            string agentVersion = "1.0.0",
            int requestTimeoutSeconds = 3,
            float pollingBackoffSeconds = 1f,
            int maxConsecutivePollingErrors = 10,
            float removeTicketSeconds = 30f,
            bool deleteBackfillsOnQuit = true,
            bool logBackfillUpdates = true,
            bool logAssignmentUpdates = true
        )
        {
            if (handler is null)
            {
                throw new Exception("Gen2Server Handler not assigned.");
            }

            Handler = handler;
            BaseUrl = baseUrl;
            AuthToken = authToken;
            TargetPlayerCount = targetPlayerCount;
            AgentVersion = agentVersion;
            RequestTimeoutSeconds = requestTimeoutSeconds;
            PollingBackoffSeconds = pollingBackoffSeconds;
            MaxConsecutivePollingErrors = maxConsecutivePollingErrors;
            RemoveTicketSeconds = removeTicketSeconds;
            DeleteBackfillsOnQuit = deleteBackfillsOnQuit;
            LogBackfillUpdates = logBackfillUpdates;
            LogAssignmentUpdates = logAssignmentUpdates;
        }

        #region Server API
        public void Status()
        {
            Gen2Api.GetMonitor(
                (MonitorResponseDTO monitor, UnityWebRequest request) =>
                {
                    if (monitor.Status == "HEALTHY")
                    {
                        Monitor._Update(monitor, "healthy");
                    }
                    else
                    {
                        Monitor._Update(monitor, "unhealthy");
                    }
                },
                (string error, UnityWebRequest request) =>
                {
                    L._Error(error);
                    Monitor._Update(null, "error");
                }
            );
        }

        public void VerifyTicket(string ticketId)
        {
            // @todo get backfill
        }

        public void RemoveTicket(bool replaceWithBackfill = true)
        {
            // @todo check if the ticket was verified
            // @todo remove ticket and create backfill
        }

        public void AddBackfill()
        {
            // @todo create backfill
        }

        public void RemoveBackfill()
        {
            // @todo delete backfill
        }

        public void RemoveAllBackfills()
        {
            Polling = false;
            // @todo foreach ticket remove backfill in parallel
        }
        #endregion

        #region Initialization
        public void Initialize(
            UnityAction<
                Observable<MonitorResponseDTO>,
                ObservableActionType,
                string
            > onMonitorUpdate,
            UnityAction<
                Observable<BackfillsResponseDTO>,
                ObservableActionType,
                string
            > onAssignmentUpdate,
            UnityAction<
                Observable<BackfillsRequestDTO<T>>,
                ObservableActionType,
                string
            > onBackfillUpdate = null
        )
        {
            if (string.IsNullOrEmpty(BaseUrl.Trim()))
            {
                throw new Exception("BaseUrl not declared.");
            }

            if (string.IsNullOrEmpty(AuthToken.Trim()))
            {
                throw new Exception("AuthToken not declared.");
            }

            _LoadTicketsFromEnv();

            Gen2Api = new Api<T, A>(Handler, AuthToken, BaseUrl);

            Gen2Api.GetMonitor(
                (MonitorResponseDTO monitor, UnityWebRequest request) =>
                {
                    _SubscribeLogger(Monitor, "Monitor");
                    _SubscribeLogger(Backfill, "Backfill", LogBackfillUpdates);
                    _SubscribeLogger(Assignment, "Assignment", LogAssignmentUpdates);

                    if (onBackfillUpdate is not null)
                    {
                        Backfill.Subscribe(onBackfillUpdate);
                    }
                    Assignment.Subscribe(onAssignmentUpdate);
                    Monitor.Subscribe(onMonitorUpdate);

                    if (monitor.Status == "HEALTHY")
                    {
                        Monitor._Update(monitor, "healthy");
                    }
                    else
                    {
                        Monitor._Update(monitor, "unhealthy");
                    }
                },
                (string error, UnityWebRequest request) =>
                {
                    L._Error(error);
                    Monitor._Update(null, "error");
                }
            );
        }

        internal void _LoadTicketsFromEnv()
        {
            //@todo read envs
        }

        internal void _SubscribeLogger<O>(
            Observable<O> observable,
            string name,
            bool enabled = true
        )
        {
            observable.Subscribe(
                (Observable<O> obs, ObservableActionType type, string message) =>
                {
                    if (!enabled)
                        return;

                    if (type == ObservableActionType.Update)
                    {
                        L._Log(L._FormatUpdateMessage(name, message, obs.Previous, obs.Current));
                    }
                    else
                    {
                        string log = L._FormatNotifyMessage(name, message, obs.Current);
                        if (type == ObservableActionType.Log)
                        {
                            L._Log(log);
                        }
                        else if (type == ObservableActionType.Warn)
                        {
                            L._Warn(log);
                        }
                        else
                        {
                            L._Error(log);
                        }
                    }
                }
            );
        }
        #endregion
    }
}
