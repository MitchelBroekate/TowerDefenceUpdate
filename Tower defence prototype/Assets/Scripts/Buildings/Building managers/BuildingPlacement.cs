using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour
{
    [SerializeField] private GameObject buildingLevelUpPrefab; 
    [SerializeField] private Camera playerCamera; 
    [SerializeField] private LayerMask grid; 
    [SerializeField] private LayerMask oilGrid;
    [SerializeField] private float yOffset;

    public GameObject currentPlacingTower;
    public GameObject buildingLevelUp;
    
    private int buildingCost;
    
    
    private TowerData currentTowerData;
    private GameObject currentGrid;
    
    public bool isPlacingTower; // bool used by buildingSelect
    public GameObject grids;
    // Update is called once per frame
    void Update()
    {
        if(currentPlacingTower != null)
        {
            Ray camray = playerCamera.ScreenPointToRay(Input.mousePosition);
            
            // set false so it wont immediatly open lvl up ui
            // buildingLevelUp.SetActive(false); 
            
            if (currentTowerData.towerType == TowerData.TowerType.OilRig)
            {
                if (Physics.Raycast(camray, out RaycastHit hitInfo, 500f, oilGrid))
                {
                    currentPlacingTower.transform.position = hitInfo.collider.gameObject.transform.position + new Vector3(0, yOffset, 0);
                    currentPlacingTower.SetActive(true);
                    
                    if(Input.GetMouseButtonDown(0))
                    {   
                        currentGrid = hitInfo.collider.gameObject; // give the tower a reference to the grid its on so you can sell the tower later and bring back the tile
                        PlaceTower();
                    }
                }
                else
                {
                    if(Input.GetMouseButtonDown(0))
                    {   
                        DeselectTower();
                        isPlacingTower = false;
                        grids.SetActive(false);
                    }
                }
                
            }
            else if (currentTowerData.towerType == TowerData.TowerType.Tower)
            {  
                if(Physics.Raycast(camray, out RaycastHit hitInfo, 500f, grid))
                {
                    currentPlacingTower.transform.position = hitInfo.collider.gameObject.transform.position + new Vector3(0, yOffset, 0);
                    currentPlacingTower.SetActive(true);
                    
                    if(Input.GetMouseButtonDown(0))
                    {  
                        currentGrid = hitInfo.collider.gameObject;  // give the tower a reference to the grid its on so you can sell the tower later and bring back the tile
                        PlaceTower();
                    }
                }
                else
                {
                    if(Input.GetMouseButtonDown(0)) 
                    {   
                        DeselectTower();
                        isPlacingTower = false;
                        grids.SetActive(false);
                    }
                }
            }
            
            if(Input.GetMouseButtonDown(1))
            {
                Destroy(currentPlacingTower);
                StartCoroutine(SmallBoolDelay(0.1f));
                // buildingLevelUp.SetActive(true);
            }   
        }
    }

    public void SetTowerToPlace(TowerData towerData)
    {
        currentPlacingTower = Instantiate(towerData.towerBlueprintPrefab, Vector3.zero, quaternion.identity);
        currentPlacingTower.SetActive(false);
        currentTowerData = towerData;
        buildingCost = towerData.cost;

        isPlacingTower = true;
        grids.SetActive(true);
    }
    #region Tower Selection
    void DeselectTower()
    {
        Destroy(currentPlacingTower);
        currentPlacingTower = null;
        // buildingLevelUp.SetActive(true);
    }

    void PlaceTower()
    {
        // if you dont have enough money dont place the tower
        if (Currency.Instance.currency >= buildingCost) 
        { 
             Currency.Instance.SubtractCurrency(buildingCost);
            
             // place tower on cursor and clear currentplacingtower gameOBject
             GameObject placingTower = Instantiate(currentTowerData.towerPrefab, currentPlacingTower.transform.position, quaternion.identity);
             placingTower.GetComponent<TurretBasics>().grid = currentGrid;
            
             currentGrid.SetActive(false);
             currentGrid = null;
             Destroy(currentPlacingTower);
             
             currentPlacingTower = null;
             // buildingLevelUp.SetActive(true);
             StartCoroutine(SmallBoolDelay(0.1f));
        }
        else
        {
            print ("You do not have enough money to place this tower.");
            
            Destroy(currentPlacingTower);
            StartCoroutine(SmallBoolDelay(0.1f));
            currentPlacingTower = null;
        }
    }
    #endregion
    
    private IEnumerator SmallBoolDelay(float delay) //small delay for the bool because it changes too fast building select script is hurting from it 
    {
        grids.SetActive(false);
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        isPlacingTower = false; 
    }
}
