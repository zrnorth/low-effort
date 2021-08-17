using UnityEngine;

public class CameraSpaceMovement : MonoBehaviour
{
    public float moveSpeed;

    // Components
    private Rigidbody _rb;
    private Camera _mainCam;

    // Privates
    private Vector3 _moveInput;
    private Vector3 _moveVelocity;

    private void Start() {
        _rb = GetComponent<Rigidbody>();
        _mainCam = Camera.main;
    }

    private void Update() {
        float leftStickHoriz = Input.GetAxis("Horizontal");
        float leftStickVert = Input.GetAxis("Vertical");

        _moveInput = new Vector3(leftStickHoriz, 0f, leftStickVert);

        // We want to move the player relative to the direction the player is moving the stick, 
        // not relative to the player's current rotation, so we do some conversions here to 
        // transform a camera-space input to local-space movement.
        // First we rotate the player, and then move using transform.forward.

        // Get the forward direction of the camera, i.e. the perspective of the player
        Vector3 cameraForward = _mainCam.transform.forward;
        cameraForward.y = 0; // Don't want to move up and down

        Quaternion cameraRelativeRotation = Quaternion.FromToRotation(Vector3.forward, cameraForward);
        Vector3 lookToward = cameraRelativeRotation * _moveInput;

        if (_moveInput.sqrMagnitude > 0) {
            Ray lookRay = new Ray(transform.position, lookToward);
            transform.LookAt(lookRay.GetPoint(1));
        }

        // Now that we are rotated in the right direction, we can move using transform.forward
        _moveVelocity = transform.forward * moveSpeed * _moveInput.sqrMagnitude;
    }

    private void FixedUpdate() {
        _rb.velocity = _moveVelocity;
    }
}