using UnityEngine;

public class GunHandler : MonoBehaviour
{
    public int damage = 10;
    public int range = 100;
    public float fireRate = 15f;

    public AudioSource fire;
    public Camera cam;

    private float nextTimeToFire = 0f;

  
    void Update()
    {
        if (Input.GetButton ("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast (cam.transform.position, cam.transform.forward, out hit, range))
        {
            fire.Play();

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

}
