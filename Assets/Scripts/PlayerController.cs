using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private ScoreManager scoreManager;
    private StartGame startGame;
    private BobObject bobObject;
    private AudioSource asrc;
    private AudioSource playerAudio;
    private MeshRenderer shootEffect;
    private MeshRenderer bodyMesh;
    private GameObject titan;
    private Vector3 spawnLocation;
    private float deathAmount = 0;
    private float rateOfFire = 0.5f;
    private bool canShoot = true;

    public GameObject bullet;
    public GameObject signal;
    public GameObject body;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public AudioClip shootSound;
    public AudioClip metalPipeSound;
    public AudioClip powerUpSound;
    public Material invisible;
    public Material shootMaterial;
    public ParticleSystem dirtSplatter;
    public float jumpForce = 10f;
    public float gravityModifier = 1.5f;
    public bool isAlive = true;
    public bool onGround = true;
    public bool canSignal = false;
    public bool isTitan = false;
    public bool isPoweredUp;

    // Start is called before the first frame update
    void Start()
    {
        // Initiates objects
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        scoreManager = GameObject.Find("Main Camera").GetComponent<ScoreManager>();
        startGame = GameObject.Find("Difficulty Select").GetComponent<StartGame>();
        bobObject = GameObject.Find("Body").GetComponent<BobObject>();
        shootEffect = GameObject.Find("Shoot Effect").GetComponent<MeshRenderer>();
        asrc = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        bodyMesh = GameObject.Find("Body").GetComponent<MeshRenderer>();
        titan = GameObject.Find("Titan_5").gameObject;

        // Modifies gravity
        Physics.gravity *= gravityModifier;

        // Hides from camera
        titan.SetActive(false);
        dirtSplatter.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        // Launches player upwards when on the ground and space is pressed
        if (Input.GetKeyDown(KeyCode.Space) && onGround && startGame.gameStart && !isTitan)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            dirtSplatter.Stop();
            onGround = false;
        }

        // Creates a bullet object out of the gun
        if (Input.GetKeyDown(KeyCode.S) && canShoot && isAlive && startGame.gameStart && !isTitan)
        {
            // Makes bullet
            spawnLocation = new Vector3(1.4f, transform.position.y + .5f, -0.4f);
            Instantiate(bullet, spawnLocation, bullet.transform.rotation);

            // Gun exhaust effect
            shootEffect.material = shootMaterial;
            Invoke("MakeShootEffectInvisible", 0.1f);

            canShoot = false;
            scoreManager.UpdateScore(-5);
            playerAudio.PlayOneShot(shootSound, 1.0f);
            Invoke("EnableGun", rateOfFire);
        }

        // Shoots signal
        if (Input.GetKeyDown(KeyCode.A) && canShoot && isAlive && startGame.gameStart && canSignal && !isTitan)
        {
            // Makes signal
            spawnLocation = new Vector3(1.4f, transform.position.y + .5f, -0.4f);
            Instantiate(signal, spawnLocation, signal.transform.rotation);

            canShoot = false;
            scoreManager.UpdateScore(-5);
            playerAudio.PlayOneShot(shootSound, 1.0f);
            Invoke("EnableGun", rateOfFire);
        }

        // Makes players model reflect that they are powered up
        if (isPoweredUp && bodyMesh.material != shootMaterial)
        {
            bodyMesh.material = shootMaterial;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isTitan)
        {
            // Allows the player to jump when they hit the ground
            if (collision.gameObject.CompareTag("Ground") && startGame.gameStart && isAlive)
            {
                onGround = true;
                dirtSplatter.Play();
            }

            // Kills player if in contact with signal laser
            else if (collision.gameObject.CompareTag("Laser End Point"))
            {
                KillPlayer();
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
                    KillPlayer();
                }
                else
                {
                    // Plays crash sound and starts player phase effect
                    playerAudio.PlayOneShot(crashSound, 1.0f);
                    bobObject.ChangeOpacity();
                    isPoweredUp = false;
                }
            }

            // When player gets a power up the player material changes, the score changes, effects play, and the player can shoot signals
            else if (collision.gameObject.CompareTag("Power Up") && !isTitan)
            {
                Destroy(collision.gameObject);
                bodyMesh.material = shootMaterial;
                playerAudio.PlayOneShot(powerUpSound, 1.0f);
                canSignal = true;
                isPoweredUp = true;

                // Player gains points when they gain a power up while already powered up
                if (isPoweredUp)
                {
                    scoreManager.UpdateScore(50);
                }
                // Player loses points when they gain a power up when they have none
                else
                {
                    scoreManager.UpdateScore(-50);
                }
            }
        }
    }

    // Allows the player to shoot again
    private void EnableGun()
    {
        canShoot = true;
    }
    
    // Hides the exhaust from shooting
    private void MakeShootEffectInvisible()
    {
        shootEffect.material = invisible;
    }

    // Changes values and sets parameters that happen when the player is dead
    private void KillPlayer()
    {
        isAlive = false;
        isPoweredUp = false;
        asrc.enabled = false;

        playerRb.position = new Vector3(0, 0.5f, 0);
        playerRb.rotation = Quaternion.Euler(0, 0, 90);

        playerAudio.PlayOneShot(metalPipeSound, 4.0f);
        bobObject.MakeOpaque();
        dirtSplatter.Stop();
    }
}
