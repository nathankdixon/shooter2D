using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelEditor : MonoBehaviour
{
    public AudioSource doorOpenSound;
    public Tilemap currentTilemap;
    public float minWaitTime;
    public TileBase tL1;
    public TileBase bL1;
    public TileBase tM1;
    public TileBase bM1;
    public TileBase tR1;
    public TileBase bR1;

    public TileBase tLO1;
    public TileBase bLO1;
    public TileBase tRO1;
    public TileBase bRO1;

    public Vector2Int left1;
    public Vector2Int right1;

    public TileBase tL2;
    public TileBase bL2;
    public TileBase tM2;
    public TileBase bM2;
    public TileBase tR2;
    public TileBase bR2;

    public TileBase tLO2;
    public TileBase bLO2;
    public TileBase tRO2;
    public TileBase bRO2;

    public Vector2Int left2;
    public Vector2Int right2;

    public BoxCollider2D player;

    private float minWaitTimeVal;
    private bool activated = false;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col != player)
        {
            return;
        }
        if (!activated)
        {
            minWaitTimeVal = Time.time + minWaitTime;
        }

        currentTilemap.SetTile(new Vector3Int(left1.x, left1.y, 0), tL1);
        currentTilemap.SetTile(new Vector3Int(left1.x, left1.y -1, 0), bL1);
        currentTilemap.SetTile(new Vector3Int(right1.x, right1.y, 0), tR1);
        currentTilemap.SetTile(new Vector3Int(right1.x, right1.y -1, 0), bR1);

        currentTilemap.SetTile(new Vector3Int(left2.x, left2.y, 0), tL2);
        currentTilemap.SetTile(new Vector3Int(left2.x, left2.y - 1, 0), bL2);
        currentTilemap.SetTile(new Vector3Int(right2.x, right2.y, 0), tR2);
        currentTilemap.SetTile(new Vector3Int(right2.x, right2.y - 1, 0), bR2);

        for (int i = left1.x + 1; i <= right1.x -1; i++)
        {
            currentTilemap.SetTile(new Vector3Int(i, right1.y, 0), tM1);
            currentTilemap.SetTile(new Vector3Int(i, right1.y - 1, 0), bM1);
        }

        for (int i = left2.x + 1; i <= right2.x - 1; i++)
        {
            currentTilemap.SetTile(new Vector3Int(i, right2.y, 0), tM2);
            currentTilemap.SetTile(new Vector3Int(i, right2.y - 1, 0), bM2);
        }
        activated = true;
    }

    private void Update()
    {
        if (! activated)
        {
            return;
        }
        if (Time.time < minWaitTimeVal)
        {
            return;
        }
        if (GameObject.FindGameObjectsWithTag("enemy").Length == 0)
        {
            currentTilemap.SetTile(new Vector3Int(left1.x, left1.y, 0), tLO1);
            currentTilemap.SetTile(new Vector3Int(left1.x, left1.y - 1, 0), bLO1);
            currentTilemap.SetTile(new Vector3Int(right1.x, right1.y, 0), tRO1);
            currentTilemap.SetTile(new Vector3Int(right1.x, right1.y - 1, 0), bRO1);

            currentTilemap.SetTile(new Vector3Int(left2.x, left2.y, 0), tLO2);
            currentTilemap.SetTile(new Vector3Int(left2.x, left2.y - 1, 0), bLO2);
            currentTilemap.SetTile(new Vector3Int(right2.x, right2.y, 0), tRO2);
            currentTilemap.SetTile(new Vector3Int(right2.x, right2.y - 1, 0), bRO2);

            for (int i = left1.x + 1; i <= right1.x - 1; i++)
            {
                currentTilemap.SetTile(new Vector3Int(i, right1.y, 0), null);
                currentTilemap.SetTile(new Vector3Int(i, right1.y - 1, 0), null);
            }

            for (int i = left2.x + 1; i <= right2.x - 1; i++)
            {
                currentTilemap.SetTile(new Vector3Int(i, right2.y, 0), null);
                currentTilemap.SetTile(new Vector3Int(i, right2.y - 1, 0), null);
            }
            doorOpenSound.Play();
            Destroy(gameObject);
        }
    }
}
