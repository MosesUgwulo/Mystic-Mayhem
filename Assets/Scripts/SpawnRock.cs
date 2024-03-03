using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRock : MonoBehaviour
{
    public GameObject[] rockPrefab;
    public GameObject[] spawnPoint;
    void Start()
    {
        
        // Place spawn points randomly
        foreach (var point in spawnPoint)
        {
            point.transform.localPosition = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
        }
        
        // Place rocks on spawn points
        
        for (int i = 0; i < spawnPoint.Length; i++)
        {
            int rockIndex = Random.Range(0, rockPrefab.Length);
            Instantiate(rockPrefab[rockIndex], spawnPoint[i].transform.position, rockPrefab[rockIndex].transform.rotation);
        }
        
        // for (int i = 0; i < spawnPoint.Length; i++)
        // {
        //     int rockIndex = Random.Range(0, rockPrefab.Length);
        //     Instantiate(rockPrefab[rockIndex], spawnPoint[i].position, rockPrefab[rockIndex].transform.rotation);
        // }
    }
}
