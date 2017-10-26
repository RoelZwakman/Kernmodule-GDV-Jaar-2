using UnityEngine;

public class PaddleManager : MonoBehaviour {

    public static PaddleManager instance;

    public GameObject ball;
    public PaddleAI[] paddles; /////Left paddle should be put at index 0, and right paddle should be put at index 1

    void Awake(){
        instance = this;
    }

	public static void SetDodge(int dodgyPaddle){ ////Sets the paddle that should be dodging to the MoveState dodging, and the other one to stop dodging

        if(dodgyPaddle == 0){ 
            instance.paddles[0].moveState = PaddleAI.MoveState.dodging;
            instance.paddles[0].TryDodge(instance.ball.transform.position);
            instance.paddles[1].moveState = PaddleAI.MoveState.updown;
        }

        else if (dodgyPaddle == 1){
            instance.paddles[1].moveState = PaddleAI.MoveState.dodging;
            instance.paddles[1].TryDodge(instance.ball.transform.position);
            instance.paddles[0].moveState = PaddleAI.MoveState.updown;
        }
    }
}