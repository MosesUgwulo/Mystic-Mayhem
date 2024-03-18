using System;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Terrain
{
    public class GenerateGrid : MonoBehaviour
    {
        private int[,] _grid;
        public int width = 160;
        public int height = 90;
        public float cellSize = 1.0f;
        public List<GameObject> cellPrefabs;
        public GameObject villagePrefab;
        public GameObject dungeonPrefab;
    
        [Range(0f,1f)]
        public float fireBiome = 0.2f;
    
        [Range(0f,1f)]
        public float earthBiome = 0.4f;
    
        [Range(0f,1f)]
        public float waterBiome = 0.6f;
    
        [Range(0f,1f)]
        public float airBiome = 0.8f;
    
        [Range(0f,10f)]
        public float percentageFilled = 0.8f;
    
        private double _iters = 100;

        public float decay = 0.9990f;
        public float chance = 100f;
        private const int Visited = -1;
        private const int Village = -2;
        private const int Dungeon = -3;

        private GameObject _folder;
        public NavMeshSurface navMeshSurface;

        void Awake()
        {
            _folder = new GameObject("Cells");
            _folder.transform.parent = transform;
            // FloodFill();
            // Generate();
        }

        private void Start()
        {
            Generate();
            navMeshSurface.BuildNavMesh();
        }

        private void Generate()
        {
            foreach (var cell in GameObject.FindGameObjectsWithTag("Cell"))
            {
                Destroy(cell);
            }
        
            FloodFill();
        }
    
        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.G))
            {
                Generate();
            }
        
        }

        void FloodFill()
        {
            _grid = new int[width, height];
            _iters = Math.Floor((width * height) * percentageFilled);
            Debug.Log(_iters);
            var chnc = chance;

            for (int i = 0; i < _iters; i++)
            {
                var tileID = Random.Range(1, cellPrefabs.Count+1);

                float spawnChance = Random.value;
                if (tileID == 1 && spawnChance > fireBiome) continue;
                if (tileID == 2 && spawnChance > earthBiome) continue;
                if (tileID == 3 && spawnChance > waterBiome) continue;
                if (tileID == 4 && spawnChance > airBiome) continue;
            
                var deque = new LinkedList<Vector2Int>();
                deque.AddLast(new Vector2Int(Random.Range(0, width), Random.Range(0, height)));
                while (deque.Count != 0)
                {
                    var pos = deque.First.Value;
                    _grid[pos.x, pos.y] = tileID;
                    deque.RemoveFirst();

                    if (Random.Range(0, 100) <= chnc)
                    {
                        var tileAbovePos = new Vector2Int(pos.x, pos.y + 1);
                        if (IsWithinBound(tileAbovePos))
                        {
                            deque.AddLast(tileAbovePos);
                            _grid[tileAbovePos.x, tileAbovePos.y] = Visited;
                        }

                        var tileRightPos = new Vector2Int(pos.x + 1, pos.y);
                        if (IsWithinBound(tileRightPos))
                        {
                            deque.AddLast(tileRightPos);
                            _grid[tileRightPos.x, tileRightPos.y] = Visited;
                        }

                        var tileBelowPos = new Vector2Int(pos.x, pos.y - 1);
                        if (IsWithinBound(tileBelowPos))
                        {
                            deque.AddLast(tileBelowPos);
                            _grid[tileBelowPos.x, tileBelowPos.y] = Visited;
                        }

                        var tileLeftPos = new Vector2Int(pos.x - 1, pos.y);
                        if (IsWithinBound(tileLeftPos))
                        {
                            deque.AddLast(tileLeftPos);
                            _grid[tileLeftPos.x, tileLeftPos.y] = Visited;
                        }
                    }

                    chnc *= decay;
                }
            }
        
            // Pick 4 random points to be dungeons
            for (int i = 0; i < 4; i++)
            {
                var randDungeon = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
                _grid[randDungeon.x, randDungeon.y] = Dungeon;
            }
        
            var randVillage = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
            _grid[randVillage.x, randVillage.y] = Village;
        
        
            BuildTerrain();

            // TODO: Add check to make sure all biomes exist
        
        }

        bool IsWithinBound(Vector2Int pos)
        {
            return pos.x >= 0 && pos.x < width && pos.y >= 0 && pos.y < height;
        }

        private void BuildTerrain()
        {
            if (_grid == null)
                return;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var tileVal = 0;
                    try
                    {
                        tileVal = _grid[x, y];
                    } 
                    catch (IndexOutOfRangeException)
                    {
                        continue;
                    }
                
                    // var size = Vector3.one * cellSize;

                    if (tileVal == 0) // Empty space
                    {
                        Instantiate(cellPrefabs[tileVal], new Vector3(x * (cellSize), 0, y * (cellSize)), Quaternion.identity, _folder.transform); 
                    }
                    else if (tileVal == Village) // Village space
                    {
                        Instantiate(villagePrefab, new Vector3(x * (cellSize), 0, y * (cellSize)), Quaternion.identity, _folder.transform);
                    }
                    else if (tileVal == Dungeon)
                    {
                        Instantiate(dungeonPrefab, new Vector3(x * (cellSize), 0, y * (cellSize)), Quaternion.identity, _folder.transform);
                    }
                    else // Filled space
                    {
                        Instantiate(cellPrefabs[tileVal-1], new Vector3(x * (cellSize), 0, y * (cellSize)), Quaternion.identity, _folder.transform);
                    }
                }
            }
        
        }
    }
}
