using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBall : MonoBehaviour {

	
    public enum MoveState { idle = 0, throwable = 1};
    [Header("Movement state")]
    public MoveState moveState;

    [Header("Throw force")]
    public float throwForce;

    [Header("Bouncing")]
    public float bounceForce;
    public float bounceTargetRange;
    public GameObject bounceEffect;

    void FixedUpdate()
    {
        InputChecker();
    }

    private void InputChecker() ////Handles the input for the ball
    {
        if (Input.GetButtonDown("Throw") && moveState == MoveState.throwable)
        {
            ThrowToTarget();
        }
    }

    private void ThrowToTarget() ////Throws the ball towards the mouse position
    {
        Vector3 _targetPos = GetTargetPosition();
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, 0);
        transform.LookAt(_targetPos);
        rb.AddForce(transform.forward * throwForce);
    }

    private Vector3 GetTargetPosition() ///Returns the mouse position in worldspace coörds (but with Z set to 0 because this is a 2D-ish thingamajigg)
    {
        Camera _cam = Camera.main;
        Vector3 _targetPosition = Input.mousePosition;
        _targetPosition = _cam.ScreenToWorldPoint(_targetPosition);
        _targetPosition.z = 0;
        return _targetPosition;
    }

    void OnCollisionEnter(Collision _col)
    {
        if(_col.collider.tag == "Paddle")
        {
            OnPaddleHit(_col.collider.name); /////Sends the information of the paddle that was hit to be processed
        }
    }

    private void OnPaddleHit(string _colName) /////Handles what should happen when what paddle has been hit by the ball
    {
        moveState = MoveState.throwable;
        Bounce(_colName);
    }

    private void Bounce(string _colName) ////Bounces the ball to the left or right depending on which paddle it has hit
    {
        Vector3 _targetPos = new Vector3(Random.Range(0 - bounceTargetRange / 2, bounceTargetRange / 2), 0, 0);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, 0);

        Instantiate(bounceEffect, transform.position, Quaternion.identity);

        transform.LookAt(_targetPos);
        rb.AddForce(transform.forward * bounceForce);

        if (_colName == "_StartGamePaddle")
        {
            ////Load the game scene
            MainMenuManager.LoadLevel(1, 0.05f);
        }

        else if (_colName == "_QuitGamePaddle")
        {
            MainMenuManager.QuitGame("y");
        }

        else
        {
            Debug.Log("Paddle that's not a paddle has been hit. What.");
        }
    }
}
