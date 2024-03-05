using UnityEngine;

namespace Terrain
{
    public class SpawnAirBiome : MonoBehaviour
    {
        public GameObject[] airPrefabs;
        public GameObject[] spawnPoints;
        void Start()
        {
            foreach (var point in spawnPoints)
            {
                point.transform.localPosition = new Vector3(Random.Range(-0.4f, 0.4f), 0, Random.Range(-0.4f, 0.4f));
            }
        
            for (int i = 0; i < spawnPoints.Length; i++)
            { 
                int airIndex = Random.Range(0, airPrefabs.Length); 
                Instantiate(airPrefabs[airIndex], spawnPoints[i].transform.position, airPrefabs[airIndex].transform.rotation);
            }
        }
    }
}
