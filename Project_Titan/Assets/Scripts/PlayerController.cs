using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Objects")]
    public Transform camera1;
    public Rigidbody rb;

    [Header("Personal Settings")]
    public float horizontalSensitivity = 5;
    public float verticalSensitivity = 5;
    public KeyCode JumpKeyCode = KeyCode.Space;
    public KeyCode SprintKeyCode = KeyCode.LeftShift;

    [Header("Camera Settings")]
    public float cameraMaximumY = 90f;
    public float cameraMinimumY = -90f;
    public float rotationSmoothSpeed = 10f;

    [Header("Game Settings")]
    public float walkSpeed = 9f;
    public float runSpeed = 14f;
    public float maxSpeed = 20f;
    public float jumpPower = 30f;
    public float extraGravity = 45;

    [Header("Game Information")]
    public bool grounded;


    float bodyRotationX;
    float camRotationY;
    Vector3 directionIntentX;
    Vector3 directionIntentY;
    float speed;



    void Update()
    {
        LookRotation();
        Movement();
        if (grounded && Input.GetKeyDown(JumpKeyCode))
        {
            Jump();

        }
    }
    void FixedUpdate()
    {
        ExtraGravity();

        GroundCheck();
 
    }

    void LookRotation()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //Camera en Body rotation waardes
        bodyRotationX += Input.GetAxis("Mouse X") * horizontalSensitivity;
        camRotationY += Input.GetAxis("Mouse Y") * verticalSensitivity;

        //Stop from Rotating
        camRotationY = Mathf.Clamp(camRotationY, cameraMinimumY, cameraMaximumY);

        //handle rotations body & camera
        Quaternion camTargetRotation = Quaternion.Euler(-camRotationY, 0, 0);
        Quaternion bodyTargetRotation = Quaternion.Euler(0, bodyRotationX, 0);

        //handle rotations
        transform.rotation = Quaternion.Lerp(transform.rotation, bodyTargetRotation, Time.deltaTime * rotationSmoothSpeed);

        camera1.localRotation = Quaternion.Lerp(camera1.localRotation, camTargetRotation, Time.deltaTime * rotationSmoothSpeed);
    }

    void Movement()
    {
        directionIntentX = camera1.right;
        directionIntentX.y = 0;
        directionIntentX.Normalize();

        directionIntentY = camera1.forward;
        directionIntentY.y = 0;
        directionIntentY.Normalize();

        rb.velocity = directionIntentY * Input.GetAxis("Vertical") * speed + directionIntentX * Input.GetAxis("Horizontal") * speed + Vector3.up * rb.velocity.y;
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);

        if (Input.GetKey(SprintKeyCode))
        {
            speed = runSpeed;
        }
        if (!Input.GetKey(SprintKeyCode))
        {
            speed = walkSpeed;
        }
    }

    void ExtraGravity()
    {
        rb.AddForce(Vector3.down * extraGravity);
    }

    void GroundCheck()
    {
        RaycastHit groundHit;
        grounded = Physics.Raycast(transform.position, -transform.up, out groundHit, 1.25f);
    }

    void Jump()
    {
       rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

    }

}
