using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreVisualiser : MonoBehaviour {

    Text scoreText;

    void Start()
    {
        scoreText = GetComponent<Text>();
    }

    void FixedUpdate()
    {
        UpdateScoreText();
    }
    
    void UpdateScoreText()
    {
        scoreText.text = ScoreManager.GetScore().ToString();
    }

}
