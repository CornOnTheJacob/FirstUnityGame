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
    private AudioSource audioSource;

    public int score = 0;
    public int time = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;

    // Start is called before the first frame update
    void Start()
    {
        // Sets up the player controller class as a variable
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        startGame = GameObject.Find("Difficulty Select").GetComponent<StartGame>();
        audioSource = GetComponent<AudioSource>();

        UpdateScore(0);
        InvokeRepeating("UpdateScoreEverySecond", 1, 1);
        InvokeRepeating("UpdateTimeEverySecond", 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (score < 100)
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
            score += 1;
            scoreText.text = "Score: " + score.ToString();
        }
        else if (!playerController.isAlive)
        {
            CancelInvoke();
        }
    }

    private void UpdateTimeEverySecond()
    {
        if (playerController.isAlive && startGame.gameStart)
        {
            time += 1;
            timeText.text = "Time: " + time.ToString();
        }
        else if (!playerController.isAlive)
        {
            CancelInvoke();
        }
    }
}
