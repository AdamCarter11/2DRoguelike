using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapVisualizer : MonoBehaviour
{
    
    [SerializeField] private Tilemap floorTileMap, wallTilemap;
    [SerializeField] private TileBase floorTile, wallTop, wallRight, wallLeft, wallBottom, wallFull, 
        wallInnerCornerDownLeft, wallInnerCornerDownRight, 
        wallDiagonalCornerDownRight, wallDiagonalCornerDownLeft, wallDiagonalCornerUpRight, wallDiagonalCornerUpLeft;


    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPos){
        PaintTiles(floorPos, floorTileMap, floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tileMap, TileBase tile){
        foreach (var position in positions)
        {
            PaintSingleTile(tileMap, tile, position);
        }
    }

    internal void PaintSingleBasicWall(Vector2Int pos, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2); //converts string binary type to int
        TileBase tile = null;
        if(WallTypesHelper.wallTop.Contains(typeAsInt)){
            tile = wallTop;
        }
        else if(WallTypesHelper.wallSideRight.Contains(typeAsInt)){
            tile = wallRight;
        }
        else if(WallTypesHelper.wallSideLeft.Contains(typeAsInt)){
            tile = wallLeft;
        }
        else if(WallTypesHelper.wallBottm.Contains(typeAsInt)){
            tile = wallBottom;
        }
        else if(WallTypesHelper.wallFull.Contains(typeAsInt)){
            tile = wallFull;
        }
        if(tile != null){
            PaintSingleTile(wallTilemap, tile, pos);
        }
        
    }

    private void PaintSingleTile(Tilemap tileMap, TileBase tile, Vector2Int position){
        var tilePos = tileMap.WorldToCell((Vector3Int)position);
        tileMap.SetTile(tilePos, tile);
    }

    public void Clear(){
        floorTileMap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }

    internal void PaintSingleCornerWall(Vector2Int pos, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;

        if(WallTypesHelper.wallInnerCornerDownLeft.Contains(typeAsInt)){
            tile = wallInnerCornerDownLeft;
        }
        else if(WallTypesHelper.wallInnerCornerDownRight.Contains(typeAsInt)){
            tile = wallInnerCornerDownRight;
        }
        else if(WallTypesHelper.wallDiagonalCornerDownLeft.Contains(typeAsInt)){
            tile = wallDiagonalCornerDownLeft;
        }
        else if(WallTypesHelper.wallDiagonalCornerDownRight.Contains(typeAsInt)){
            tile = wallDiagonalCornerDownLeft;
        }
        else if(WallTypesHelper.wallDiagonalCornerUpRight.Contains(typeAsInt)){
            tile = wallDiagonalCornerUpRight;
        }
        else if(WallTypesHelper.wallDiagonalCornerUpLeft.Contains(typeAsInt)){
            tile = wallDiagonalCornerUpLeft;
        }
        else if(WallTypesHelper.wallFullEightDirections.Contains(typeAsInt)){
            tile = wallFull;
        }
        else if(WallTypesHelper.wallBottmEightDirections.Contains(typeAsInt)){
            tile = wallBottom;
        }

        if(tile != null){
            PaintSingleTile(wallTilemap, tile, pos);
        }
    }
}
