using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingHp : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    
    protected BuildingList buildingList;
    protected BuildingLevelUp buildingLevelUp;
    
    public EnemyList enemyList;
    public EnemyManager enemyManager;
    public AudioSource audioSource;
    [SerializeField] public Slider healthSlider;
    protected void Start()
    {
        currentHealth = maxHealth;
        
        buildingList = BuildingList.Instance;
        buildingLevelUp = BuildingLevelUp.instance;
        
        buildingList.RegisterBuilding(transform);
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        
        print("The Base took" + damage + " damage");
        if (currentHealth <= 0)
        {
            DIE();
        }
    }

    virtual public void DIE()
    {
        
    }
}
