using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GunHandler : MonoBehaviour
{
    public int damage = 10;
    public int range = 100;
    public float fireRate = 15f;

    public int maxAmmo = 30;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;

    public AudioSource fire;
    public Camera cam;

    private float nextTimeToFire = 0f;

    public Animator animator;

    public Text reloadCount;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("reloading", false);
    }

    void Update()
    {

        if (isReloading)
            return;

        if(currentAmmo <= 0 )
        {
            StartCoroutine(Reload());
            return;
        }
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo != maxAmmo)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton ("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }

        reloadCount.text = currentAmmo.ToString();
    }

    void Shoot()
    {

        currentAmmo--;

        fire.Play();
        RaycastHit hit;
        if (Physics.Raycast (cam.transform.position, cam.transform.forward, out hit, range))
        {
            

            if (hit.transform.tag == "Envoirement")
            {

            }

            Dummy target = hit.transform.GetComponent<Dummy>();
            if(target != null)
            {
                target.TakeDamage(damage); 
            }
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("kogels zijn op");

        animator.SetBool("reloading", true);

        yield return new WaitForSeconds(reloadTime - .25f);

        animator.SetBool("reloading", false);

        yield return new WaitForSeconds(.25f);

        currentAmmo = maxAmmo;
        isReloading = false;
    }

}
