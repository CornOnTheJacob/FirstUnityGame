using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private PlayerController playerController;
    private StartGame startGame;
    private int randNumItem;
    private float randNumInterval;
    private float randNumCloudYPos;
    private float randNumCloudZPos;
    private bool signalPowerup = true;
    private ScoreManager scoreManager;

    public GameObject[] obstacles;
    public GameObject[] clouds;
    public GameObject[] trees;
    public GameObject[] powerUps;
    public float waitTime = 2f;
    public float difficultyModifier;

    // Start is called before the first frame update
    void Start()
    {
        // Sets up the player controller class  and start game class as variables
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        scoreManager = GameObject.Find("Main Camera").GetComponent<ScoreManager>();
        startGame = GameObject.Find("Instructions Background").GetComponent<StartGame>();

        // Continuosly spawns enemies, background objects, and powerups
        Invoke("SpawnGroundObstacleRandomly", waitTime);
        Invoke("SpawnSkyObstacleRandomly", waitTime + 2);
        Invoke("SpawnCloudRandomly", waitTime - 0.75f);
        Invoke("SpawnCloudRandomly", waitTime - 1f);
        Invoke("SpawnTreeRandomly", waitTime - 0.5f);
        Invoke("SpawnPowerUp", 20f);
    }

    // Spawn a ground obstacle after a certain amount of time
    private void SpawnGroundObstacleRandomly()
    {
        
        if (playerController.isAlive && startGame.gameStart)
        {
            randNumInterval = Random.Range(2f - difficultyModifier, 3f - difficultyModifier);
            if (scoreManager.score > 100)
            {
                randNumInterval -= 0.5f;
            }

            // Spawns a ground obstacle
            Instantiate(obstacles[0], new Vector3(25, 0, 0), obstacles[0].transform.rotation);
            
        }
        Invoke("SpawnGroundObstacleRandomly", randNumInterval);
    }

    // Spawn a sky obstacle after a certain amount of time
    private void SpawnSkyObstacleRandomly()
    {
        if (playerController.isAlive && startGame.gameStart)
        {
            randNumInterval = Random.Range(3f - difficultyModifier, 6f - difficultyModifier);
            if (scoreManager.score > 500)
            {
                randNumInterval -= 0.5f;
            }

            // Spawns a sky obstacle
            Instantiate(obstacles[1], new Vector3(30, 3, 0), obstacles[1].transform.rotation);
        }
        Invoke("SpawnSkyObstacleRandomly", randNumInterval);
    }

    // Spawn a cloud after a certain amount of time
    private void SpawnCloudRandomly()
    {
        if (playerController.isAlive && startGame.gameStart)
        {
            randNumItem = Random.Range(0, clouds.Length);
            randNumInterval = Random.Range(1, 3);
            randNumCloudYPos = Random.Range(8f, 14f);
            randNumCloudZPos = Random.Range(1f, 6f);

            // Spawns a cloud
            Instantiate(clouds[randNumItem], new Vector3(60, randNumCloudYPos, randNumCloudZPos + 17), clouds[randNumItem].transform.rotation);
        }
        Invoke("SpawnCloudRandomly", randNumInterval);
    }

    // Spawn a cloud after a certain amount of time
    private void SpawnTreeRandomly()
    {
        if (playerController.isAlive && startGame.gameStart)
        {
            randNumItem = Random.Range(0, trees.Length);
            randNumInterval = Random.Range(0.5f, 1.25f);

            // Spawns a cloud
            Instantiate(trees[randNumItem], new Vector3(30, 1, 3.8f), trees[randNumItem].transform.rotation);
        }
        Invoke("SpawnTreeRandomly", randNumInterval);
    }
    // Spawns a power up after a random amount of time
    private void SpawnPowerUp()
    {
        if (playerController.isAlive && startGame.gameStart)
        {
            if (signalPowerup)
            {
                randNumItem = 0;
                signalPowerup = false;
            }
            else
            {
                randNumItem = 1;
                signalPowerup = true;
            }
            //randNumItem = Random.Range(0, powerUps.Length);
            randNumInterval = Random.Range(15f + (difficultyModifier * 5), 25f + (difficultyModifier * 5));

            // Spawns a basic power up
            Instantiate(powerUps[randNumItem], new Vector3(25, 1, 0), powerUps[randNumItem].transform.rotation);

        }
        Invoke("SpawnPowerUp", randNumInterval);
    }
}
