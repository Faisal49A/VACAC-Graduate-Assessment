using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float fastMoveSpeed = 16f;

    [Header("Zoom")]
    [SerializeField] private float zoomSpeed = 8f;
    [SerializeField] private float minHeight = 3f;
    [SerializeField] private float maxHeight = 25f;

    [Header("Rotation")]
    [SerializeField] private float lookSensitivity = 2f;
    [SerializeField] private float minPitch = 15f;
    [SerializeField] private float maxPitch = 80f;

    private float yaw;
    private float pitch;

    private void Start()
    {
        Vector3 angles = transform.eulerAngles;

        yaw = angles.y;
        pitch = angles.x;
    }

    private void Update()
    {
        HandleMovement();
        HandleZoom();
        HandleRotation();
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        // Keep WASD movement horizontal.
        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 direction =
            (forward * vertical + right * horizontal).normalized;

        float currentSpeed =
            Input.GetKey(KeyCode.LeftShift)
            ? fastMoveSpeed
            : moveSpeed;

        transform.position +=
            direction * currentSpeed * Time.deltaTime;
    }

    private void HandleZoom()
    {
        float scroll = Input.mouseScrollDelta.y;

        if (Mathf.Abs(scroll) < 0.01f)
            return;

        Vector3 position = transform.position;

        position += transform.forward * scroll * zoomSpeed;

        position.y = Mathf.Clamp(
            position.y,
            minHeight,
            maxHeight
        );

        transform.position = position;
    }

    private void HandleRotation()
    {
        if (!Input.GetMouseButton(1))
            return;

        float mouseX =
            Input.GetAxis("Mouse X") * lookSensitivity;

        float mouseY =
            Input.GetAxis("Mouse Y") * lookSensitivity;

        yaw += mouseX;
        pitch -= mouseY;

        pitch = Mathf.Clamp(
            pitch,
            minPitch,
            maxPitch
        );

        transform.rotation =
            Quaternion.Euler(
                pitch,
                yaw,
                0f
            );
    }
}