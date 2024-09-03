using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerActivation : MonoBehaviour
{
    public GameObject spawner;
    public BoxCollider2D player;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col != player)
        {
            return;
        }
        
    }
}
