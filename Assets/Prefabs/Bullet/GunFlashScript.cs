using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFlashScript : MonoBehaviour
{
    private float killTime;

    // Start is called before the first frame update
    void Start()
    {
        killTime = Time.time + 0.05f;
    }

    void Update()
    {
        if (Time.time > killTime)
        {
            Destroy(gameObject);
        }
    }
}
