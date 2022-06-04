using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomFirstDunGeneration : SimpleRandoWalkGen
{
    [SerializeField] private int minRoomWidth = 4, minRoomHeight = 4;
    [SerializeField] private int dungeonWidth = 20, dungeonHeight = 20;     //the area that we can split up to make rooms
    [SerializeField] [Range(0,10)] private int offset = 1;        //amount of offset between rooms (so no overlap)
    [SerializeField] private bool randomWalkRooms = false;  //generate square rooms or random walk

    protected override void RunProceduralGeneration()
    {
        CreateRooms();
    }

    private void CreateRooms()
    {
        var roomsList = ProceduralGenAlgos.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPos, new Vector3Int(dungeonWidth, dungeonHeight, 0)), minRoomWidth, minRoomHeight);

        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        floor = CreateSimpleRooms(roomsList);

        tileMapVisualizerScript.PaintFloorTiles(floor);
        WallGenerator.CreateWalls(floor, tileMapVisualizerScript);
    }

    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomsList)
    {
        //if we wanted to procedurally generate decorations, etc. we would have to save each hashset and process further
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        foreach (var room in roomsList)
        {
            for (int col = offset; col < room.size.x - offset; col++)
            {
                for (int row = offset; row < room.size.y - offset; row++)
                {
                    Vector2Int pos = (Vector2Int)room.min + new Vector2Int(col, row);
                    floor.Add(pos);
                }
            }
        }
        return floor;
    }


}
