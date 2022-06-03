using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}

public static class Direction2D{
    public static List<Vector2Int> cardinalDirList = new List<Vector2Int>{
        new Vector2Int(0,1), //UP
        new Vector2Int(1,0), //RIGHT
        new Vector2Int(0,-1), //DOWN
        new Vector2Int(-1,0), //LEFT
    };
    public static Vector2Int GetRandoCardinalDir(){
        return cardinalDirList[Random.Range(0,cardinalDirList.Count)];
    }
}