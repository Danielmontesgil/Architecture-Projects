using System;
using System.Collections.Generic;
using UnityEngine;

namespace MissionsService.Impl
{
    public class MissionService : MissionServiceBase
    {
        public override void InitializeMissionsService(List<IMission> missions)
        {
            allMissions.AddRange(missions);
            
            foreach (var mission in missions)
            {
                switch (mission.State)
                {
                    case MissionState.Active:
                        activeMissions.Add(mission);
                        break;
                    case MissionState.Idle:
                        idleMissions.Add(mission);
                        break;
                    case MissionState.InProgress:
                        inProgressMissions.Add(mission);
                        break;
                    case MissionState.Complete:
                        completedMissions.Add(mission);
                        break;
                    case MissionState.Cancelled:
                        canceledMissions.Add(mission);
                        break;
                    case MissionState.Unknown:
                        Debug.LogError($"Mission with Id {mission.Id} is in state Unknown");
                        break;
                }
            }
        }

        public override void ActiveNewMissions()
        {
            foreach (var mission in idleMissions)
            {
                var shouldActiveMission = true;
                foreach (var requirement in mission.Requirements)
                {
                    if (requirement.RequirementAccomplished())
                    {
                        continue;
                    }
                    shouldActiveMission = false;
                }

                if (shouldActiveMission)
                {
                    activeMissions.Add(mission);
                    mission.State = MissionState.Active;
                }
            }

            foreach (var mission in activeMissions)
            {
                if (idleMissions.Contains(mission))
                {
                    idleMissions.Remove(mission);
                }
            }
        }

        public override void AcceptMission(IMission mission)
        {
            if (inProgressMissions.Contains(mission) || !activeMissions.Contains(mission))
            {
                return;
            }

            mission.State = MissionState.InProgress;

            activeMissions.Remove(mission);
            inProgressMissions.Add(mission);
        }

        public override IEnumerable<IMission> GetActiveMissions()
        {
            return activeMissions;
        }

        public override IEnumerable<IMission> GetCompletedMissions()
        {
            return completedMissions;
        }

        public override IEnumerable<IMission> GetCanceledMissions()
        {
            return canceledMissions;
        }

        public override IMission GetMission(string missionId)
        {
            foreach (var mission in allMissions)
            {
                if (mission.Id == missionId)
                {
                    return mission;
                }
            }

            return null;
        }
        
        public override void AddProgress(IMission mission, int progress)
        {
            mission.Progress += progress;
            IsMissionCompleted(mission);
        }

        protected override bool IsMissionCompleted(IMission mission)
        {
            if (mission.Progress < mission.QuantityToComplete)
            {
                return false;
            }
            
            inProgressMissions.TryGetValue(mission, out var activeMission);
            if (activeMission == null)
            {
                return false;
            }
            inProgressMissions.Remove(activeMission);
            completedMissions.Add(activeMission);
            activeMission.State = MissionState.Complete;
            return true;
        }
        
        public override void CancelMission(IMission mission)
        {
            inProgressMissions.TryGetValue(mission, out var activeMission);
            if (activeMission == null)
            {
                return;
            }
            inProgressMissions.Remove(activeMission);
            canceledMissions.Add(activeMission);
            activeMission.State = MissionState.Cancelled;
        }
    }
}