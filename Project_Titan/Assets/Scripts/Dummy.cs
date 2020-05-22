using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Dummy : MonoBehaviour
{

    public TextMeshPro healthText;

    public int maxHealth = 100;
    public int curHealth;


    // Start is called before the first frame update
    void Awake()
    {
        healthText.text = curHealth.ToString();
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = curHealth.ToString();

        healthText.transform.LookAt(Camera.current.transform.position);
        healthText.transform.Rotate(0, 180, 0);

    }

    public void TakeDamage(int damageTotal)
    {
            if (damageTotal > 0)
            {
                curHealth -= damageTotal;
            }

            if (curHealth <= 0)
            {
            gameObject.SetActive(false);
            }

    }
}
