using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform camera;
    public Rigidbody rb;

    public float horizontalSensitivity = 5;
    public float verticalSensitivity = 5;
    public float cameraMaximumY = 90f;
    public float cameraMinimumY = -90f;
    public float rotationSmoothSpeed = 10f;

    public float walkSpeed = 9f;
    public float runSpeed = 14f;
    public float maxSpeed = 20f;
    public float jumpPower = 30f;

    public float extraGravity = 45;

    float bodyRotationX;
    float camRotationY;
    Vector3 directionIntentX;
    Vector3 directionIntentY;
    float speed;

    public bool grounded;

    void Update()
    {
        LookRotation();
        Movement();
        if (grounded && Input.GetButtonDown("Jump"))
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

        camera.localRotation = Quaternion.Lerp(camera.localRotation, camTargetRotation, Time.deltaTime * rotationSmoothSpeed);
    }

    void Movement()
    {
        directionIntentX = camera.right;
        directionIntentX.y = 0;
        directionIntentX.Normalize();

        directionIntentY = camera.forward;
        directionIntentY.y = 0;
        directionIntentY.Normalize();

        rb.velocity = directionIntentY * Input.GetAxis("Vertical") * speed + directionIntentX * Input.GetAxis("Horizontal") * speed + Vector3.up * rb.velocity.y;
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
        }
        if (!Input.GetKey(KeyCode.LeftShift))
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
