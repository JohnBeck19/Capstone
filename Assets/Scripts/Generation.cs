using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation : MonoBehaviour
{
    [SerializeField] GameObject[] prefabs;

    [SerializeField] float cutoff = .4f; //cutoff between wall and floor
    [SerializeField] int RoomAmount = 2; // Number of rooms in each direction (grid of rooms)
    [SerializeField] float roomSpacing = 2.5f; // Size of each tile, used to calculate room placement
    [SerializeField] float scale = .1f;
    [SerializeField] float falloffIntensity = 3.0f; //room falloff 
    [SerializeField] float falloffStrength = 3.3f;  //room falloff
    [SerializeField] int roomSizes = 15;    //size of rooms

    private Room[,] rooms;

    // Method to generate the map
    public void GenerateMap()
    {
        // Clear any previously spawned tiles
        ClearMap();

        // Generate grid of rooms
        rooms = new Room[RoomAmount, RoomAmount];
        for (int y = 0; y < RoomAmount; y++)
        {
            for (int x = 0; x < RoomAmount; x++)
            {
                // Create a new GameObject to hold the room component
                GameObject roomObject = new GameObject("Room_" + x + "_" + y);
                Room room = roomObject.AddComponent<Room>();

                room.prefabs = prefabs;
                room.size = roomSizes;
                // Calculate the room's position based on its size and tile size
                float roomOffsetX = x * (room.size * roomSpacing);
                float roomOffsetY = y * (room.size * roomSpacing);
                room.xRoomLocation = roomOffsetX;
                room.yRoomLocation = roomOffsetY;
                room.cutoff = cutoff;
                room.scale = scale;
                
                room.falloffIntensity = falloffIntensity;
                room.falloffStrength = falloffStrength;
                room.GenerateRoom();

                rooms[x, y] = room;
            }
        }
    }

    // Method to clear the previously generated map
    public void ClearMap()
    {
        if (rooms != null)
        {
            foreach (Room room in rooms)
            {
                if (room != null)
                {
                    room.ClearRoom();
                    DestroyImmediate(room.gameObject);
                }
            }
        }

        rooms = null;
    }
}
