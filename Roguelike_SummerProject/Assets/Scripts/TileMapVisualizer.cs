using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapVisualizer : MonoBehaviour
{
    
    [SerializeField] private Tilemap floorTileMap, wallTilemap;
    [SerializeField] private TileBase floorTile, wallTop;


    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPos){
        PaintTiles(floorPos, floorTileMap, floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tileMap, TileBase tile){
        foreach (var position in positions)
        {
            PaintSingleTile(tileMap, tile, position);
        }
    }

    internal void PaintSingleBasicWall(Vector2Int pos)
    {
        PaintSingleTile(wallTilemap, wallTop, pos);
    }

    private void PaintSingleTile(Tilemap tileMap, TileBase tile, Vector2Int position){
        var tilePos = tileMap.WorldToCell((Vector3Int)position);
        tileMap.SetTile(tilePos, tile);
    }

    public void Clear(){
        floorTileMap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }
}
