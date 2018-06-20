using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallEntity : MonoBehaviour {

    public enum DesiredDir {left = 0, right = 1}; ////Which paddle do I want the player to hit?
    public enum MoveState { idle = 0, throwable = 1, throwing = 2};
    [Header("States")]
    public MoveState moveState;
    public DesiredDir desiredDir;

    [Header("Throw force")]
    public float throwForce;

    [Header("Bouncing")]
    public float bounceForce;
    public float bounceTargetRange;


    void Awake()
    {
        ScoreManager.ResetScore();
    }

    void FixedUpdate()
    {
        InputChecker();
    }

    private void InputChecker() ////Handles the input for the ball
    {
        if (Input.GetButtonDown("Throw") && moveState == MoveState.throwable)
        {
            ThrowToTarget();
            PaddleManager.SetDodge((int)desiredDir);
        }
    }

    private void ThrowToTarget() ////Throws the ball towards the mouse position
    {
        Vector3 _targetPos = GetTargetPosition();
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, 0);
        transform.LookAt(_targetPos);
        rb.AddForce(transform.forward * throwForce);
        moveState = MoveState.throwing;
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
        if(_col.collider.tag == "Paddle") /////Sends the information of the paddle that was hit to be processed
        {
            OnPaddleHit(_col.collider.name);
        }
    }

    private void OnPaddleHit(string _colName) /////Resets the ball's moveState Handles what should happen when what paddle has been hit by the ball
    {
        moveState = MoveState.throwable;


        CheckScore(_colName); ////Checks if score should be added or not
        Bounce(); ////Bounces away
    }

    private void CheckScore(string _colName)
    {
        if (_colName == "LeftPaddle" && desiredDir == DesiredDir.left)
        {
            ScoreManager.AddScore();
            desiredDir = DesiredDir.right;
            DesiredDirectionVisualiser.SwitchDirectionToShow((int)desiredDir);
            PaddleManager.SetDodge((int)desiredDir);
        }

        else if (_colName == "RightPaddle" && desiredDir == DesiredDir.right)
        {
            ScoreManager.AddScore();
            desiredDir = DesiredDir.left;
            DesiredDirectionVisualiser.SwitchDirectionToShow((int)desiredDir);
            PaddleManager.SetDodge((int)desiredDir);
        }

        else
        {
            Debug.Log("Paddle that's not a paddle has been hit. What.");
        }
    }

    private void Bounce() ////Bounces the ball to the left or right
    {
        Vector3 _targetPos = new Vector3(0, Random.Range(0 - bounceTargetRange / 2, bounceTargetRange / 2), 0);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, 0);

        ParticlePooler.instance.SpawnFromPoolHitFX(transform);
        transform.LookAt(_targetPos);
        rb.AddForce(transform.forward * bounceForce);

        
    }

}
