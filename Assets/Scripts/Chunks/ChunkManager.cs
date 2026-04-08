using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    [Header("Chunk Settings")]
    [SerializeField] private int chunkSize = 50;
    [SerializeField] private int viewDistance = 2; // chunks around player

    [Header("Planet Spawning")]
    [SerializeField] private int minPlanetsPerChunk = 1;
    [SerializeField] private int maxPlanetsPerChunk = 4;

    private Dictionary<Vector2Int, GameObject> activeChunks = new();
    private Transform player;
    private int globalSeed;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        globalSeed = Random.Range(0, 999999); // unique per playthrough
    }

    void Update()
    {
        UpdateChunks();
    }

    void UpdateChunks()
    {
        Vector2Int playerChunk = WorldToChunkCoord(player.position);

        // Load nearby chunks
        for (int x = -viewDistance; x <= viewDistance; x++)
        {
            for (int y = -viewDistance; y <= viewDistance; y++)
            {
                Vector2Int coord = new Vector2Int(playerChunk.x + x, playerChunk.y + y);
                if (!activeChunks.ContainsKey(coord))
                    LoadChunk(coord);
            }
        }

        // Unload distant chunks
        List<Vector2Int> toRemove = new();
        foreach (var chunk in activeChunks)
        {
            if (Vector2Int.Distance(chunk.Key, playerChunk) > viewDistance + 1)
                toRemove.Add(chunk.Key);
        }

        foreach (var coord in toRemove)
            UnloadChunk(coord);
    }

    void LoadChunk(Vector2Int coord)
    {
        GameObject chunk = new GameObject($"Chunk {coord}");
        chunk.transform.position = new Vector3(coord.x * chunkSize, coord.y * chunkSize, 0);
        activeChunks[coord] = chunk;

        SpawnPlanetsInChunk(coord, chunk.transform);
    }

    void UnloadChunk(Vector2Int coord)
    {
        if (activeChunks.TryGetValue(coord, out GameObject chunk))
        {
            // Return planets to pool before destroying chunk
            foreach (Transform child in chunk.transform)
                PlanetsPool.Instance.ReturnToPool(child.gameObject);

            Destroy(chunk);
            activeChunks.Remove(coord);
        }
    }

    void SpawnPlanetsInChunk(Vector2Int coord, Transform parent)
    {
        // Seed is deterministic per chunk + playthrough — same chunk = same planets
        int chunkSeed = globalSeed + coord.x * 1000 + coord.y;
        Random.State previousState = Random.state;
        Random.InitState(chunkSeed);

        int planetCount = Random.Range(minPlanetsPerChunk, maxPlanetsPerChunk + 1);

        for (int i = 0; i < planetCount; i++)
        {
            float x = Random.Range(-chunkSize / 2f, chunkSize / 2f);
            float y = Random.Range(-chunkSize / 2f, chunkSize / 2f);
            Vector3 spawnPos = parent.position + new Vector3(x, y, 0);

            GameObject planet = PlanetsPool.Instance.GetFromPool();
            planet.transform.position = spawnPos;
            planet.transform.SetParent(parent);

            // Randomize planet appearance
            planet.GetComponent<PlanetData>().Randomize(chunkSeed + i);
        }

        Random.state = previousState; // restore global random state
    }

    Vector2Int WorldToChunkCoord(Vector3 worldPos)
    {
        return new Vector2Int(
            Mathf.FloorToInt(worldPos.x / chunkSize),
            Mathf.FloorToInt(worldPos.y / chunkSize)
        );
    }
}