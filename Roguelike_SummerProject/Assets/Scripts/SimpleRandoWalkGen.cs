using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleRandoWalkGen : AbstractDungeonGenerator
{
    [SerializeField] private SimpleRandomWalkSO randomWalkParameters;
    
    protected override void RunProceduralGeneration(){
        HashSet<Vector2Int> floorPos = RunRandomWalk();
        tileMapVisualizerScript.Clear();
        tileMapVisualizerScript.PaintFloorTiles(floorPos);
        WallGenerator.CreateWalls(floorPos,tileMapVisualizerScript);
    }

    protected HashSet<Vector2Int> RunRandomWalk(){
        var currPos = startPos;
        HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>();
        for (int i = 0; i < randomWalkParameters.iterations; i++)
        {
            var path = ProceduralGenAlgos.SimpleRandomWalk(currPos, randomWalkParameters.walkLen);
            floorPos.UnionWith(path);   //Copying positions from path without duplicates
            if(randomWalkParameters.startRandomlyEachIteration){
                currPos = floorPos.ElementAt(Random.Range(0,floorPos.Count));
            }
        }
        return floorPos;
    }

}
