using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /*
    private bool isMoving;
    private Vector3 originPos, targetPos;
    [SerializeField] private float timeToMove = 0.2f;   //.2 is 1/5 a second to move from one point to another

    void Update()
    {
        Movement();
    }
 
    private void Movement(){
        if(Input.GetKey(KeyCode.W) && !isMoving){
           StartCoroutine(MovePlayer(Vector3.up));
        }
        if(Input.GetKey(KeyCode.A) && !isMoving){
            StartCoroutine(MovePlayer(Vector3.left));
        }
        if(Input.GetKey(KeyCode.S) && !isMoving){
            StartCoroutine(MovePlayer(Vector3.down));
        }
        if(Input.GetKey(KeyCode.D) && !isMoving){
            StartCoroutine(MovePlayer(Vector3.right));
        }
    }

    private IEnumerator MovePlayer(Vector3 dir){
        isMoving = true;

        float elapsedTime = 0;
        originPos = transform.position;
        targetPos = originPos + dir;

        while(elapsedTime < timeToMove){
            transform.position = Vector3.Lerp(originPos, targetPos, (elapsedTime/timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;

        isMoving = false;
    }
    */

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Transform movePoint;
    [SerializeField] private LayerMask whatStopsMovement;

    private void Start() {
        movePoint.parent = null;
    }
    private void Update() {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        if(Vector3.Distance(transform.position, movePoint.position) <= .05f){
            if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f){
                if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f,0f), .2f, whatStopsMovement)){
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                } 
            }
            else if(Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f){
                if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, whatStopsMovement)){
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                }
            }
        }
    }
}
