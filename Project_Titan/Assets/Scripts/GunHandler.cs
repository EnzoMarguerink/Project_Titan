using UnityEngine;

public class GunHandler : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;

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
            Debug.Log(hit.transform.name);

            Damageable target = hit.transform.GetComponent<Damageable>();
            if(target != null)
            {
                target.DoDamage(damage, null ); 
            }
        }
    }

}
