using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//reference: https://www.youtube.com/watch?v=-QOCX6SVFsk&list=PLcRSafycjWFenI87z7uZHFv6cUG2Tzu9v
public static class ProceduralGenAlgos
{
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPos, int walkLen){
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        path.Add(startPos);
        var prevPos = startPos;
        for (int i = 0; i < walkLen; i++)
        {
            var newPos = prevPos + Direction2D.GetRandoCardinalDir();
            path.Add(newPos);
            prevPos = newPos;
        }
        return path;
    }

    //list because we want to keep track of last item added to keep corridors connected linearly
    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPos, int corridorLen){
        List<Vector2Int> corridor = new List<Vector2Int>();
        var dir = Direction2D.GetRandoCardinalDir();
        var currentPos = startPos;
        corridor.Add(currentPos);

        for (int i = 0; i < corridorLen; i++)
        {
            currentPos += dir;
            corridor.Add(currentPos);
        }
        return corridor;
    }

    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight){
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomsList = new List<BoundsInt>();
        roomsQueue.Enqueue(spaceToSplit);
        while(roomsQueue.Count>0){
            var room = roomsQueue.Dequeue();
            if(room.size.y >= minHeight && room.size.x >= minWidth){
                //room is big enough to split or add to queue

                //the if statement is used to make it slightly more random instead of always checking one first
                if(Random.value < .5f){
                    //50% change to split horizontally or 50% vertically
                    if(room.size.y >= minHeight*2){
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }
                    else if(room.size.x >= minWidth * 2){
                        SplitVertically(minWidth, roomsQueue, room);
                    }
                    else{
                        //area cant be split but can contain room
                        roomsList.Add(room);
                    }
                }
                else{
                    if(room.size.x >= minWidth * 2){
                        SplitVertically(minWidth, roomsQueue, room);
                    }
                    else if(room.size.y >= minHeight*2){
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }
                    else{
                        //area cant be split but can contain room
                        roomsList.Add(room);
                    }
                }
            }
        }
        return roomsList;
    }

    private static void SplitVertically(int minWidth,  Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        //var xSplit = Random.Range(minWidth,room.size.x-minWidth);     //no wasted space
        var xSplit = Random.Range(1,room.size.x);   //starting from 1 and not going all the way to the end so it doesn't split too early/late
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z), 
            new Vector3Int(room.size.x-xSplit, room.size.y, room.size.z));
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

    private static void SplitHorizontally(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        //var ySplit = Random.Range(minHeight,room.size.y-minHeight);      //no wasted space
        var ySplit = Random.Range(1,room.size.y);   //(minHeight, room.size.y - minHeight) used to create a gridline structure
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z), 
            new Vector3Int(room.size.x, room.size.y-ySplit, room.size.z));
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }
}

public static class Direction2D{
    public static List<Vector2Int> cardinalDirList = new List<Vector2Int>{
        new Vector2Int(0,1), //UP
        new Vector2Int(1,0), //RIGHT
        new Vector2Int(0,-1), //DOWN
        new Vector2Int(-1,0) //LEFT
    };

    public static List<Vector2Int> diagonalDirList = new List<Vector2Int>{
        new Vector2Int(1,1), //UP RIGHT
        new Vector2Int(1,-1), //DOWN RIGHT
        new Vector2Int(-1,-1), //DOWN LEFT
        new Vector2Int(-1,1) //UP LEFT
    };

    public static List<Vector2Int> eightDirList = new List<Vector2Int>{
        new Vector2Int(0,1), //UP
        new Vector2Int(1,1), //UP RIGHT
        new Vector2Int(1,0), //RIGHT
        new Vector2Int(1,-1), //DOWN RIGHT
        new Vector2Int(0,-1), //DOWN
        new Vector2Int(-1,-1), //DOWN LEFT
        new Vector2Int(-1,0), //LEFT
        new Vector2Int(-1,1) //UP LEFT
    };

    public static Vector2Int GetRandoCardinalDir(){
        return cardinalDirList[Random.Range(0,cardinalDirList.Count)];
    }
}