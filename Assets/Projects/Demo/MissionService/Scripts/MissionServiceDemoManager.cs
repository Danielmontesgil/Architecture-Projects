using System;
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

    #region UI

    public Transform activeMissionsParent;
    public Transform inProgressMissionsParent;
    public GameObject activeMissionPrefab;
    private List<ActiveMisionEntry> missionsEntry = new List<ActiveMisionEntry>();

    #endregion
    
    void Start()
    {
        initializeServiceButton.onClick.AddListener(OnInitializeMissionService);
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.A))
        {
            return;
        }
        var currentMission = missionsEntry[0];
        missionService.AddProgress(currentMission.mission, 1);
        if (currentMission.mission.State == MissionState.Complete)
        {
            currentMission.ToggleState();
        }
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
        missionService.ActiveNewMissions();

        foreach (IMission mission in missionService.GetActiveMissions())
        {
            if (missionsEntry.Count > 0)
            {
                bool missionFound = false;
                foreach (var missionEntry in missionsEntry)
                {
                    if (mission == missionEntry.mission)
                    {
                        missionFound = true;
                        break;
                    }
                }

                if (!missionFound)
                {
                    CreateMissionEntry(mission);
                }
                continue;
            }

            CreateMissionEntry(mission);
        }
    }

    private void CreateMissionEntry(IMission mission)
    {
        ActiveMisionEntry newMissionEntry = Instantiate(activeMissionPrefab, activeMissionsParent).GetComponent<ActiveMisionEntry>();
        newMissionEntry.Init(mission, AcceptMission);
        missionsEntry.Add(newMissionEntry);
    }

    private void AcceptMission(IMission mission, ActiveMisionEntry entry)
    {
        missionService.AcceptMission(mission);
        entry.gameObject.transform.SetParent(inProgressMissionsParent);
        entry.ToggleState();
    }
}
