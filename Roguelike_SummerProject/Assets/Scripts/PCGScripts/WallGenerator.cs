using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int> floorPos, TileMapVisualizer tileMapVisualizerScript){
        var basicWallPos = FindWallsInDirections(floorPos, Direction2D.cardinalDirList);
        foreach (var pos in basicWallPos)
        {
            tileMapVisualizerScript.PaintSingleBasicWall(pos);
        }
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPos, List<Vector2Int> dirList)
    {
        HashSet<Vector2Int> wallPos = new HashSet<Vector2Int>();
        foreach (var pos in floorPos)
        {
            foreach (var direction in dirList)
            {
                var neighborPos = pos + direction;
                if(floorPos.Contains(neighborPos) == false){
                    wallPos.Add(neighborPos);
                }
            }
        }
        return wallPos;
    }


}
