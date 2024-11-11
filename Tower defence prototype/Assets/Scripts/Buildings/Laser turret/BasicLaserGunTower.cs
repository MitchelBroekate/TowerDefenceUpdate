using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BasicLaserGunTower : TowerBehaviour
{
    [SerializeField] private Transform laserTower;
    [SerializeField] private Transform rotatingGunHolder;
    [SerializeField] private Transform[] guns;
    [SerializeField] private Transform[] shootPoints;
    [SerializeField] private int shootPointIndex = 0;
    
    [SerializeField] private float rotationSmooth;
    [SerializeField] private float attackWaitTime;
    
    [SerializeField] private int damage;
    private float timer;
    
    public GameObject bulletPrefab;
    public AudioSource bulletSound;
    
    // Update is called once per frame
    void Update()
    {
        enemy = FindEnemy();
        
        if (enemy && isActive)
        {
            RotateGunHolder();
            RotateGun();
                               
            Shoot();
        }
    }
    
    void Shoot()
    {
        timer += Time.deltaTime;
        
        if (shootPointIndex == shootPoints.Length)
        {
            shootPointIndex = 0;
        } 
        
        if (timer > attackWaitTime)
        {
            GameObject currentBullet = Instantiate(bulletPrefab, shootPoints[shootPointIndex].position,shootPoints[shootPointIndex].rotation); 
            Instantiate(bulletSound, transform.position, transform.rotation);
            currentBullet.GetComponent<LaserBullet>().target = enemy;
            currentBullet.GetComponent<LaserBullet>().damage = damage;
            shootPointIndex++;
            timer = 0;
        }
    }
    
    #region RotationManager
    
    void RotateGunHolder()
    {
        Vector3 direction = enemy.position - laserTower.position;
        direction.y = 0;
        Quaternion rotation = Quaternion.LookRotation(direction);
        rotatingGunHolder.rotation = Quaternion.Slerp(rotatingGunHolder.rotation, rotation, rotationSmooth);
    }

    void RotateGun()
    {
        for (int i = 0; i < guns.Length; i++)
        {
            Vector3 direction = enemy.position - guns[i].position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            guns[i].rotation = Quaternion.Slerp(guns[i].rotation, rotation, rotationSmooth);
        }
    } 
    #endregion
}
