using System.Collections.Generic;
using UnityEngine;

namespace MissionsService.Impl
{
    public class MissionService : MissionServiceBase
    {
        public override void AddMissions()
        {
            throw new System.NotImplementedException();
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
            
            activeMissions.TryGetValue(mission, out var activeMission);
            if (activeMission == null)
            {
                return false;
            }
            activeMissions.Remove(activeMission);
            completedMissions.Add(activeMission);
            activeMission.State = MissionState.Complete;
            return true;
        }
        
        public override void CancelMission(IMission mission)
        {
            activeMissions.TryGetValue(mission, out var activeMission);
            if (activeMission == null)
            {
                return;
            }
            activeMissions.Remove(activeMission);
            canceledMissions.Add(activeMission);
            activeMission.State = MissionState.Cancelled;
        }
    }
}