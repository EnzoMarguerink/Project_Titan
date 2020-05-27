using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{

    [Header("Objects")]
    public Transform camera1;
    public Rigidbody rb;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI armorText;
    public TextMeshProUGUI bombText;
    public TextMeshProUGUI bombcountdownText;

    [Header("Personal Settings")]
    public float horizontalSensitivity = 5;
    public float verticalSensitivity = 5;
    public KeyCode JumpKeyCode = KeyCode.Space;
    public KeyCode SprintKeyCode = KeyCode.LeftShift;
    public KeyCode PlantKeyCode = KeyCode.E;

    [Header("Camera Settings")]
    public float cameraMaximumY = 90f;
    public float cameraMinimumY = -90f;
    public float rotationSmoothSpeed = 10f;

    [Header("Movement Settings")]
    public float walkSpeed = 9f;
    public float runSpeed = 14f;
    public float maxSpeed = 20f;
    public float jumpPower = 30f;
    public float extraGravity = 45;

    [Header("Health Settings")]
    public int maxHealth = 100;
    public static float curHealth;
    public int staArmor = 50;
    public int curArmor;
    public bool Godmode = false;

    [Header("Game Information")]
    public bool grounded;


    float bodyRotationX;
    float camRotationY;
    Vector3 directionIntentX;
    Vector3 directionIntentY;
    float speed;

    void Awake()
    {
        curHealth = maxHealth;
        healthText.text = curHealth.ToString("F0");
        curArmor = staArmor;
        armorText.text = curArmor.ToString("F0");
        bombcountdownText.gameObject.SetActive(false);
    }

    void Update()
    {
        LookRotation();
        Movement();
        if (grounded && Input.GetKeyDown(JumpKeyCode))
        {
            Jump();

        }
        HealthCheck();
        

    }
    void FixedUpdate()
    {
        ExtraGravity();

        GroundCheck();
 
    }

    #region Look & Rotation

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

    #endregion

    #region Movement

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

    #endregion


    #region Damage & Health

    void HealthCheck()
    {
        healthText.text = curHealth.ToString("F0");
        armorText.text = curArmor.ToString("F0");
    }

    public void TakeDamage (int damageTotal)
    {
        if (!Godmode)
        {
            

            if (curArmor > 0)
            {
                var armorDamage = Mathf.Min(damageTotal, curArmor);
                curArmor -= armorDamage;
                damageTotal -= armorDamage;
            }

            if (damageTotal > 0)
            {
                curHealth -= damageTotal;
            }

            if (curHealth <= 0)
            {
                Debug.Log("Died");
            }


        }
    }

    public void addHealth (int healTotal)
    {
        curHealth += healTotal;
        curHealth = Mathf.Clamp(curHealth, 0, maxHealth);
    }

    public void addArmor(int armoraddTotal)
    {
        curArmor += armoraddTotal;
        curArmor = Mathf.Clamp(curArmor, 0, staArmor);
    }

    #endregion

}
