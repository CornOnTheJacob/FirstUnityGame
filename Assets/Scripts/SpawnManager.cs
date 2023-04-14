using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private PlayerController playerController;
    private int randNumItem;
    private float randNumInterval;
    private float randNumCloudYPos;
    private float randNumCloudZPos;
    private StartGame startGame;

    public GameObject[] obstacles;
    public GameObject[] clouds;
    public GameObject[] trees;
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
        Invoke("SpawnTreeRandomly", waitTime - 0.5f);
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
            randNumInterval = Random.Range(2f - difficultyModifier, 3f - difficultyModifier);

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
            randNumInterval = Random.Range(1f, 1.5f);
            randNumCloudYPos = Random.Range(6f, 8f);
            randNumCloudZPos = Random.Range(2f, 3.8f);

            // Spawns a cloud
            Instantiate(clouds[randNumItem], new Vector3(30, randNumCloudYPos, randNumCloudZPos), clouds[randNumItem].transform.rotation);
        }
        Invoke("SpawnCloudRandomly", randNumInterval);
    }

    // Spawn a cloud after a certain amount of time
    private void SpawnTreeRandomly()
    {
        if (playerController.isAlive && startGame.gameStart)
        {
            randNumItem = Random.Range(0, trees.Length);
            randNumInterval = Random.Range(1f, 1.5f);

            // Spawns a cloud
            Instantiate(trees[randNumItem], new Vector3(30, 1, 3.8f), trees[randNumItem].transform.rotation);
        }
        Invoke("SpawnTreeRandomly", randNumInterval);
    }
}
