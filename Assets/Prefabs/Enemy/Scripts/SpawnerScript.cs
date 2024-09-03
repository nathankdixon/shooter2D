using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    private float timer = 0;

    private GameObject monster;
    private float spawnTimeInterval;
    private int numToSpawn;
    private float spawnSizeX;
    private float spawnSizeY;

    // Start is called before the first frame update
    void Start()
    {
        timer = spawnTimeInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            numToSpawn--;
            timer = spawnTimeInterval;
            Vector3 spawnPos = transform.position;
            spawnPos += Vector3.right * spawnSizeX * (Random.value - 0.5f);
            spawnPos += Vector3.up * spawnSizeY * (Random.value - 0.5f);
            Instantiate(monster, spawnPos, Quaternion.identity);
            if (numToSpawn == 0 && numToSpawn != null)
            {
                Destroy(gameObject);
            }
        }
    }


    public void setData(GameObject whatToSpawn, float spawnereInterval, int numToSpawn, float xSize, float ySize)
    {
        this.monster = whatToSpawn;
        this.spawnTimeInterval = spawnereInterval;
        this.numToSpawn = numToSpawn;
        this.spawnSizeX = xSize;   
        this.spawnSizeY = ySize;
    }
}
