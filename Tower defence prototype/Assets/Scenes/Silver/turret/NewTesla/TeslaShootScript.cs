using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeslaShootScript : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float chainRadius = 10f;  // Radius for detecting enemies
    public int maxChains = 5;
    public Transform trailStartPoint;  // The starting point of the trail
    public Transform detectionPoint;   // The point for detecting enemies
    public int repeatCount = 5;
    public float delayBetweenChains = 2f;
    public float chainSpeed = 0.2f;
    public float trailVisibleDuration = 2f;  // Time the trail is visible
    public GameObject chainEffect;
    public LayerMask enemyLayer;  // LayerMask for detecting enemies

    private List<Transform> hitEnemies = new List<Transform>();
    private bool isChainRunning = false;



    void Update()
    {
        // Check if there are enemies within the detection radius from the detectionPoint
        Collider[] enemiesInRange = Physics.OverlapSphere(detectionPoint.position, chainRadius, enemyLayer);

        // If there are enemies in range and the chain is not already running, start the chain attack
        if (enemiesInRange.Length > 0 && !isChainRunning)
        {
            StartCoroutine(StartChainAttack());
        }
    }

    IEnumerator StartChainAttack()
    {
        isChainRunning = true;

        for (int repeat = 0; repeat < repeatCount; repeat++)
        {
            chainEffect.SetActive(true);

            // Set LineRenderer's starting position to trailStartPoint's position
            lineRenderer.positionCount = 1;
            lineRenderer.SetPosition(0, trailStartPoint.position);  // Start from trailStartPoint position

            yield return StartCoroutine(ChainToEnemies(trailStartPoint.position));

            yield return new WaitForSeconds(trailVisibleDuration);  // Wait until the trail is visible

            lineRenderer.positionCount = 0;

            chainEffect.SetActive(false);

            yield return new WaitForSeconds(delayBetweenChains);
        }

        isChainRunning = false;
    }

    IEnumerator ChainToEnemies(Vector3 startPos)
    {
        Vector3 currentPos = startPos;

        for (int i = 0; i < maxChains; i++)
        {
            Collider[] enemiesInRange = Physics.OverlapSphere(currentPos, chainRadius);

            Transform closestEnemy = null;
            float shortestDistance = Mathf.Infinity;

            foreach (Collider enemy in enemiesInRange)
            {
                // Check if the enemy is on the specified layer and has the "Enemy" tag
                if (enemy.CompareTag("Enemy") && ((1 << enemy.gameObject.layer) & enemyLayer) != 0)
                {
                    float distanceToEnemy = Vector3.Distance(currentPos, enemy.transform.position);

                    if (distanceToEnemy < shortestDistance && !hitEnemies.Contains(enemy.transform))
                    {
                        closestEnemy = enemy.transform;
                        shortestDistance = distanceToEnemy;
                    }
                }
            }

            if (closestEnemy == null)
            {
                break;
            }

            hitEnemies.Add(closestEnemy);

            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, closestEnemy.position);  // Set world position

            currentPos = closestEnemy.position;

            yield return new WaitForSeconds(chainSpeed);
        }

        hitEnemies.Clear();
    }

    // Visualize the detection radius with red lines in the Scene view
    private void OnDrawGizmosSelected()
    {
        // Draw a red sphere representing the detection radius around the detectionPoint
        if (detectionPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(detectionPoint.position, chainRadius);
        }

        // Draw lines to each enemy in the hitEnemies list
        if (hitEnemies != null && hitEnemies.Count > 0)
        {
            Gizmos.color = Color.red;
            foreach (Transform enemy in hitEnemies)
            {
                Gizmos.DrawLine(trailStartPoint.position, enemy.position);
            }
        }
    }
}
