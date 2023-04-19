using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Vector3 spawnLocation;
    private bool canShoot = true;
    private ScoreManager scoreManager;
    private float deathAmount = 0;
    private StartGame startGame;
    private float rateOfFire = 0.5f;
    private AudioSource playerAudio;
    private BobObject bobObject;
    private MeshRenderer shootEffect;
    private AudioSource asrc;
    private MeshRenderer bodyMesh;

    public float jumpForce = 10f;
    public float gravityModifier = 1.5f;
    public GameObject bullet;
    public GameObject signal;
    public bool isAlive = true;
    public bool onGround = true;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public AudioClip shootSound;
    public AudioClip metalPipeSound;
    public AudioClip powerUpSound;
    public GameObject body;
    public Material invisible;
    public Material shootMaterial;
    public bool canSignal = false;
    public bool isTitan = false;

    // Start is called before the first frame update
    void Start()
    {
        // Initiates player rigid body and score manager
        playerRb = GetComponent<Rigidbody>();
        scoreManager = GameObject.Find("Main Camera").GetComponent<ScoreManager>();
        startGame = GameObject.Find("Difficulty Select").GetComponent<StartGame>();
        bobObject = GameObject.Find("Body").GetComponent<BobObject>();
        playerAudio = GetComponent<AudioSource>();
        shootEffect = GameObject.Find("Shoot Effect").GetComponent<MeshRenderer>();
        asrc = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        bodyMesh = GameObject.Find("Body").GetComponent<MeshRenderer>();
        GameObject g = GameObject.Find("Titan_5").gameObject;
        g.SetActive(false);

        shootEffect.material = invisible;

        // Modifies gravity
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        // Launches player upwards when on the ground and space is pressed
        if (Input.GetKeyDown(KeyCode.Space) && onGround && startGame.gameStart && !isTitan)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            onGround = false;

            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }

        // Creates a bullet object out of the gun
        if (Input.GetKeyDown(KeyCode.S) && canShoot && isAlive && startGame.gameStart && !isTitan)
        {
            spawnLocation = new Vector3(1.4f, transform.position.y + .5f, -0.4f);
            Instantiate(bullet, spawnLocation, bullet.transform.rotation);

            canShoot = false;
            Invoke("EnableGun", rateOfFire);
            scoreManager.UpdateScore(-5);
            playerAudio.PlayOneShot(shootSound, 1.0f);

            shootEffect.material = shootMaterial;
            Invoke("MakeShootEffectInvisible", 0.1f);
        }

        // Shoots signal
        if (Input.GetKeyDown(KeyCode.A) && canShoot && isAlive && startGame.gameStart && canSignal && !isTitan)
        {
            spawnLocation = new Vector3(1.4f, transform.position.y + .5f, -0.4f);
            Instantiate(signal, spawnLocation, signal.transform.rotation);

            canShoot = false;
            Invoke("EnableGun", rateOfFire);
            scoreManager.UpdateScore(-5);
            playerAudio.PlayOneShot(shootSound, 1.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isTitan)
        {
            // Allows the player to jump when they hit the ground
            if (collision.gameObject.CompareTag("Ground"))
            {
                onGround = true;
            }

            // When players hit an obstacle the obstacle is destroyed, the score is updated, and special effects are run
            else if (collision.gameObject.CompareTag("GroundObstacle") || collision.gameObject.CompareTag("SkyObstacle") && !isTitan)
            {
                canSignal = false;
                Destroy(collision.gameObject);
                if (collision.gameObject.CompareTag("GroundObstacle") || collision.gameObject.CompareTag("SkyObstacle") && !isTitan)
                {
                    scoreManager.UpdateScore(-10);
                }

                // When player gets below a certain score they die
                if (scoreManager.score < deathAmount)
                {
                    isAlive = false;
                    scoreManager.scoreText.text = "You Died";
                    playerRb.position = new Vector3(0, 0.5f, 0);
                    playerRb.rotation = Quaternion.Euler(0, 0, 90);
                    playerAudio.PlayOneShot(metalPipeSound, 3.0f);
                    asrc.enabled = false;
                    bobObject.MakeOpaque();
                }
                else
                {
                    // Plays crash sound and starts player phase effect
                    playerAudio.PlayOneShot(crashSound, 1.0f);
                    bobObject.ChangeOpacity();
                }
            }


            else if (collision.gameObject.CompareTag("Power Up") && !isTitan)
            {
                Destroy(collision.gameObject);
                bodyMesh.material = shootMaterial;
                canSignal = true;
                playerAudio.PlayOneShot(powerUpSound, 1.0f);
                scoreManager.UpdateScore(-100);
            }
        }
    }

    // Allows the player to shoot again
    private void EnableGun()
    {
        canShoot = true;
    }

    private void MakeShootEffectInvisible()
    {
        shootEffect.material = invisible;
    }
}
