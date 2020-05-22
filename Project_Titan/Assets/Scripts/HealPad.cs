using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPad : MonoBehaviour
{
    public int maxHealBack = 50;
    public int healPerSecond = 5;

    // Start is called before the first frame update
    void Start()
    {
        //debug.log("yeet");
    }

    void OnTriggerEnter(Collider col)
    {
        

        if (col.gameObject.tag.Equals("Player") && PlayerController.curHealth < maxHealBack)
        {
            StartCoroutine("Heal");
        }
    }


    void OnTriggerExit(Collider col)
    {

        if (col.gameObject.tag.Equals("Player"))
        {
            StopCoroutine("Heal");
        }
    }


    IEnumerator Heal()
    {
        for (float currentHealth = PlayerController.curHealth; currentHealth <= maxHealBack; currentHealth += healPerSecond * Time.deltaTime)
        {
            PlayerController.curHealth = currentHealth;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        PlayerController.curHealth = maxHealBack;
    }

}
