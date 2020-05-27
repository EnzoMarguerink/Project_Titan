using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBomb : MonoBehaviour
{
    public GameObject bomb;
    public PlayerController pc;
    public float plantTime = 5;
    public float plantingTime = 0;

    public GameObject weaponHolder;
    public int weaponHolderAmount;

    public bool inplantZone;
    public bool bombPlanted;
    public bool canBePlanted;
    public bool hasBomb;



    // Start is called before the first frame update
    void Start()
    {
        pc.bombText.gameObject.SetActive(false);
        inplantZone = false;
        bombPlanted = false;
        canBePlanted = true;
        hasBomb = false;

        weaponHolder = GameObject.FindGameObjectWithTag("WeaponHolder");
        weaponHolderAmount = weaponHolder.transform.childCount;
        
    }


    // Update is called once per frame
    void Update()
    {
        if (weaponHolderAmount == 4)
        {
            hasBomb = true;
        } else if(weaponHolderAmount != 4) 
        {
            hasBomb = false;
        }


        if (inplantZone && Input.GetKey(pc.PlantKeyCode) && canBePlanted && hasBomb)
        {
            Plant();
        }
        if (inplantZone && Input.GetKeyUp(pc.PlantKeyCode) && canBePlanted)
        {
            plantingTime = 0;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag.Equals("Player") && hasBomb)
        {
            pc.bombText.gameObject.SetActive(true);
            inplantZone = true;
        }

    }

    void OnTriggerExit(Collider col)
    {
       

        if (col.gameObject.tag.Equals("Player") && hasBomb)
        {
            pc.bombText.gameObject.SetActive(false);
            inplantZone = false;
        }
    }

    void Plant()
    {
        plantingTime += Time.deltaTime;
        
        if (plantingTime >= plantTime)
        {
            pc.bombText.text = "Bomb Planted";
            bombPlanted = true;
            Instantiate(bomb, pc.transform.position, Quaternion.FromToRotation(Vector3.forward, pc.transform.position));
            canBePlanted = false;
        }
    }
}
