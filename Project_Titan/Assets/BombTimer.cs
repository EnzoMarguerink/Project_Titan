using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTimer : MonoBehaviour
{
    public PlayerController pc;

    

    public float timeLeft = 10;

    void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        pc.bombcountdownText.gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Awake()
    {


    }

    // Update is called once per frame
    void Update()
    {

        timeLeft -= Time.deltaTime;
        pc.bombcountdownText.text = (timeLeft).ToString("F0");
        if (timeLeft < 0)
        {
            Debug.Log("BIEM");
        }
    }


}
