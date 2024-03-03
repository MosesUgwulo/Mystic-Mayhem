using UnityEngine;

namespace Terrain
{
    public class SpawnAirBiome : MonoBehaviour
    {
        public GameObject[] rockPrefab;
        public GameObject[] spawnPoint;
        void Start()
        {
            foreach (var point in spawnPoint)
            {
                point.transform.localPosition = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
            }
        
            for (int i = 0; i < spawnPoint.Length; i++)
            {
                int rockIndex = Random.Range(0, rockPrefab.Length);
                Instantiate(rockPrefab[rockIndex], spawnPoint[i].transform.position, rockPrefab[rockIndex].transform.rotation);
            }
        }
    }
}
