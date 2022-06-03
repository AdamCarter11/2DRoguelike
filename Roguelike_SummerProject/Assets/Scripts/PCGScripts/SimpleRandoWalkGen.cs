using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleRandoWalkGen : AbstractDungeonGenerator
{
    [SerializeField] protected SimpleRandomWalkSO randomWalkParameters;
    
    protected override void RunProceduralGeneration(){
        HashSet<Vector2Int> floorPos = RunRandomWalk(randomWalkParameters, startPos);
        tileMapVisualizerScript.Clear();
        tileMapVisualizerScript.PaintFloorTiles(floorPos);
        WallGenerator.CreateWalls(floorPos,tileMapVisualizerScript);
    }

    protected HashSet<Vector2Int> RunRandomWalk(SimpleRandomWalkSO para, Vector2Int pos){
        var currPos = pos;
        HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>();
        for (int i = 0; i < para.iterations; i++)
        {
            var path = ProceduralGenAlgos.SimpleRandomWalk(currPos, para.walkLen);
            floorPos.UnionWith(path);   //Copying positions from path without duplicates
            if(para.startRandomlyEachIteration){
                currPos = floorPos.ElementAt(Random.Range(0,floorPos.Count));
            }
        }
        return floorPos;
    }

}
