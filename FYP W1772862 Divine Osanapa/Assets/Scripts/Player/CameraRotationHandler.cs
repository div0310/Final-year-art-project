using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotationHandler : MonoBehaviour
{
    public Transform player;
    public float rotationSpeed = 2.0f;
    public float minVerticalAngle = -80.0f;
    public float maxVerticalAngle = 80.0f;

    private Vector3 offset; // the space between player gamobj and the camera.

    void Start()
    {
        offset = transform.position - player.position;
    }

    void LateUpdate() // using lateupdate for for camera control.
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Limit vertical rotation
        mouseY = Mathf.Clamp(mouseY, minVerticalAngle, maxVerticalAngle);

        // Rotate the camera around the player based on mouse movement
        Quaternion rotation = Quaternion.Euler(-mouseY * rotationSpeed, mouseX * rotationSpeed, 0);
        offset = rotation * offset;

        // Update the camera's position based on the new offset.
        transform.position = player.position + offset;
        transform.LookAt(player.position + new Vector3(0, 1f));
    }
}