using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    private PlayerController playerController;
    private StartGame startGame;
    private AudioSource audioSource;
    private bool resetTime = false;
    private bool resetScore = false;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public int score = 0;
    public int time = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Sets up the player controller class as a variable
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        startGame = GameObject.Find("Instructions Background").GetComponent<StartGame>();
        audioSource = GetComponent<AudioSource>();

        // Runs score update functions
        UpdateScore(0);
        InvokeRepeating("UpdateScoreEverySecond", 1, 1);
        InvokeRepeating("UpdateTimeEverySecond", 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        // Changes pitch and tempo of music based on the players score
        if (score < 100 || resetScore)
        {
            audioSource.pitch = 1f;
        }
        else if (score < 200)
        {
            audioSource.pitch = 1.20f;
        }
        else if (score < 300)
        {
            audioSource.pitch = 1.50f;
        }
        else if (score > 1000)
        {
            audioSource.pitch = 1.75f;
        }
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
            if (resetScore)
            {
                score = 0;
                resetScore = false;
            }
            score += 1;
            scoreText.text = "Score: " + score.ToString();
        }
        else if (!playerController.isAlive)
        {
            resetScore = true;
            //CancelInvoke();
        }
    }

    // Updates timer
    private void UpdateTimeEverySecond()
    {
        if (playerController.isAlive && startGame.gameStart)
        {
            if (resetTime)
            {
                time = 0;
                resetTime = false;
            }
            time += 1;
            int minutes = time / 60;
            int seconds = time % 60;

            if (seconds < 10)
            {
                timeText.text = "Time: " + minutes.ToString() + ":0" + seconds.ToString();
            }
            else
            {
                timeText.text = "Time: " + minutes.ToString() + ":" + seconds.ToString();
            }
        }
        else if (!playerController.isAlive)
        {
            resetTime = true;
            //CancelInvoke();
        }
    }
}
