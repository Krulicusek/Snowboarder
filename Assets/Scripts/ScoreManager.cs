using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    #region Properties
    public static ScoreManager instance;
    public Text scoreText;
    public Text highscoreText;
    #endregion

    #region Members
    // Moment is counted as 0.5s
    [SerializeField] private int pointsPerMomentInAir = 5;
    [SerializeField] private int pointsPerMomentInButterfly = 50;
    [SerializeField] private int pointsPerRotation = 1;
    int score = 0;
    int highscore = 0;
    int scoreForFlying;
    int scoreForButterfly;
    int scoreForRotation;
    #endregion
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        scoreText.text = $"{score.ToString()} POINTS";
        highscoreText.text = $"HIGHSCORE: {highscore.ToString()}";
    }
    
    public void AddPointsForFlying(float time)
    {
        int scoreToAdd = (int)Math.Round(time) * pointsPerMomentInAir;
        score += scoreToAdd;
        scoreForFlying += scoreToAdd;
        scoreText.text = $"{score.ToString()} POINTS";      
    }
    public void AddPointsForRotation(float rotation)
    {
        int scoreToAdd = (int)Math.Abs(rotation) * pointsPerRotation;
        score += scoreToAdd;
        scoreForRotation += scoreToAdd;
        scoreText.text = $"{score.ToString()} POINTS";
    }
    public void AddPointsForButterfly(float time)
    {
        int scoreToAdd = (int)Math.Round(time) * pointsPerMomentInButterfly;
        score += scoreToAdd;
        scoreForButterfly += scoreToAdd;
        scoreText.text = $"{score.ToString()} POINTS";
        Debug.Log(scoreForButterfly);
    }
}
