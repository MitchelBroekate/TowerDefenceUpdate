using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : MonoBehaviour
{
    [SerializeField] private float force;
    public int damage;
    public Transform target;
    public float rotateSpeed;
    private float _timer;
    public float homingDuration;
    private Rigidbody rb; 
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 5);
    }

    private void Update()
    {
        if (!target)
        {
            Destroy(gameObject);
        }
    }

    private void LateUpdate()
    {   
        if (!target)
        {
            Destroy(gameObject);
        }
        
        _timer += Time.deltaTime;

        if(_timer <= homingDuration)
        {
            Vector3 direction = target.position - rb.position;
            direction.Normalize();

            Vector3 amountToRotate = Vector3.Cross(direction, transform.forward) * Vector3.Angle(transform.forward, direction);

            rb.angularVelocity = -amountToRotate * rotateSpeed;

            rb.velocity = transform.forward * force;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
            print("Hit");
            
            Destroy(gameObject);
        }
    }
}
