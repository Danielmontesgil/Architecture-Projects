using System.Collections.Generic;
using UnityEngine;

namespace MissionsService
{
    public abstract class MissionServiceBase
    {
        protected List<IMission> allMissions = new List<IMission>();
        protected List<IMission> idleMissions = new List<IMission>();
        
        protected HashSet<IMission> activeMissions = new HashSet<IMission>();
        protected HashSet<IMission> inProgressMissions = new HashSet<IMission>();
        protected HashSet<IMission> completedMissions = new HashSet<IMission>();
        protected HashSet<IMission> canceledMissions = new HashSet<IMission>();
        
        public abstract void InitializeMissionsService(List<IMission> missions);
        public abstract void ActiveNewMissions();
        public abstract void AcceptMission(IMission mission);
        public abstract IEnumerable<IMission> GetActiveMissions();
        public abstract IEnumerable<IMission> GetCompletedMissions();
        public abstract IEnumerable<IMission> GetCanceledMissions();
        public abstract IMission GetMission(string missionId);
        public abstract void AddProgress(IMission mission, int progress);
        protected abstract bool IsMissionCompleted(IMission mission);
        public abstract void CancelMission(IMission mission);
    }
}