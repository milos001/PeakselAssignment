using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestMovement : MonoBehaviour
{
    private float moveSpeed = 15f;
    private bool movingRight;
    private bool movingLeft;
    public bool movingChest;
    private bool touchingRightWall;
    private bool touchingLeftWall;

    private void Update()
    {
        if (movingChest)
        {
            //when the player starts dragging the chest we get the touch position and use the .y from it as our transform.position.y
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            Vector2 newMovePos = new Vector2(touchPos.x, transform.position.y);
            transform.position = newMovePos;
        }
        
        if (movingLeft && !movingRight && !touchingLeftWall)
        {
            //move left when button pressed and not touching wall
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }
        else if (movingRight && !movingLeft && !touchingRightWall)
        {
            //move right when button pressed and not touching wall
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //check to see if chest is touching wall
        if (other.gameObject.CompareTag("RightWall")) touchingRightWall = true;
        if (other.gameObject.CompareTag("LeftWall")) touchingLeftWall = true;
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        //check to see if the chest stopped touching wall
        if (other.gameObject.CompareTag("RightWall")) touchingRightWall = false;
        if (other.gameObject.CompareTag("LeftWall")) touchingLeftWall = false;
    }

    public void MoveLeft()
    {
        //changed by left ui button/indicates when button is pressed
        movingLeft = !movingLeft;
    }
    
    public void MoveRight()
    {
        //changed by right ui button/indicates when button is pressed
        movingRight = !movingRight;
    }

    public void MovingChest()
    {
        //changed by event trigger on chest game object
        movingChest = !movingChest;
    }
}
