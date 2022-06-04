using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int> floorPos, TileMapVisualizer tileMapVisualizerScript)
    {
        var basicWallPos = FindWallsInDirections(floorPos, Direction2D.cardinalDirList);
        var cornerWallPos = FindWallsInDirections(floorPos, Direction2D.diagonalDirList);

        CreateBasicWall(tileMapVisualizerScript, basicWallPos, floorPos);
        CreateCornerWalls(tileMapVisualizerScript, cornerWallPos, floorPos);
    }

    private static void CreateCornerWalls(TileMapVisualizer tileMapVisualizerScript, HashSet<Vector2Int> cornerWallPos, HashSet<Vector2Int> floorPos)
    {
        foreach (var pos in cornerWallPos)
        {
            string neighborsBinaryType = "";
            foreach (var dir in Direction2D.eightDirList)
            { 
                var neighborPos = pos + dir;
                if(floorPos.Contains(neighborPos)){
                    neighborsBinaryType += "1";
                }
                else{
                    neighborsBinaryType += "0";
                }
            }
            tileMapVisualizerScript.PaintSingleCornerWall(pos, neighborsBinaryType);
        }
    }

    private static void CreateBasicWall(TileMapVisualizer tileMapVisualizerScript, HashSet<Vector2Int> basicWallPos, HashSet<Vector2Int> floorPos)
    {
        foreach (var pos in basicWallPos)
        {
            string neighborsBinaryType = "";
            foreach (var dir in Direction2D.cardinalDirList)
            {
                var neighborPos = pos + dir;
                if(floorPos.Contains(neighborPos)){
                    neighborsBinaryType += "1";
                }
                else{
                    neighborsBinaryType += "0";
                }
            }
            tileMapVisualizerScript.PaintSingleBasicWall(pos, neighborsBinaryType);
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
