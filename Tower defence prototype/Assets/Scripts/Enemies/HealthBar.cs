using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    private Transform cam;

    void Start()
    {
        cam = Camera.main.transform;
    }
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - cam.position);
    }
}
