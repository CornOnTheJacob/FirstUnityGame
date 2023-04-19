using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitanController : MonoBehaviour
{
    private ScoreManager scoreManager;
    private AudioSource playerAudio;
    private PlayerController playerController;
    private new BoxCollider collider;
    private StartGame startGame;

    public GameObject titanBody;
    public GameObject robotBody;
    public GameObject laser;
    public AudioClip powerUpSound;
    public AudioClip laserSound;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerAudio = GetComponent<AudioSource>();
        scoreManager = GameObject.Find("Main Camera").GetComponent<ScoreManager>();
        startGame = GameObject.Find("Difficulty Select").GetComponent<StartGame>();
        collider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        // Launches player upwards when on the ground and space is pressed
        if (Input.GetKeyDown(KeyCode.Space) )
        {
            Vector3 spawnLocation = new Vector3(4.25f, -0.36f, 0);
            Instantiate(laser, spawnLocation, laser.transform.rotation);
            playerAudio.PlayOneShot(laserSound);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Power Up 2"))
        {
            Destroy(collision.gameObject);
            ChangeToTitan();
            Invoke("ChangeToRobot", 1000f);
            playerAudio.PlayOneShot(powerUpSound, 1.0f);
            scoreManager.UpdateScore(-100);
        }

        if (playerController.isTitan)
        {

        }
    }

    private void ChangeToTitan()
    {
        playerController.isTitan = true;
        robotBody.SetActive(false);
        titanBody.SetActive(true);
        collider.center = new Vector3(4.608548f, 29.32533f, -0.0556767f);
        collider.size = new Vector3(16.74342f, 58.98114f, 9.244705f);
    }

    private void ChangeToRobot()
    {
        playerController.isTitan = false;
        robotBody.SetActive(true);
        titanBody.SetActive(false);
        collider.center = new Vector3(-0.04683256f, 6.956938f, -0.0556767f);
        collider.size = new Vector3(7.432652f, 14.24435f, 9.244705f);

    }
}
