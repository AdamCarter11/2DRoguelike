using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Vector3 lastDirection = Vector3.zero;
    bool moveDone = false;   
    List<WorldTile> reachedPathTiles = new List<WorldTile>(); 
    List<WorldTile> path2 = new List<WorldTile>();

    	
    public Transform movePoint;
    public LayerMask stopMovementMask;
    public float moveSpeed;
    private Vector2 movement;

    public CreateGrid gridPath;

    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)){
            print(gridPath.path.Count);
            path2 = gridPath.path;
        }
        MovementPerformed();
    }

    void MovementPerformed() {
        SetMovementVector();
        
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.fixedDeltaTime);
 
        if (Vector3.Distance(transform.position, movePoint.position) <= .001f)
        {
            if (Mathf.Abs(movement.x) == 1f)
            {
                // we add 0.5f to 'y' component of the 'position'
                // to account the bottom pivot point of the sprite
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(movement.x, 0.5f, 0f), .2f, stopMovementMask))
                {
                    movePoint.position += new Vector3(movement.x, 0f, 0f);
                }
            } 
            else if (Mathf.Abs(movement.y) == 1f)
            {
                // we add 0.5f to 'y' component of the 'position'
                // to account the bottom pivot point of the sprite
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0, movement.y + 0.5f, 0f), .2f, stopMovementMask))
                {
                    movePoint.position += new Vector3(0f, movement.y, 0f); 
                }
            }
        }
        
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
                    print(lastDirection);
                    if (lastDirection.Equals(Vector3.up)) movement.y = 1;
                    if (lastDirection.Equals(Vector3.down)) movement.y = -1;
                    if (lastDirection.Equals(Vector3.left)) movement.x = -1;
                    if (lastDirection.Equals(Vector3.right)) movement.x = 1;
                    moveDone = true;
                    
                }
                else
                {
                    movement = Vector2.zero;
                    if (Vector3.Distance(transform.position, movePoint.position) <= .001f)
                        moveDone = false;
                }
            }
        }
    }
    
}
