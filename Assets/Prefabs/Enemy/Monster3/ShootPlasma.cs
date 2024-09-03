using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPlasma : MonoBehaviour
{
    public float fireTime;
    public float plasmaSpeed;
    public float plasmaTime;
    public int plasmaDmg;
    public GameObject plasmaPrefab;

    private PlayerController player;
    private Transform pos;

    private float nextFire;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        pos = GetComponent<Transform>();
        nextFire = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && Time.time > nextFire)
        {
            nextFire = Time.time + fireTime;

            float playerX = player.transform.position.x;
            float playerY = player.transform.position.y;
            float monsterX = pos.position.x;
            float monsterY = pos.position.y;

            float xdif = -monsterX + playerX;
            float ydif = -monsterY + playerY;

            float angle = Mathf.Atan(ydif / xdif);

            if (xdif > 0)
            {
                angle = angle * Mathf.Rad2Deg - 90;
            }
            else
            {
                angle = angle * Mathf.Rad2Deg + 90;
            }

            Quaternion rot = Quaternion.Euler(0, 0, angle);

            GameObject plasma = Instantiate(plasmaPrefab, pos.position, rot);
            plasma.GetComponent<PlasmaController>().setProperties(plasmaSpeed, plasmaTime + Time.deltaTime, plasmaDmg);

            Rigidbody2D rb = plasma.GetComponent<Rigidbody2D>();
            rb.AddForce(plasma.transform.up * plasmaSpeed, ForceMode2D.Impulse);

        }
    }
}
