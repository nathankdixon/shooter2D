using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    public int damageToGive;


    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "PlayerHitBox")
        {
            other.gameObject.transform.parent.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(damageToGive);
        }
    }
}
