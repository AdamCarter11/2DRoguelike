using System.Collections;
using System.Collections.Generic;
using  System.Linq;
using UnityEngine;


public class Graph
{
    private static List<Vector2Int> neighbors4Dir = new List<Vector2Int>{
        new Vector2Int(0,1),
        new Vector2Int(1,0),
        new Vector2Int(0,-1),
        new Vector2Int(-1,0)
    };
    private static List<Vector2Int> neighbors8Dir = new List<Vector2Int>{
        new Vector2Int(0,1),
        new Vector2Int(1,0),
        new Vector2Int(0,-1),
        new Vector2Int(-1,0),
        new Vector2Int(1,1),
        new Vector2Int(1,-1),
        new Vector2Int(-1,1),
        new Vector2Int(-1,-1),
    };

    List<Vector2Int> graph;

    public Graph(IEnumerable<Vector2Int> vertices){
        graph = new List<Vector2Int>(vertices);
    }

    public List<Vector2Int> GetNeighbors4Dir(Vector2Int startPos){
        return GetNeighbors(startPos, neighbors4Dir);
    }
    public List<Vector2Int> GetNeighbors8Dir(Vector2Int startPos){
        return GetNeighbors(startPos, neighbors4Dir);
    }

    private List<Vector2Int> GetNeighbors(Vector2Int startPos, List<Vector2Int> neighborsOffsetList){
        List<Vector2Int> neighbors = new List<Vector2Int>();
        foreach (var neighborDir in neighborsOffsetList)
        {
            Vector2Int potentialNeighbor = startPos + neighborDir;
            if(graph.Contains(potentialNeighbor)){
                neighbors.Add(potentialNeighbor);
            }
        }
        return neighbors;
    }
}
