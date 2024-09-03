using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaController : MonoBehaviour
{

    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.05f;
    
    
    private float lifeTime = 4f;
    private Transform pos;
    private float speed = 2f;
    private int damageToGive = 1;
    private Rigidbody2D rb;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    // Start is called before the first frame update
    void Start()
    {
        pos = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        print("----");
        print(other.gameObject);
        if (other.gameObject.tag == "PlayerHitBox")
        {
            print(other.gameObject.tag);
            print("Hit");
            other.gameObject.transform.parent.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(damageToGive);
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Wall")
        {
            print("Wall");
            Destroy(gameObject);
        }
    }

    public void setProperties(float lifeTime, float speed, int damageToGive)
    {
        this.lifeTime = lifeTime;
        this.speed = speed;
        this.damageToGive = damageToGive;
    }
}
