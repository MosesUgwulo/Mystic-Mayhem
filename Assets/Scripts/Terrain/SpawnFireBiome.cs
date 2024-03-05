using UnityEngine;

namespace Terrain
{
    public class SpawnFireBiome : MonoBehaviour
    {
        public GameObject[] firePrefabs;
        public GameObject[] spawnPoints;
        void Start()
        {
            foreach (var point in spawnPoints)
            {
                point.transform.localPosition = new Vector3(Random.Range(-0.4f, 0.4f), 0, Random.Range(-0.4f, 0.4f));
            }
        
            for (int i = 0; i < spawnPoints.Length; i++)
            { 
                int fireIndex = Random.Range(0, firePrefabs.Length); 
                Instantiate(firePrefabs[fireIndex], spawnPoints[i].transform.position, firePrefabs[fireIndex].transform.rotation);
            }
        }
    }
}
