using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleAI : MonoBehaviour {

    [Header("Up & down movement")]
    public float pingpongrange;
    public float updownSpeed;

    
    public enum MoveState { idle = 0, updown = 1, dodging = 2};
    [Header("Movement state")]
    public MoveState moveState;

    [Header("Dodging movement")]
    public float dodgeSpeed;
    public Vector3 predictedPosition;

    public void TryDodge(Vector3 _predictedPos) ////Can be called by the ball to allow the paddle to start dodging when the player has attempted a try
    {
        predictedPosition = _predictedPos;
        StartCoroutine(MovementSequence());
    }

    IEnumerator MovementSequence() /////Checks for movement state and applies it 
    {   
        Vector3 tempPos = predictedPosition;
        tempPos.x = transform.position.x;
        tempPos.z = transform.position.z;
        Debug.Log("Performing DodgeSequence");

        while (moveState == MoveState.dodging)
        { 
            transform.position = Vector3.MoveTowards(transform.position, tempPos, -1f * dodgeSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        while (moveState == MoveState.updown && transform.position != new Vector3(transform.position.x, Mathf.PingPong(Time.time * updownSpeed, pingpongrange) - pingpongrange / 2, transform.position.z))
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, Mathf.PingPong(Time.time * updownSpeed, pingpongrange) - pingpongrange / 2, transform.position.z), 1 * dodgeSpeed * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }
            
        while (moveState == MoveState.updown)
        {
            transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time * updownSpeed, pingpongrange) - pingpongrange / 2, transform.position.z);
            yield return new WaitForEndOfFrame();
        }
        
    }

}
