using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float range = 1f;
    public bool m_Start;
    public Transform player;
    

    void playerCollision()
    {
        if (player.transform.position = )
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (m_Start)
            Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
