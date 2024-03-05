using UnityEngine;

namespace Terrain
{
    public class SpawnWaterBiome : MonoBehaviour
    {
        public GameObject[] waterPrefabs;
        public GameObject[] spawnPoints;
        void Start()
        {
            foreach (var point in spawnPoints)
            {
                point.transform.localPosition = new Vector3(Random.Range(-0.4f, 0.4f), 0, Random.Range(-0.4f, 0.4f));
            }
        
            for (int i = 0; i < spawnPoints.Length; i++)
            { 
                int waterIndex = Random.Range(0, waterPrefabs.Length); 
                Instantiate(waterPrefabs[waterIndex], spawnPoints[i].transform.position, waterPrefabs[waterIndex].transform.rotation);
            }
        }
    }
}
