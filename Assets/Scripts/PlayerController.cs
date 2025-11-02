using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 10.0f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Transform camTransform;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        camTransform = Camera.main.transform;
    }

    void Update()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 inputDir = new Vector3(horizontal, 0f, vertical);

        if (inputDir.magnitude < 0.1f)
        {
            controller.Move(velocity * Time.deltaTime);
            return;
        }

        Vector3 camForward = camTransform.forward;
        Vector3 camRight = camTransform.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDirection = (camForward * vertical + camRight * horizontal).normalized;

        controller.Move(moveDirection * moveSpeed * Time.deltaTime + velocity * Time.deltaTime);

        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}