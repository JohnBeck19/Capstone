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
    public float xRoomLocation = .1f;
    public float yRoomLocation = .1f;

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
        //float[,] falloffmap = new float[size, size];
        //for (int y = 0; y < size; y++)
        //{
        //    for (int x = 0; x < size; x++)
        //    {
        //        float xv = x / (float)size * 2 - 1;
        //        float yv = y / (float)size * 2 - 1;
        //        float v = Mathf.Max(Mathf.Abs(xv), Mathf.Abs(yv));
        //        falloffmap[x, y] = Mathf.Pow(v, falloffIntensity) / (Mathf.Pow(v, falloffIntensity) + Mathf.Pow(falloffStrength - falloffStrength * v, falloffIntensity));
        //    }
        //}


        float[,] falloffmap = new float[size, size];
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float xv = x / (float)(size - 1);
                float yv = y / (float)(size - 1);
                float distanceX = Mathf.Abs(xv - 0.5f) * 2; 
                float distanceY = Mathf.Abs(yv - 0.5f) * 2; 
                float falloff = Mathf.Pow(distanceX * distanceY, falloffIntensity);
                falloffmap[x, y] = falloff / (falloff + Mathf.Pow(falloffStrength, falloffIntensity));
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
                cell.cellSize = prefabs[2].transform.localScale.x;
                cell.isWall = noiseValue < cutoff;
                grid[x, y] = cell;
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

    public void SpawnTiles()
    {
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                Cell cell = grid[x, y];
                GameObject spawnedTile = Instantiate(prefabs[3], new Vector3(x * cell.cellSize + xRoomLocation,0, y * cell.cellSize + yRoomLocation), Quaternion.identity);
                spawnedTiles.Add(spawnedTile);
                if (cell.isWall)
                {       //2 5                                                   //for normal walls change 0 to 5             Quaternion.Euler(0,180,0) for wall        
                    spawnedTile = Instantiate(prefabs[4], new Vector3(x * cell.cellSize + xRoomLocation, 0, y * cell.cellSize + yRoomLocation),Quaternion.Euler(0,Random.Range(0,360),0));
                    spawnedTiles.Add(spawnedTile);
                }
              

            }
        }
    }
}
