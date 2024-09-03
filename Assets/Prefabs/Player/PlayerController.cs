using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float rifleWalkSpeed = 1f;
    public int rifleDmg = 1;
    public float rifleBulletSpeed = 2f;
    public float rifleBulletTime = 2f;
    public float rifleFireSpeed = 0.1f;
    public float rifleShake = 0.25f;

    public float shotgunWalkSpeed = 0.3f;
    public int shotgunDmg = 3;
    public float shotgunBulletSpeed = 0.75f;
    public float shotgunBulletTime = 2f;
    public float shotgunFireSpeed = 1f;
    public float shotgunShake = 0.5f;

    public Transform firePoint;
    public GameObject bulletPrefab;

    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;

    public GameObject shootingSound;
    private AudioSource shootingSoundSource;
    private CameraShake cameraShake;

    private int weapon = 1;
    private float moveSpeed;
    private bool fire = false;
    private float nextFire = 0f;

    Rigidbody2D rb;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        cameraShake = GetComponent<CameraShake>();
        shootingSoundSource = shootingSound.GetComponent<AudioSource>();
        moveSpeed = rifleWalkSpeed;
    }

    private void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {

            bool success = TryMove(movementInput);

            if (!success)
            {
                success = TryMove(new Vector2(movementInput.x, 0));
            }

            if (!success)
            {
                success = TryMove(new Vector2(0, movementInput.y));
            }
            animator.SetBool("isMoving", success);
        }else
        {
            animator.SetBool("isMoving", false);
        }

        // Set direction of sprite to movement direction
        if (movementInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }else if (movementInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        if (fire && Time.time > nextFire)
        {
            if (weapon == 1)
            {
                nextFire = Time.time + rifleFireSpeed;
                shoot();
            }
            else
            {
                nextFire = Time.time + shotgunFireSpeed;
                shoot();
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


    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    private void shoot()
    {
        shootingSoundSource.Play();

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());


        float xdif = -firePoint.position.x + mousePos.x;
        float ydif = -firePoint.position.y + mousePos.y;

        float angle = Mathf.Atan(ydif / xdif);

        if (xdif > 0)
        {
            angle = angle * Mathf.Rad2Deg - 90;
        }
        else
        {
            angle = angle * Mathf.Rad2Deg + 90;
        }

        // rifle
        if (weapon == 1)
        {
            cameraShake.ShakeCamera(rifleShake, 0.25f);
            Quaternion rot = Quaternion.Euler(0, 0, angle);

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rot);
            bullet.GetComponent<BulletController>().setProperties(rifleBulletSpeed, rifleBulletTime, rifleDmg);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(bullet.transform.up * rifleBulletSpeed, ForceMode2D.Impulse); 

            //shotgun
        }
        else if (weapon == 2)
        {
            cameraShake.ShakeCamera(shotgunShake, 0.25f);
            for (int i = 0; i < 9; i++)
            {
                Quaternion rot = Quaternion.Euler(0, 0, angle + ((i * 5) - 20));

                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rot);
                bullet.GetComponent<BulletController>().setProperties(shotgunBulletSpeed, shotgunBulletTime, shotgunDmg);

                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(bullet.transform.up * shotgunBulletSpeed, ForceMode2D.Impulse);
            }

        }
    }


    public void OnFire(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                fire = true;
                break;
            case InputActionPhase.Started:
                fire = true;
                break;
            case InputActionPhase.Canceled:
                fire = false;
                break;
        }
    }

    public void OnSwapWeapon(InputAction.CallbackContext context)
    {
        if (weapon == 1)
        {
            weapon = 2;
            moveSpeed = shotgunWalkSpeed;
        }
        else
        {
            weapon = 1;
            moveSpeed = rifleWalkSpeed;
        }
    }

}
