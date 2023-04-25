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
    private float laserFireRate = 2f;
    private AudioSource audioSource;
    private Rigidbody playerRb;

    public GameObject titanBody;
    public GameObject robotBody;
    public GameObject laser;
    public AudioClip powerUpSound;
    public AudioClip laserSound;
    public AudioClip coolSong;
    public AudioClip normalSong;
    public bool canLaser = true;
    public float titanDuration = 15f;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
        playerAudio = GetComponent<AudioSource>();
        scoreManager = GameObject.Find("Main Camera").GetComponent<ScoreManager>();
        startGame = GameObject.Find("Difficulty Select").GetComponent<StartGame>();
        collider = GetComponent<BoxCollider>();
        audioSource = scoreManager.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Shoots laser from titans eye when space is pressed
        if (Input.GetKeyDown(KeyCode.Space) && playerController.isTitan && canLaser)
        {
            Vector3 spawnLocation = new Vector3(4.25f, -0.36f, 0);
            Instantiate(laser, spawnLocation, laser.transform.rotation);
            playerAudio.PlayOneShot(laserSound, 3f);
            canLaser = false;
            Invoke("EnableLaser", laserFireRate);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Power Up 2"))
        {
            Destroy(collision.gameObject);
            ChangeToTitan();
            Invoke("ChangeToRobot", titanDuration);
            playerAudio.PlayOneShot(powerUpSound, 1.0f);
            scoreManager.UpdateScore(-20);
        }
    }

    private void ChangeToTitan()
    {
        playerController.isTitan = true;
        robotBody.SetActive(false);
        titanBody.SetActive(true);
        collider.center = new Vector3(4.608548f, 29.32533f, -0.0556767f);
        collider.size = new Vector3(16.74342f, 58.98114f, 9.244705f);
        playerRb.velocity = Vector3.zero;
        gameObject.transform.position = new Vector3(0, 0.025f, 0);

        audioSource.clip = coolSong;
        audioSource.Play();
    }

    private void ChangeToRobot()
    {
        playerController.isTitan = false;
        playerController.onGround = true;
        robotBody.SetActive(true);
        titanBody.SetActive(false);
        collider.center = new Vector3(-0.04683256f, 6.956938f, -0.0556767f);
        collider.size = new Vector3(7.432652f, 14.24435f, 9.244705f);
        audioSource.clip = normalSong;
        audioSource.Play();
    }

    private void EnableLaser()
    {
        canLaser = true;
    }
}
