using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private PlayerController playerController;
    private float randNumGround;
    private float randNumSky;
    private float randNumCloud;
    private float randNumCloudYPos;
    private StartGame startGame;

    public GameObject[] obstacles;
    public float waitTime = 2f;
    public float difficultyModifier;

    // Start is called before the first frame update
    void Start()
    {
        // Sets up the player controller class as a variable
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        startGame = GameObject.Find("Difficulty Select").GetComponent<StartGame>();

        // Continuosly spawns enemies
        Invoke("SpawnGroundObstacleRandomly", waitTime);
        Invoke("SpawnSkyObstacleRandomly", waitTime + 2);
        Invoke("SpawnCloudRandomly", waitTime - 0.75f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Spawn a ground obstacle after a certain amount of time
    private void SpawnGroundObstacleRandomly()
    {
        
        if (playerController.isAlive && startGame.gameStart)
        {
            randNumGround = Random.Range(2f - difficultyModifier, 3f - difficultyModifier);

            // Spawns a ground obstacle
            Instantiate(obstacles[0], new Vector3(25, 0, 0), obstacles[0].transform.rotation);
        }
        Invoke("SpawnGroundObstacleRandomly", randNumGround);
    }

    // Spawn a sky obstacle after a certain amount of time
    private void SpawnSkyObstacleRandomly()
    {
        if (playerController.isAlive && startGame.gameStart)
        {
            randNumSky = Random.Range(3f - difficultyModifier, 6f - difficultyModifier);

            // Spawns a sky obstacle
            Instantiate(obstacles[1], new Vector3(30, 3, 0), obstacles[1].transform.rotation);
        }
        Invoke("SpawnSkyObstacleRandomly", randNumSky);
    }

    // Spawn a cloud after a certain amount of time
    private void SpawnCloudRandomly()
    {
        if (playerController.isAlive && startGame.gameStart)
        {
            randNumCloud = Random.Range(1f, 1.5f);
            randNumCloudYPos = Random.Range(6, 8);

            // Spawns a cloud
            Instantiate(obstacles[2], new Vector3(30, randNumCloudYPos, 2), obstacles[2].transform.rotation);
        }
        Invoke("SpawnCloudRandomly", randNumCloud);
    }
}
