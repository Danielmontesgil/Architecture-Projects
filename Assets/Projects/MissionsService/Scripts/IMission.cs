using System.Collections.Generic;
using UnityEngine;

namespace MissionsService
{
    public enum MissionState
    {
        Unknown = -1,
        Idle = 0,
        Active = 1,
        InProgress = 2,
        Complete = 3,
        Cancelled = 4
    }
    
    public interface IMission
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public MissionState State { get; set; }
        public List<IRewardable> Reward { get; set; }
        public int QuantityToComplete { get; set; }
        public int Progress { get; set; }
        public List<IMissionRequirement> Requirements { get; set; }
    }
}