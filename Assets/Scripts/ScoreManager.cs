using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System;

public class ScoreManager : MonoBehaviour
{
    private PlayerController playerController;
    private StartGame startGame;

    public int score = 30;
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        // Sets up the player controller class as a variable
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        startGame = GameObject.Find("Difficulty Select").GetComponent<StartGame>();

        UpdateScore(0);
        InvokeRepeating("UpdateScoreEverySecond", 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Function used to update the score throughout the program
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score.ToString();
    }

    // Increases the score passivley over time
    private void UpdateScoreEverySecond()
    {
        if (playerController.isAlive && startGame.gameStart)
        {
            score += 1;
            scoreText.text = "Score: " + score.ToString();
        }
        else if (!playerController.isAlive)
        {
            CancelInvoke();
        }
    }
}
