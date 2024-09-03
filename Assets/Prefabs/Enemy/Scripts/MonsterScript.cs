using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    private Rigidbody2D rb;

    public int health;
    public float moveSpeed;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;

    public float randomMovement = 0.03f;

    private PlayerController player;
    private Transform pos;

    SpriteRenderer spriteRenderer;
    Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerController>();
        pos = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player != null)
        {
            float playerX = player.transform.position.x;
            float playerY = player.transform.position.y;
            float monsterX = playerX - pos.position.x;
            float monsterY = playerY - pos.position.y;

            Vector2 wobble = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)).normalized * Random.Range(-randomMovement, randomMovement);
            Vector2 direction = new Vector2(monsterX, monsterY).normalized;
            Vector2 move = direction + wobble;


            if (monsterX > 0)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }

            bool success = TryMove(move);

            if (!success)
            {
                success = TryMove(new Vector2(monsterX, 0).normalized);
            }

            if (!success)
            {
                success = TryMove(new Vector2(0, monsterY).normalized);
            }
        }

    }

    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            // Check for potential collisions
            int count = rb.Cast(
                direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
                movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
                castCollisions, // List of collisions to store the found collisions into after the Cast is finished
                moveSpeed * Time.fixedDeltaTime + collisionOffset); // The amount to cast equal to the movement plus an offset

            if (count == 0)
            {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            // Can't move if there's no direction to move in
            return false;
        }

    }
}
