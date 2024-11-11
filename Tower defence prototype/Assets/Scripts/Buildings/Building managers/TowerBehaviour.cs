using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class TowerBehaviour : TurretBasics
{
    public float radius;
    
    protected Transform enemy;

    [SerializeField] private int flyingEnemyLayer = 13;

    public bool canTargetFlyingEnemies;
    public Transform FindEnemy()
    {
        List<Transform> allEnemies = EnemyList.Instance.GetActiveEnemies(); 

        Transform closestTarget = null;
        float closestDistance = radius;

        foreach (Transform enemyTransform in allEnemies)
        {
            bool isFlyingEnemy = IsFlyingEnemy(enemyTransform);

            // Skip flying enemies if you cant target it
            if (isFlyingEnemy && !canTargetFlyingEnemies)
            {
                continue;
            }


            float distanceToEnemy = IsFlyingEnemy(enemyTransform) ? GetHorizontalDistance(enemyTransform) : Vector3.Distance(transform.position, enemyTransform.position);
            
            if (distanceToEnemy < closestDistance)
            {
                closestTarget = enemyTransform;
                closestDistance = distanceToEnemy;
            }
        }

        return closestTarget;
    }
    
    private bool IsFlyingEnemy(Transform enemyTransform)
    {
        return enemyTransform.gameObject.layer == flyingEnemyLayer; 
    }

    // Get horizontal distance by ignoring height 
    private float GetHorizontalDistance(Transform enemyTransform)
    {
        Vector3 towerPosition = transform.position;
        Vector3 enemyPosition = enemyTransform.position;
        
        towerPosition.y = 0;
        enemyPosition.y = 0;

        return Vector3.Distance(towerPosition, enemyPosition);
    }
}
