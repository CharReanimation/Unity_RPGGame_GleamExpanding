using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Target to Sync With")]
    [Tooltip("The object whose rotation the camera will sync with.")]
    public Transform target;

    [Header("Offset Settings")]
    [Tooltip("Optional offset for the camera's Y-Axis Rotation relative to the target.")]
    public float yRotationOffset;

    [Header("Position Offset Settings")]
    [Tooltip("Vertical Distance from the target.")]
    public float yDistance = 2f;

    [Tooltip("Backward Distance from the target.")]
    public float zDistance = -3f;

    [Header("Smooth Settings")]
    public float positionSmoothTime = 0.2f;
    public float rotationSmoothTime = 0.2f;

    [Header("Mouse Look Settings")]
    public float mouseHorizontalSensitivity = 20f;
    public float mouseVerticalSensitivity = 25f;

    [Tooltip("Maximum vertical rotation limit.")]
    public float verticalRotationLimit = 80f;

    // Private fields
    private Vector3 velocity = Vector3.zero;

    private float currentYRotation; // For smooth Y Rotation
    private float currentVelocity = 0f;

    private float horizontalRotation = 0f; // Horizontal Rotation Angle
    private float verticalRotation = 0f; // Vertical Rotation Angle

    // PlayerMove
    public PlayerMove playerMove;




    void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Target is not assigned. Please assign a target in the inspector.");
            return;
        }
        // Handle Mouse Look
        HandleMouseHorizontalLook();
        HandleMouseVerticalLook();

        // Handle Sync Camera
        HandleSyncCamera();
    }








    // Handle Mouse Vertical Look
    private void HandleMouseVerticalLook()
    {
        // Get Mouse Y Input
        float mouseY = Input.GetAxis("Mouse Y") * mouseVerticalSensitivity * Time.deltaTime;

        // Adjust Vertical Rotation
        float targetVerticalRotation = verticalRotation - mouseY;
        targetVerticalRotation = Mathf.Clamp(targetVerticalRotation, -verticalRotationLimit, verticalRotationLimit);

        verticalRotation = Mathf.LerpAngle(verticalRotation, targetVerticalRotation, rotationSmoothTime);
    }


    // Handle Mouse Horizontal Look
    private void HandleMouseHorizontalLook()
    {
        // Get Mouse Input
        float mouseX = Input.GetAxis("Mouse X") * mouseHorizontalSensitivity * Time.deltaTime;

        // Adjust Horizontal Rotation
        horizontalRotation += mouseX;
    }


    // Handle Sync Camera
    private void HandleSyncCamera()
    {
        SyncCameraPosition();
    }


    // Sync Camera Position
    private void SyncCameraPosition()
    {
        // Get Target Rotation
        bool isMoving = (playerMove.currentXSpeed != 0) || (playerMove.currentZSpeed != 0);

        // Rotation Only During Movement
        if (isMoving)
        {
            float targetYRotation = target.eulerAngles.y;
            horizontalRotation = Mathf.SmoothDampAngle(horizontalRotation, targetYRotation, ref currentVelocity, rotationSmoothTime);
        }

        // Get the target's position and calculate the desired position
        Quaternion horizontalRotationQuat = Quaternion.Euler(0, horizontalRotation, 0);
        Vector3 offset = horizontalRotationQuat * new Vector3(0, 0, zDistance);
        Vector3 targetPosition = target.position + Vector3.up * yDistance + offset;

        // Update the camera's position smoothly
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, positionSmoothTime);

        Quaternion targetRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * (1f / rotationSmoothTime));
    }
}
