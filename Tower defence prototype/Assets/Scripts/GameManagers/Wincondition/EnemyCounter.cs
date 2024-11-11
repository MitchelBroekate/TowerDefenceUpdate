using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    public int amountOfEnemies;
    public int enemiesKilled;
    
    public GameObject winScreen;

    public void UpdateEnemiesKilled()
    {
        enemiesKilled++;
        
        if (enemiesKilled == amountOfEnemies)
        {
            winScreen.SetActive(true);
            Time.timeScale = 1; 
        }
    }
}
