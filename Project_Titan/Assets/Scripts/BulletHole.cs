using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHole : MonoBehaviour
{
    private SpriteRenderer SpriteR;
    public Sprite[] sprites;

    public float despawnTime;


    // Start is called before the first frame update
    void Start()
    {
        SpriteR = gameObject.GetComponentInChildren<SpriteRenderer>();

       RandomSpriteSelect();

        StartCoroutine(DespawnTime());
    }


    IEnumerator DespawnTime()
    {
        yield return new WaitForSeconds(despawnTime);

        Destroy(gameObject);
    }

    void RandomSpriteSelect()
    {
        int randomSpriteint = 0;

        randomSpriteint = Random.Range(0, sprites.Length);
        SpriteR.sprite = sprites[randomSpriteint];
    }


    }

