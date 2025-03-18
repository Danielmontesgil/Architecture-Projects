using System;
using System.Collections.Generic;
using MissionsService;
using UnityEngine;

[Serializable]
public class Mission: IMission
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

[Serializable]
public class MissionsData
{
    public List<Mission> Missions { get; set; }
}
