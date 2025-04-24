using System.Collections.Generic;
using System.IO;
using MissionsService;
using MissionsService.Impl;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class MissionServiceDemoManager : MonoBehaviour
{
    public Button initializeServiceButton;
    public Button activateMissionButton;
    
    private MissionsData missions;
    private MissionService missionService;
    
    void Start()
    {
        initializeServiceButton.onClick.AddListener(OnInitializeMissionService);
    }
    
    public void OnInitializeMissionService()
    {
        string filePath = Path.Combine(Application.dataPath, "Projects", "Demo", "MissionService", "Data", "missions.json");
        string jsonContent = File.ReadAllText(filePath);
        missions = JsonConvert.DeserializeObject<MissionsData>(jsonContent);

        missionService = new MissionService();
        
        var missionInterfaces = new List<IMission>();
        foreach (var mission in missions.Missions) {
            missionInterfaces.Add(mission);
        }
        
        missionService.InitializeMissionsService(missionInterfaces);
        
        // Active the complete demo
        activateMissionButton.interactable = true;
        activateMissionButton.onClick.AddListener(OnActivateMission);
    }

    public void OnActivateMission()
    {
        
    }
}
