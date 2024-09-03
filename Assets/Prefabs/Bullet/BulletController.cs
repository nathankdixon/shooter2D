using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
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

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        Vector2 direction = new Vector2(pos.position.x, pos.position.y).normalized;

        int count = rb.Cast(
            direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
            movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
            castCollisions, // List of collisions to store the found collisions into after the Cast is finished
            speed * Time.fixedDeltaTime + collisionOffset); // The amount to cast equal to the movement plus an offset  

        if (count >= 1)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "EnemyHitBox")
        {
            other.gameObject.transform.parent.gameObject.GetComponent<MonsterHealthManager>().HurtMonster(damageToGive);
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
