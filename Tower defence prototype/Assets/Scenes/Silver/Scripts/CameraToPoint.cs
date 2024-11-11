using UnityEngine;
using UnityEngine.UI;

public class CameraToPoint : MonoBehaviour
{
    public Transform positionTarget; // The target position for the camera to move to
    public Transform lookAtTarget;   // The target for the camera to look at
    public float duration = 5f;      // Duration to reach the target position
    public Button moveButton;        // Reference to the UI Button

    private Vector3 startPosition;   // Starting position of the camera
    private Quaternion startRotation; // Starting rotation of the camera
    private float startTime;         // Time when the movement starts
    private bool isMoving = false;   // Flag to check if the camera is moving

    void Start()
    {
        if (moveButton != null)
        {
            // Add listener to the button
            moveButton.onClick.AddListener(StartCameraMovement);
        }
    }

    void Update()
    {
        if (isMoving)
        {
            // Calculate the elapsed time
            float elapsedTime = Time.time - startTime;

            // Calculate the interpolation factor (0 to 1)
            float t = elapsedTime / duration;

            // Smoothly interpolate the camera's position
            transform.position = Vector3.Lerp(startPosition, positionTarget.position, t);

            // Smoothly interpolate the camera's rotation
            if (lookAtTarget != null)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookAtTarget.position - transform.position);
                transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            }

            // Check if the camera has reached the target position and rotation
            if (t >= 1f)
            {
                isMoving = false; // Stop the movement when the target is reached
            }
        }
    }

    // Method to start the camera movement
    void StartCameraMovement()
    {
        // Prevent starting a new movement if the camera is already moving
        if (isMoving)
        {
            return;
        }

        if (positionTarget == null || lookAtTarget == null)
        {
            return;
        }

        // Initialize the starting position, rotation, and time
        startPosition = transform.position;
        startRotation = transform.rotation;
        startTime = Time.time;
        isMoving = true;
    }
}
