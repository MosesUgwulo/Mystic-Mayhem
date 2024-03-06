using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnObby : MonoBehaviour
{
    public GameObject[] opps;
    public GameObject[] spawnPoints;
    void Start()
    {
        
    }

    void Update()
    {
        
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                Instantiate(opps[i], spawnPoints[i].transform.position, Quaternion.identity);
            }
        }
    }
}
