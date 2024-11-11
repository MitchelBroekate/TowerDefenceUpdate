using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Timers;
using UnityEditor;
using UnityEngine;

public class Tesla : TowerBehaviour
{
 
    public int maxBounces;
    public int damage;
    public int chainRange;
    public float attackWaitTime;
    public LayerMask enemyLayerMask;

  
    public LineRenderer lineRenderer;
    public GameObject chainEffect;
    public Transform trailStartPoint; 
    public Transform detectionPoint; 
    public float chainSpeed = 0.2f;
    public float trailVisibleDuration = 2f;

    private List<Transform> hitEnemies = new List<Transform>();
    private bool isChainRunning = false;
    private float timer;
    
    public AudioSource teslaSFX;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= attackWaitTime && !isChainRunning)
        {
            Collider[] enemiesInRange = Physics.OverlapSphere(detectionPoint.position, chainRange, enemyLayerMask);

            if (enemiesInRange.Length > 0)
            {
                timer = 0;
                StartCoroutine(StartChainAttack());
            }
        }
    }

    private IEnumerator StartChainAttack()
    {
        isChainRunning = true;
        hitEnemies.Clear();
        chainEffect.SetActive(true);

        // Set LineRenderer's starting position
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, trailStartPoint.position);

        yield return ChainToEnemies(trailStartPoint.position, maxBounces);

        yield return new WaitForSeconds(trailVisibleDuration);
        
        lineRenderer.positionCount = 0;
        chainEffect.SetActive(false);

        isChainRunning = false;
    }

    private IEnumerator ChainToEnemies(Vector3 startPos, int bouncesLeft)
    {
        Vector3 currentPos = startPos;

        for (int i = 0; i < bouncesLeft; i++)
        {
            Collider[] enemiesInRange = Physics.OverlapSphere(currentPos, chainRange, enemyLayerMask);
            Transform closestEnemy = FindClosestEnemy(enemiesInRange, currentPos);

            if (closestEnemy == null)
                break;
            
            EnemyHealth enemyScript = closestEnemy.GetComponent<EnemyHealth>();
            if (enemyScript != null)
            {
                enemyScript.TakeDamage(damage);
                Instantiate(teslaSFX, trailStartPoint.position, Quaternion.identity);
            }

            // Record hit position and update line renderer
            hitEnemies.Add(closestEnemy);
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, closestEnemy.position);
            currentPos = closestEnemy.position;

            yield return new WaitForSeconds(chainSpeed);
        }
    }

    private Transform FindClosestEnemy(Collider[] enemies, Vector3 position)
    {
        Transform closestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Collider enemy in enemies)
        {
            if (enemy.CompareTag("Enemy") && !hitEnemies.Contains(enemy.transform))
            {
                float distanceToEnemy = Vector3.Distance(position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    closestEnemy = enemy.transform;
                    shortestDistance = distanceToEnemy;
                }
            }
        }
        return closestEnemy;
    }

    private void OnDrawGizmosSelected()
    {
        if (detectionPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(detectionPoint.position, chainRange);
        }
    }
}
