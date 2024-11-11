using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilRig : TurretBasics
{
    public float harvestAmount;
    void Update()
    {
        if (enemyManager.gameStarted && isActive)
        {
            Currency.Instance.AddCurrency(harvestAmount * Time.deltaTime);
        }
    }
}
