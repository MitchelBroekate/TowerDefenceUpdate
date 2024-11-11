using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class BuildingSelect : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private BuildingPlacement buildingPlacement;

    [SerializeField] private LayerMask tower;

    public GameObject levelUpUIPanel;
    public GameObject shopUIPanel;
    
    public BuildingLevelUp buildingLevelUp;
    private GameObject _activeTower;
    
    // Update is called once per frame
    void Update()
    {
        Inputs();
    }

    private void Inputs()
    {
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUI() && !buildingPlacement.isPlacingTower )
        {
            if (buildingLevelUp.activeBuilding != null)
            {
                buildingLevelUp.HideRadius();
            }
            
            buildingLevelUp.activeBuilding = ActiveTower();

            if (buildingLevelUp.activeBuilding != null)
            {
               SetWindowUpgrade();
            }
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            if (buildingLevelUp.activeBuilding != null)
            {
                buildingLevelUp.HideRadius();
                
                SetWindowShop();
                
                buildingLevelUp.activeBuilding = null;
            }
        }
    }

    private GameObject ActiveTower()
    {
        Ray camRay = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(camRay, out RaycastHit hitInfo,300f ,tower))
        {
            _activeTower = hitInfo.collider.gameObject;
            return _activeTower;
        }
        buildingLevelUp.HideRadius();
        SetWindowShop();
        return null;
    }
    
    private bool IsPointerOverUI() // check if mouse is over UI
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    public void SetWindowShop()
    {
        buildingLevelUp.activeBuilding = null;
        levelUpUIPanel.SetActive(false);
        shopUIPanel.SetActive(true);
    }

    public void SetWindowUpgrade()
    {
        buildingLevelUp.ChangeTurretInfo();
        levelUpUIPanel.SetActive(true); 
        shopUIPanel.SetActive(false);
        
        buildingLevelUp.ShowRadius();
    }
    
}
