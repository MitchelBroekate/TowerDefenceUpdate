using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingList : MonoBehaviour
{
    public static BuildingList Instance;
    private List<Transform> activeBuildings = new List<Transform>();
    public TutorialTyper tutorialTyper;
    private void Awake()
    {
        Instance = this;
    }

    public void RegisterBuilding(Transform building)
    {
        activeBuildings.Add(building);
        if (activeBuildings.Count == 2 && tutorialTyper != null)
        {
            tutorialTyper.continuePlacedTurret = true;
        }
    }

    public void UnregisterBuilding(Transform building)
    {
        activeBuildings.Remove(building);
    }

    public List<Transform> GetActiveBuildings()
    {
        return activeBuildings;
    }
}
