using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TankMovement : EnemyMovement
{
    [SerializeField] LayerMask tank;
    
    public override void Update()
    {
        base.Update();
        
        if (Vector3.Distance(transform.position, wayPoints.position) <= 0.2f)
        {
            if (wayPointIndex >= waypoints.wayPointsLeft.Length -1|| wayPointIndex >= waypoints.wayPointsLeft.Length -1)
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
        
        if (Physics.Raycast(transform.position, transform.forward, rayLength, tank ))
        {
            speed = 0; 
            Debug.Log("Raycast hit detected. Stopping.");
        }
        else
        {
            speed = speedHolder;
        }
    }
    void GetNextPoint()
    {
        wayPointIndex++;
        if (left)
        {
            Debug.Log("Waypoints Left Length: " + waypoints.wayPointsLeft.Length);
            Debug.Log("Current Waypoint Index: " + wayPointIndex);
            
            if (wayPointIndex < waypoints.wayPointsLeft.Length)
            {
                wayPoints = waypoints.wayPointsLeft[wayPointIndex];
            }
            else
            {
                speed = 0;
                Debug.Log("Final waypoint reached.");
            }
        }
        else if (right)
        {
            Debug.Log("Waypoints Right Length: " + waypoints.wayPointsRight.Length);
            Debug.Log("Current Waypoint Index: " + wayPointIndex);
            
            if (wayPointIndex < waypoints.wayPointsRight.Length)
            {
                 wayPoints = waypoints.wayPointsRight[wayPointIndex];
            }
            else
            {
                speed = 0;
                Debug.Log("Final waypoint reached.");
            }
           
        }
    }
    
}
