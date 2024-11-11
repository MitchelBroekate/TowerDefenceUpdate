using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowLookAtPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform cam;
    public Image Arrow;
    public Vector3 camLookRot;
    void Start()
    {
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        camLookRot = transform.position - cam.position;

        camLookRot.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(camLookRot);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);


    }
}
