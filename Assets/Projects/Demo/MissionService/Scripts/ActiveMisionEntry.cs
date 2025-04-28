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
}
