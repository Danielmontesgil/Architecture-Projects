using System;
using MissionsService;

[Serializable]
public class LevelRequirement: IMissionRequirement
{
    public int playerLevel;
    private int levelRequired;

    public LevelRequirement(int levelRequired)
    {
        this.levelRequired = levelRequired;
    }

    public bool RequirementAccomplished()
    {
        return playerLevel >= levelRequired;
    }
}
