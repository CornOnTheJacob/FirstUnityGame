using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRight : MonoBehaviour
{
    private BoxCollider objectCollider;
    private ScoreManager scoreManager;
    private float speed = 20f;
    private bool stuck = false;
    private Vector3 pos;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        scoreManager = GameObject.Find("Main Camera").GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerController.isAlive)
        {
            Destroy(gameObject);
        }

        // Moves object rights
        if (!stuck)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        else
        {
            transform.Translate(Vector3.left * Time.deltaTime * 10f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.CompareTag("Signal") || gameObject.CompareTag("Projectile"))
        {


            if (gameObject.CompareTag("Projectile"))
            {
                // Bullet will destroy sky obstacles
                if (collision.gameObject.CompareTag("SkyObstacle"))
                {
                    scoreManager.UpdateScore(10);
                }
                // Bullet is destroyed when it hits something
                Destroy(gameObject);
            }
            else if (gameObject.CompareTag("Signal"))
            {
                if (collision.gameObject.CompareTag("GroundObstacle"))
                {
                    stuck = true;
                    pos = gameObject.transform.position;
                    pos.x = collision.transform.position.x - 0.42f;
                    gameObject.transform.position = pos;
                    Invoke("DestroyObject", 0.5f);
                }
            }
        }
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
