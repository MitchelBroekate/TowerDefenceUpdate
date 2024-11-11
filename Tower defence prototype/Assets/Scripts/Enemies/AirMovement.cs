using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirMovement : EnemyMovement
{
    [SerializeField] private LayerMask airCraft;
    public float flyTilt;
    public override void Update()
    {
        base.Update();
        
        if (Vector3.Distance(transform.position, wayPoints.position) <= 0.2f)
        {
            if (wayPointIndex >= waypoints.airWayPointsLeft.Length  -1 || wayPointIndex >= waypoints.airWayPointsRight.Length -1 )
            {
                transform.position = transform.position;
                speed = 0;
                Debug.Log("Final waypoint reached.");
            }
            else
            {
                GetNextPoint();
            }
        }
        
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, rayLength, airCraft ))
        {
            speed = 0; 
            Debug.Log("Raycast hit detected. Stopping.");
        }
        else
        {
            speed = speedHolder;
        }

        if (speed == speedHolder)
        {
            
        }
    }
    
    public void GetNextPoint()
    {
        wayPointIndex++;
        if (left)
        {
            wayPoints = waypoints.airWayPointsLeft[wayPointIndex];
        }
        else if (right)
        {
            wayPoints = waypoints.airWayPointsRight[wayPointIndex];
        }
    }
   
}
