using System;
using MissionsService;

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
        return true;
    }
}
