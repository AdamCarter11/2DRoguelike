using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Vector3 lastDirection = Vector3.zero;
    bool moveDone = true;   
    List<WorldTile> reachedPathTiles = new List<WorldTile>(); 
    List<WorldTile> path2 = new List<WorldTile>();

    	
    public Transform movePoint;
    public LayerMask stopMovementMask;
    public float moveSpeed;
    private Vector2 movement;

    public CreateGrid gridPath;

    [SerializeField] private CreateGrid createGridScript;

    [SerializeField] private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;

        //testing code
        Vector3 testVec = new Vector3(.5f,0,0);
        //print(testVec.Equals(Vector3.right));
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)){
            gridPath.FindPath(transform.position, target.transform.position);
            path2 = gridPath.path;
            moveDone = false;
            print(path2.Count);
        }
        MovementPerformed();
    }

    void MovementPerformed() {
        SetMovementVector();
        
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.fixedDeltaTime);
        //transform.position = movePoint.position;

        if (Vector3.Distance(transform.position, movePoint.position) <= .001f)
        {
            if (Mathf.Abs(movement.x) == 1f)
            {
                // we add 0.5f to 'y' component of the 'position'
                // to account the bottom pivot point of the sprite
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(movement.x, 0f , 0f), .1f, stopMovementMask))
                {
                    movePoint.position += new Vector3(movement.x, 0f, 0f);
                    //print("X: " + movePoint.position);
                }
            } 
            else if (Mathf.Abs(movement.y) == 1f)
            {
                // we add 0.5f to 'y' component of the 'position'
                // to account the bottom pivot point of the sprite
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0, movement.y, 0f), .1f, stopMovementMask))
                {
                    movePoint.position += new Vector3(0f, movement.y, 0f); 
                    //print("Y: " + movePoint.position);
                }
            }
        }
        //print(Vector3.Distance(transform.position, movePoint.position));
    }

    void SetMovementVector()
    {
        if (path2 != null)
        {
            if (path2.Count > 0)
            {
                if (!moveDone)
                {
                    for (int i = 0; i < path2.Count; i++)
                    {
                        if (reachedPathTiles.Contains(path2[i])) continue;
                        else reachedPathTiles.Add(path2[i]); break;
                    }
                    WorldTile wt = reachedPathTiles[reachedPathTiles.Count - 1];
                    lastDirection = new Vector3(Mathf.Ceil(wt.cellX - transform.position.x), Mathf.Ceil(wt.cellY - transform.position.y), 0);
                    //print(lastDirection);
                    if (lastDirection.y >= 1) movement.y = 1;
                    if (lastDirection.y <= -1) movement.y = -1;
                    if (lastDirection.x <= -1) movement.x = -1;
                    if (lastDirection.x >= 1) movement.x = 1;
                    moveDone = true;

                    /*
                    //origionally replaced lines 91-94
                    if (lastDirection.Equals(Vector3.up)) movement.y = 1;
                    if (lastDirection.Equals(Vector3.down)) movement.y = -1;
                    if (lastDirection.Equals(Vector3.left)) movement.x = -1;
                    if (lastDirection.Equals(Vector3.right)) movement.x = 1;
                    */
                }
                else
                {
                    movement = Vector2.zero;
                    if (Vector3.Distance(transform.position, movePoint.position) <= .1f)
                        moveDone = false;
                }
            }
        }
    }
    
}
