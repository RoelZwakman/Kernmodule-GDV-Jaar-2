using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleAI : MonoBehaviour {

    [Header("Up & down movement")]
    public float updownSpeed;
    public Vector3 upperLimit;
    public Vector3 lowerLimit;
    public enum Dir {up = 0, down = 1 };
    Dir direction;

    
    public enum MoveState { idle = 0, updown = 1, dodging = 2};
    [Header("Movement state")]
    public MoveState moveState;

    [Header("Dodging movement")]
    public float dodgeSpeed;
    public Vector3 predictedPosition;

    

    private void FixedUpdate()
    {
        SetupMoveLimits();
        MovementBehaviour();
    }

    private void SetupMoveLimits() ///sets the movement's upper and lower limits
    {
        Vector3 tempPos = transform.position;
        tempPos.y = upperLimit.y;
        upperLimit = tempPos;
        tempPos.y = lowerLimit.y;
        lowerLimit = tempPos;
    }

    public void TryDodge(Vector3 _predictedPos) ////Can be called by the ball to allow the paddle to start dodging when the player has attempted a try
    {
        predictedPosition = _predictedPos;
        moveState = MoveState.dodging;
        if (direction == Dir.up)
        {
            direction = Dir.down;
        }
        else
        {
            direction = Dir.up;
        }
    }

    private void MovementBehaviour()
    {
        switch (moveState)
        {
            case MoveState.updown:

                MoveUpDown(updownSpeed);

                break;
            case MoveState.dodging:
                    
                MoveUpDown(dodgeSpeed);
                break;
        }
    }

    private void MoveUpDown(float speed)
    {
        switch (direction)
        {
            case Dir.up:

                if(Vector3.Distance(transform.position, upperLimit) < 0.1f)
                {
                    direction = Dir.down;
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, upperLimit, Time.deltaTime * speed);
                }

                break;

            case Dir.down:

                if (Vector3.Distance(transform.position, lowerLimit) < 0.1f)
                {
                    direction = Dir.up;
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, lowerLimit, Time.deltaTime * speed);
                }

                break;
            
        }
    }



   

}
