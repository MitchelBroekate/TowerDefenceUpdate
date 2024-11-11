using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class CameraMovement : MonoBehaviour
{
  
    [SerializeField]private GameObject cameraHolder;

    [SerializeField] private int rotationSpeed;
    [SerializeField] private int zoomSpeed;
    [SerializeField] private float smoothFactor;
    
    private float rotationy;
    private float positionY;

    // [SerializeField] private bool rotatetingRight;
    // [SerializeField] private bool rotatetingLeft

    void Update() 
    {
        if (Input.GetKey(KeyCode.D)) //Check inputs
        {
            RotateRight();
        }
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }

        if (Input.mouseScrollDelta.y > 0)
        {
            ZoomIn();
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            ZoomOut();
        }
    }
    public void RotateRight() //rotate camera holder to right 
    {
       
        rotationy -= (rotationSpeed * Time.deltaTime);
        cameraHolder.transform.rotation = Quaternion.Euler(0, rotationy, 0);
    }

    public void RotateLeft() //rotate camera holder to left
    {
        
        rotationy += (rotationSpeed * Time.deltaTime);
        cameraHolder.transform.rotation = Quaternion.Euler(0, rotationy, 0);
    }

    public void ZoomIn()
    {
        positionY -= zoomSpeed * 100 * Time.deltaTime;
        
        positionY = Mathf.Clamp(positionY, -9, 80);
        
        Vector3 targetPosition = new Vector3(cameraHolder.transform.position.x, positionY, cameraHolder.transform.position.z);
        cameraHolder.transform.position = Vector3.Lerp(cameraHolder.transform.position, targetPosition, Time.deltaTime * smoothFactor);
        
    }
    public void ZoomOut()
    {
        positionY += zoomSpeed * 100 * Time.deltaTime;
        
        positionY = Mathf.Clamp(positionY, -9, 80);
        
        Vector3 targetPosition = new Vector3(cameraHolder.transform.position.x, positionY, cameraHolder.transform.position.z);
        cameraHolder.transform.position = Vector3.Lerp(cameraHolder.transform.position, targetPosition, Time.deltaTime * smoothFactor);
    }
}
