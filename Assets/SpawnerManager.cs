using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public BoxCollider2D player;
    public GameObject spawner;
    public int numOfSpawneres;
    public GameObject[] whatToSpawn;
    public int[] numToSpawn;
    public float[] spawnInterval;
    public float[] xPos;
    public float[] yPos;
    public float[] xSize;
    public float[] ySize; 

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col != player)
        {
            return;
        }
        CreateSpawners();
    }

    private void CreateSpawners()
    {
        for (int i = 0; i < numOfSpawneres; i++)
        {
            print("CC"+i);
            GameObject newSpawner = Instantiate(spawner, new Vector3(xPos[i], yPos[i], 0), Quaternion.identity);
            SpawnerScript newSpawnerScript = newSpawner.GetComponent<SpawnerScript>();
            newSpawnerScript.setData(whatToSpawn[i], spawnInterval[i], numToSpawn[i], xSize[i], ySize[i]);
        }
        Destroy(gameObject);
    }
}
