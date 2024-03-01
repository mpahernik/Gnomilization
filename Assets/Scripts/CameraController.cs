using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of camera movement
    public float zoomSpeed = 5f; // Speed of camera zoom
    public Vector2 panLimit = new Vector2(10f, 10f); // Clamps for camera movement

    void Update()
    {
        HandleMouseInput();
        HandleKeyboardInput();
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButton(2)) // Middle mouse button for panning
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            Vector3 newPosition = transform.position;
            newPosition.x -= mouseX * moveSpeed * Time.deltaTime;
            newPosition.y -= mouseY * moveSpeed * Time.deltaTime;

            // Apply clamps
            newPosition.x = Mathf.Clamp(newPosition.x, -panLimit.x, panLimit.x);
            newPosition.y = Mathf.Clamp(newPosition.y, -panLimit.y, panLimit.y);

            transform.position = newPosition;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel"); // Mouse scroll for zooming
        float newSize = Camera.main.orthographicSize - scroll * zoomSpeed;

        // Apply clamps to zoom
        newSize = Mathf.Clamp(newSize, 1f, 20f);

        Camera.main.orthographicSize = newSize;
    }

    void HandleKeyboardInput()
    {
        float horizontal = Input.GetAxis("Horizontal"); // A and D keys for horizontal movement
        float vertical = Input.GetAxis("Vertical"); // W and S keys for vertical movement

        Vector3 direction = new Vector3(horizontal, vertical, 0).normalized;

        if (direction.magnitude >= 0.1f)
        {
            Vector3 newPosition = transform.position + direction * moveSpeed * Time.deltaTime;

            // Apply clamps
            newPosition.x = Mathf.Clamp(newPosition.x, -panLimit.x, panLimit.x);
            newPosition.y = Mathf.Clamp(newPosition.y, -panLimit.y, panLimit.y);

            transform.position = newPosition;
        }
    }
}
