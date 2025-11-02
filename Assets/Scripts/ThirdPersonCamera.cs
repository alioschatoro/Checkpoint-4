using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;
    public float distance = 5.0f;
    public float mouseSensitivity = 2.0f;

    public float pitchMin = -40.0f;
    public float pitchMax = 80.0f;

    public LayerMask collisionMask;
    public float collisionOffset = 0.2f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private float currentDistance;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        currentDistance = distance;
    }

    void LateUpdate()
    {
        if (target == null) return;

        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;

        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        Vector3 desiredPosition = target.position - (rotation * Vector3.forward * distance);

        RaycastHit hit;
        Vector3 directionToCamera = desiredPosition - target.position;

        float desiredDist = distance;

        if (Physics.Raycast(target.position, directionToCamera.normalized, out hit, distance, collisionMask))
        {
            currentDistance = hit.distance - collisionOffset;
        }
        else
        {
            currentDistance = Mathf.Lerp(currentDistance, distance, Time.deltaTime * 5f);
        }

        currentDistance = Mathf.Clamp(currentDistance, 0.1f, distance);

        Vector3 finalPosition = target.position - (rotation * Vector3.forward * currentDistance);

        transform.position = finalPosition;
        transform.LookAt(target.position);
    }
}