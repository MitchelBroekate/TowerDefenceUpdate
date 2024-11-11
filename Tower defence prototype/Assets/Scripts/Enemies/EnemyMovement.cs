using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public float speedHolder;
    public float rayLength;
    public float rotationSpeed;
    
    public Transform wayPoints;
    public Waypoints waypoints;

    public int wayPointIndex = 0;

    public bool left;
    public bool right;

    public bool airShip;
    // Update is called once per frame
    public virtual  void Start()
    {
        speedHolder = speed; 
        waypoints = WayPointManager.instance.waypoints;
        if (left)
        {
            if (airShip)
            {
                wayPoints = waypoints.airWayPointsLeft[0];
            }
            else
            {
                wayPoints = waypoints.wayPointsLeft[0];
            }
            
        }
        else if (right)
        {
            if (airShip)
            {
                wayPoints = waypoints.airWayPointsRight[0];
            }
            else
            {
                wayPoints = waypoints.wayPointsRight[0];
            }
        }
        
    }

    public virtual void Update()
    {
        Vector3 direction = (wayPoints.position - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, wayPoints.position, speed * Time.deltaTime);
        
        Quaternion targetRotation = Quaternion.LookRotation(direction); 

        
        if (targetRotation != Quaternion.identity)
        {
            Quaternion smoothRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        
            
            smoothRotation = Quaternion.Euler(0, smoothRotation.eulerAngles.y, 0);
            
            transform.rotation = smoothRotation;
        }
        
        //oude manier van rotating 
        
        /*Vector3 direction = (wayPoints.position - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, wayPoints.position, speed * Time.deltaTime);
        // Rotate smoothly towards the target waypoint

        Quaternion targetRotation = Quaternion.LookRotation(direction); 
        if (targetRotation == quaternion.identity) 
        { 
            transform.rotation = transform.rotation;
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }*/
    }
}
