using UnityEngine;

namespace Terrain
{
    public class SpawnEarthBiome : MonoBehaviour
    {
        public GameObject[] earthPrefabs;
        public GameObject[] spawnPoints;
        void Start()
        {
            foreach (var point in spawnPoints)
            {
                point.transform.localPosition = new Vector3(Random.Range(-0.4f, 0.4f), 0, Random.Range(-0.4f, 0.4f));
            }
        
            for (int i = 0; i < spawnPoints.Length; i++)
            { 
                int earthIndex = Random.Range(0, earthPrefabs.Length); 
                Instantiate(earthPrefabs[earthIndex], spawnPoints[i].transform.position, earthPrefabs[earthIndex].transform.rotation);
            }
        }
    }
}
