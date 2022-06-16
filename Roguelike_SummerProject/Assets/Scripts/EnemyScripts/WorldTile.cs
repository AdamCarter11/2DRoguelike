using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldTile : MonoBehaviour
{
    public int gCost;
    public int hCost;
    public int gridX, gridY, cellX, cellY;
    public bool walkable = true;
    public List<WorldTile> myNeighbours;
    public WorldTile parent;

    [SerializeField] private GameObject target;
    [SerializeField] private Color[] states;
    private TMPro.TMP_Text txt;
    private SpriteRenderer graphic;
    public enum NodeState {
        Default,
        OnPath,
        Next
    }

    //color of path
    private void Start() {
        graphic = GetComponent<SpriteRenderer>();
        txt = GetComponentInChildren<TMPro.TMP_Text>();
        txt.text = this.gameObject.name.Substring(name.IndexOf("_") + 1);
    }
    public void SetColor(NodeState state){
        graphic.color = states[(int)state];
    }
    public static void ResetColor(){
        var arr = GameObject.FindObjectsOfType<WorldTile>();
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i].SetColor(NodeState.Default);
        }
    }


    public WorldTile(bool _walkable, int _gridX, int _gridY)
    {
        walkable = _walkable;
        gridX = _gridX;
        gridY = _gridY;
    }
 
    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
}
