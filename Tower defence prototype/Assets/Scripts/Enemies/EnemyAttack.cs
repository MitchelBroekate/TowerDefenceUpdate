using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingAttack : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private int oilRigLayer = 10;
    
    [SerializeField] private Transform laserTower;
    [SerializeField] private Transform rotatingGunHolder;
    [SerializeField] private Transform guns;
    [SerializeField] private Transform gunLeftShootPoint, gunRightShootPoint;
    
    [SerializeField] private float rotationSmooth;
    [SerializeField] private float attackWaitTime;
    [SerializeField] private bool canHitOilRig;
   
    [SerializeField] private float timer;
    
    
    public GameObject bulletPrefab;
    private BuildingList _buildingList;
    
    [SerializeField] Transform target;
    
    public AudioSource audioSource;

    void Start()
    {
        _buildingList = BuildingList.Instance;
    }
    void Update()
    {
        target = FindBuilding();
        
        if (target)
        {
            RotateGunHolder();
            RotateGun();
                               
            Shoot();
        }
    }
    
    void Shoot()
    {
        timer += Time.deltaTime;
        if (timer > attackWaitTime)
        {
            GameObject currentBullet = Instantiate(bulletPrefab, gunLeftShootPoint.position, gunLeftShootPoint.rotation);
            Instantiate(audioSource, transform.position, transform.rotation);
            currentBullet.GetComponent<TankBullet>().target = target; 
            
            timer = 0;
        }
    }
    #region FindBuilding

     public Transform FindBuilding() // find the closest target in an area around the object
        {
            List<Transform> allEnemies = _buildingList.GetActiveBuildings(); 

            Transform closestTarget = null;
            float closestDistance = radius;

            foreach (Transform buildingTransform in allEnemies)
            {
                bool isOilRig = IsOilRig(buildingTransform);

                // Skip flying enemies if you cant target it
                if (isOilRig && !canHitOilRig)
                {
                    continue;
                }
            
                float distanceTobuilding = IsOilRig(buildingTransform) ? GetHorizontalDistance(buildingTransform) : Vector3.Distance(transform.position, buildingTransform.position);
            
                if (distanceTobuilding < closestDistance)
                {
                    closestTarget = buildingTransform;
                    closestDistance = distanceTobuilding;
                }
            }

            return closestTarget;;
        }
     
        private bool IsOilRig(Transform buildingTransform)
        {
            return buildingTransform.gameObject.layer == oilRigLayer; 
        }
        
        private float GetHorizontalDistance(Transform buildingTransform)
        {
            Vector3 enemyPosition = transform.position;
            Vector3 buildingPosition = buildingTransform.position;
        
            enemyPosition.y = 0;
            buildingPosition.y = 0;

            return Vector3.Distance(enemyPosition, buildingPosition);
        }
    #endregion
   
    
    #region RotationManager
    
    void RotateGunHolder()
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0;
        Quaternion rotation = Quaternion.LookRotation(direction);
        rotatingGunHolder.rotation = Quaternion.Slerp(rotatingGunHolder.rotation, rotation, rotationSmooth);
    }

    void RotateGun()
    {
        Vector3 direction = target.position - guns.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        guns.rotation = Quaternion.Slerp(guns.rotation, rotation, rotationSmooth);
    } 
    #endregion
}
