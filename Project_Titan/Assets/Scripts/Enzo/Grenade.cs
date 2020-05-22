using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{

    public float delay = 3f;

    public GameObject ExplodeEffect;

    float countdown;

    bool hasExploded = false;
    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded) 
        {
            Explode();
            hasExploded = true;
        }
    }

    void Explode()
    {
        Instantiate(ExplodeEffect, transform.position, transform.rotation);

        Destroy(gameObject);
        //Debug.Log("Boem");
    }
}
