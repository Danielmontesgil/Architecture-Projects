using System;
using MissionsService;
using Random = UnityEngine.Random;

[Serializable]
public class LevelRequirement: IMissionRequirement
{
   
    public string Type;
    public int LevelRequired;

    public LevelRequirement(int levelRequired)
    {
        this.LevelRequired = levelRequired;
    }
    
    public bool RequirementAccomplished()
    {
        //return Random.Range(0, 1) > 0.5;
        return true;
    }
}
