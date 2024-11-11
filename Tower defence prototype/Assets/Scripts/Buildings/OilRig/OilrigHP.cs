using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class OilrigHP : BuildingHp
{
    public override void DIE()
    {
        gameObject.GetComponent<OilRig>().grid.SetActive(true);
        
        Destroy(gameObject);
    }
    
    public override void TakeDamage(float damage)
    {
        currentHealth -= damage;
        
        healthSlider.value = currentHealth;
        print("The Base took" + damage + " damage");
        if (currentHealth <= 0)
        {
            Instantiate(audioSource, transform.position, Quaternion.identity);
            DIE();
        }
    }

    private void OnDestroy()
    {
        buildingList.UnregisterBuilding(transform);

        if (buildingLevelUp.activeBuilding == gameObject)
        {
            buildingLevelUp.activeBuilding = null;
            buildingLevelUp.buildingSelect.SetWindowShop();
        }
    }
}
