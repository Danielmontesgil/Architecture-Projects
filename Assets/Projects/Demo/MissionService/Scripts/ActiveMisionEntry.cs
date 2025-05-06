using System;
using MissionsService;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActiveMisionEntry : MonoBehaviour
{
    public TextMeshProUGUI missionTitle;
    public Button acceptButton;
    public IMission mission;
    public void Init(IMission mission, Action<IMission, ActiveMisionEntry> onClick)
    {
        this.mission = mission;
        missionTitle.SetText(mission.Title);
        acceptButton.onClick.AddListener(() => { onClick(this.mission, this); });
    }

    public void ToggleState()
    {
        switch (mission.State)
        {
            case MissionState.Complete:
                acceptButton.interactable = true;
                acceptButton.image.color = Color.red;
                acceptButton.onClick.RemoveAllListeners();
                acceptButton.onClick.AddListener(ClaimReward);
                break;
            case MissionState.InProgress:
                acceptButton.interactable = false;
                acceptButton.image.color = Color.blue;
                break;
            case MissionState.Unknown:
                break;
            case MissionState.Idle:
                break;
            case MissionState.Active:
                break;
            case MissionState.Cancelled:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void ClaimReward()
    {
        
    }
}
