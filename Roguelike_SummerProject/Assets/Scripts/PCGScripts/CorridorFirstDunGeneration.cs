using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorFirstDunGeneration : SimpleRandoWalkGen
{
    [SerializeField] private int corridorLen = 14, corridorCount = 5;
    [SerializeField] [Range(0.1f,1)] private float roomPercent = .8f;   //percent of rooms we can create

    private Dictionary<Vector2Int, HashSet<Vector2Int>> roomDictionary = new Dictionary<Vector2Int, HashSet<Vector2Int>>();
    private HashSet<Vector2Int> floorPos, corridorPos;

    protected override void RunProceduralGeneration()
    {
        CorridorFirstGen();
    }

    private void CorridorFirstGen(){
        HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPos = new HashSet<Vector2Int>();   //used to keep track of where we can spawn rooms

        CreateCorridors(floorPos, potentialRoomPos);

        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPos);

        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPos);  //contains a list of dead end points
        CreateRoomsAtDeadEnd(deadEnds, roomPositions);  //need roomPositions because we might already have a room being attached
        
        floorPos.UnionWith(roomPositions);

        tileMapVisualizerScript.PaintFloorTiles(floorPos);
        WallGenerator.CreateWalls(floorPos, tileMapVisualizerScript);
    }

    private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomPositions)
    {
        foreach (var pos in deadEnds)
        {
            if(roomPositions.Contains(pos) == false){
                var roomFloor = RunRandomWalk(randomWalkParameters, pos);
                roomPositions.UnionWith(roomFloor);
            }
        }
    }

    private void CreateCorridors(HashSet<Vector2Int> floorPos, HashSet<Vector2Int> potentialRoomPos){
        var currentPos = startPos;  //startPos is inherited
        potentialRoomPos.Add(currentPos);

        for (int i = 0; i < corridorCount; i++)
        {
            var corridor = ProceduralGenAlgos.RandomWalkCorridor(currentPos,corridorLen);
            currentPos = corridor[corridor.Count-1];    //setting currentPos to the last position in the corridor
            potentialRoomPos.Add(currentPos);
            floorPos.UnionWith(corridor);
        }
        corridorPos = new HashSet<Vector2Int>(floorPos);
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPos){
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPos.Count * roomPercent); //gets an int by taking total room count and multiplying by a percent

        List<Vector2Int> roomsToCreate = potentialRoomPos.OrderBy(x => Random.Range(-100,100)).Take(roomToCreateCount).ToList();

        clearRoomData();

        foreach (var roomPos in roomsToCreate)
        {
            var roomFloor = RunRandomWalk(randomWalkParameters, roomPos);

            SaveRoomData(roomPos, roomFloor);

            roomPositions.UnionWith(roomFloor);
        }
        return roomPositions;
    }

    private void SaveRoomData(Vector2Int roomPos, HashSet<Vector2Int> roomFloor)
    {
        roomDictionary[roomPos] = roomFloor;
    }

    private void clearRoomData()
    {
        roomDictionary.Clear();
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions){
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (var pos in floorPositions)
        {
            int neighborsCount = 0; //if this is only 1 then we have found a dead end
            foreach (var dir in Direction2D.cardinalDirList)
            {
                if(floorPositions.Contains(pos + dir)){
                    neighborsCount++;
                }
            }
            if(neighborsCount == 1){
                deadEnds.Add(pos);
            }
        }
        return deadEnds;
    }


}
