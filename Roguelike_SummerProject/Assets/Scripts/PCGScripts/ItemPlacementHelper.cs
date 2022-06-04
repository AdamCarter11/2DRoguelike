using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemPlacementHelper
{
    //work in progress
    /*
    Dictionary<PlacementType, HashSet<Vector2Int>> tileByType = new Dictionary<PlacementType, HashSet<Vector2Int>>();
    HashSet<Vector2Int> roomFloorNoCorridor;

    public ItemPlacementHelper(HashSet<Vector2Int> roomFloor, HashSet<Vector2Int> roomFloorNoCorridor){
        Graph graph = new Graph(roomFloor);
        this.roomFloorNoCorridor = roomFloorNoCorridor;
        foreach (var pos in roomFloorNoCorridor)
        {
            int neighborsCount8Dir = graph.GetNeighbors8Dir(pos).Count;
            PlacementType type = neighborsCount8Dir < 8 ? PlacementType.NearWall : PlacementType.OpenSpace;

            if(tileByType.ContainsKey(type) == false){
                tileByType[type] = new HashSet<Vector2Int>();
            }
            if(type == PlacementType.NearWall && graph.GetNeighbors4Dir(pos).Count){
                continue;
            }
            tileByType[type].Add(pos);
        }
    }
    */
}

public enum PlacementType{
    OpenSpace,
    NearWall
}