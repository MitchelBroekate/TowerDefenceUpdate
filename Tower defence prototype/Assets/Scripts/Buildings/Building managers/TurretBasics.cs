using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBasics : MonoBehaviour
{
    public GameObject grid; // the exact grid this gameObject is on can be used later for selling this building to reset the grid
    public GameObject radiusUI;
    
    public int upgradeCost;
    public int sellWorth;
    public bool lastUpgrade;
    
    public TowerUiData UIData;
    public GameObject towerUpgrade;
    
    public EnemyManager enemyManager;
    private Currency currency;
    private BuildingLevelUp buildingLevelUp;

    public float activationDelay;
    public AudioSource audioSource;
    [SerializeField] protected bool isActive = false;

    protected void Start()
    {
        currency = Currency.Instance;
        enemyManager = EnemyManager.Instance;
        buildingLevelUp = BuildingLevelUp.instance;
        Instantiate(audioSource, transform.position, Quaternion.identity);
        StartCoroutine(ActivateTurretAfterDelay());
    }
    
    IEnumerator ActivateTurretAfterDelay()
    {
        yield return new WaitForSeconds(activationDelay);

        isActive = true;
    }

    public void UpgradeTurret() 
    {
        if (!lastUpgrade && currency.currency >= upgradeCost && isActive)
        {
            currency.SubtractCurrency(upgradeCost);
            
            GameObject newTurret = Instantiate(towerUpgrade, transform.position, Quaternion.identity); 
            newTurret.GetComponent<TurretBasics>().grid = grid;
            buildingLevelUp.activeBuilding = newTurret;
            Destroy(gameObject);
        }
    }
  
    public void SellBuilding()
    {
        currency.AddCurrency(sellWorth);
        grid.SetActive(true);
        Destroy(gameObject);
    }

    public void ShowRadius()
    {
        radiusUI.SetActive(true);
    }

    public void HideRadius()
    {
        radiusUI.SetActive(false);
    }
}
