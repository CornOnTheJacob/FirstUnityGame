using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    private SpawnManager spawnManager;
    private PlayerController playerController;
    private float hardModifier = 0.6f;

    public bool gameStart = false;

    // Start is called before the first frame update
    void Start()
    {
        // Sets up the player controller and spawn manager classes as variables
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Regular difficulty
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            spawnManager.difficultyModifier = 0;
            GameStart();
        }

        // Hard difficulty
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            spawnManager.difficultyModifier = hardModifier;
            GameStart();
        }
    }

    // Function that starts the game
    void GameStart()
    {
        gameStart = true;
        playerController.dirtSplatter.Play();
        gameObject.SetActive(false);
    }
}
