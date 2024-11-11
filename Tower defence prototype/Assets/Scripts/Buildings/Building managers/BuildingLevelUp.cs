using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingLevelUp : MonoBehaviour
{
    public static BuildingLevelUp instance;
    public BuildingSelect buildingSelect;
    public TMP_Text nameText;
    public TMP_Text descText;
    public TMP_Text upgradeCostText;
    public Image buildingPortrait;
    public GameObject activeBuilding;
    public GameObject upgradeButton;

    void Awake()
    {
        instance = this;
    }
    
    public void TurretUpgradeButton()
    {
        activeBuilding.GetComponent<TurretBasics>().UpgradeTurret();
        ChangeTurretInfo();
        ShowRadius();
    }

    public void SellBuildingButton()
    {
        activeBuilding.GetComponent<TurretBasics>().SellBuilding();
        buildingSelect.SetWindowShop();
    }

    public void ChangeTurretInfo()
    {
        if (activeBuilding.GetComponent<TurretBasics>().lastUpgrade)
        {
            // remove upgrade button
        }
        buildingPortrait.sprite = activeBuilding.GetComponent<TurretBasics>().UIData.icon;
        nameText.text = activeBuilding.GetComponent<TurretBasics>().UIData.buildingName;
        descText.text = activeBuilding.GetComponent<TurretBasics>().UIData.descriptionText;
        upgradeCostText.text = activeBuilding.GetComponent<TurretBasics>().UIData.upgradeCostText;
    } 
    public void ShowRadius() 
    { 
        activeBuilding.GetComponent<TurretBasics>().ShowRadius();
    }

    public void HideRadius()
    {
        if (activeBuilding != null)
        {
            activeBuilding.GetComponent<TurretBasics>().HideRadius();
        }
    }
}
