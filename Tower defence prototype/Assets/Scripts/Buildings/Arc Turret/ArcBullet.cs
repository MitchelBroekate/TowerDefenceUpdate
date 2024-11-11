using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcBullet : MonoBehaviour
{
    [SerializeField] private float force;
    public float lifeTime;
    public float damage;
    
    private Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, lifeTime);
    }
    private void FixedUpdate()
    {
        rb.velocity = transform.forward * force;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
            print("Hit");
            
        }
    }
}
