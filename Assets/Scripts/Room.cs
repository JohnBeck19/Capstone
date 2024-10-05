using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject[] prefabs;

    public float cutoff = .25f;
    public int size = 15;
    public float scale = .1f;
    public float falloffIntensity = 4f;
    public float falloffStrength = 4.4f;
    public float xRoomLocation = 1;
    public float yRoomLocation = 1;

    private Cell[,] grid;
    private List<GameObject> spawnedTiles = new List<GameObject>();
    // Method to generate the map
    public void GenerateRoom()
    {
        // Clear any previously spawned tiles
        ClearRoom();

        // Generate Perlin noise map
        float[,] noiseMap = new float[size, size];
        float xOffset = Random.Range(-10000f, 10000f);
        float yOffset = Random.Range(-10000f, 10000f);
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float noiseValue = Mathf.PerlinNoise(x * scale * xOffset, y * scale * yOffset);
                noiseMap[x, y] = noiseValue;
            }
        }

        // Generate falloff map
        float[,] falloffmap = new float[size, size];
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float xv = x / (float)size * 2 - 1;
                float yv = y / (float)size * 2 - 1;
                float v = Mathf.Max(Mathf.Abs(xv), Mathf.Abs(yv));
                falloffmap[x, y] = Mathf.Pow(v, falloffIntensity) / (Mathf.Pow(v, falloffIntensity) + Mathf.Pow(falloffStrength - falloffStrength * v, falloffIntensity));
            }
        }

        // Generate grid
        grid = new Cell[size, size];
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                Cell cell = new Cell();
                float noiseValue = noiseMap[x, y];
                noiseValue -= falloffmap[x, y];
                cell.cellSize = 2.5f;
                cell.isWall = noiseValue < cutoff;
                grid[x, y] = cell;
            }
        }

        // Spawn tiles
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                Cell cell = grid[x, y];
                GameObject tilePrefab = cell.isWall ? prefabs[2] : prefabs[1];
                GameObject spawnedTile = Instantiate(tilePrefab, new Vector3(x * cell.cellSize + xRoomLocation, cell.isWall ? 2.5f : 0, y * cell.cellSize + yRoomLocation), Quaternion.identity);
                spawnedTiles.Add(spawnedTile);  // Keep track of spawned tiles for clearing later
            }
        }
    }

    // Method to clear the previously generated map
    public void ClearRoom()
    {
        foreach (GameObject tile in spawnedTiles)
        {
            DestroyImmediate(tile);  // Use DestroyImmediate for Editor mode clearing
        }
        spawnedTiles.Clear();  // Clear the list of spawned tiles
    }
}
