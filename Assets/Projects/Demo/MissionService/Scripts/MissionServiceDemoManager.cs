using System.Collections.Generic;
using System.IO;
using MissionsService;
using UnityEngine;
using UnityEngine.UI;

public class MissionServiceDemoManager : MonoBehaviour
{
    public Button initializeServiceButton;
    private List<IMission> missions;
    
    void Start()
    {
        string filePath = Path.Combine(Application.dataPath, "C:/Projects/Architecture/Assets/Projects/Demo/MissionService/Data/missions.json");
        string jsonContent = File.ReadAllText(filePath);
        var missions = JsonUtility.FromJson<MissionsData>("{\"missions\":" + jsonContent + "}");
        initializeServiceButton.onClick.AddListener(()=> OnInitializeMissionService());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnInitializeMissionService()
    {
        
    }
}
