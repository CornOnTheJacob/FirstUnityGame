using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveLeft : MonoBehaviour
{
    private PlayerController playerController;
    private Vector3 previousPos;
    private Vector3 bobPosition;
    private LineRenderer lineRenderer;
    private ScoreManager scoreManager;

    public float speed = 10f;
    public GameObject laserDeathEffect;

    // Start is called before the first frame update
    void Start()
    {
        // Sets up the player controller class as a variable
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        scoreManager = GameObject.Find("Main Camera").GetComponent<ScoreManager>();

        if (gameObject.CompareTag("GroundObstacle"))
        {
            lineRenderer = gameObject.GetComponent<LineRenderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // When the game ends the object is destroyed
        if (!playerController.isAlive)
        {
            Destroy(gameObject);
        }

        // Moves the obstacles to the left
        if (gameObject.CompareTag("Cloud") || gameObject.CompareTag("Tree") || gameObject.CompareTag("Laser End Point"))
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
            if (gameObject.CompareTag("Laser End Point"))
            {
                Invoke("DestroyObject", 0.5f);
            }
        }
        else
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }

        if (gameObject.CompareTag("GroundObstacle"))
        {
            lineRenderer.SetPosition(0, gameObject.transform.position);
            Vector3 gameObjectPosition = gameObject.transform.position;
            gameObjectPosition.y += 20;
            lineRenderer.SetPosition(1, gameObjectPosition);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.CompareTag("Laser End Point") && collision.gameObject.CompareTag("SkyObstacle"))
        {
            scoreManager.UpdateScore(10);
            Instantiate(laserDeathEffect, collision.transform.position, collision.transform.rotation);
            Destroy(collision.gameObject);
        }
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
