using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 500f;

    private float xRotation = 0f; // Vertical rotation (up/down)

    public float topClamp = -90f;
    public float bottomClamp = 90f;

    public Transform playerBody; // Reference to player body

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor
    }

    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate player body left/right
        playerBody.Rotate(Vector3.up * mouseX);

        // Rotate camera up/down
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, topClamp, bottomClamp);

        // Apply vertical rotation to camera only
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
