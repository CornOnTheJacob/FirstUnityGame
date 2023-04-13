using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRight : MonoBehaviour
{
    private BoxCollider objectCollider;
    private ScoreManager scoreManager;
    private float speed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        scoreManager = GameObject.Find("Main Camera").GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Moves object rights
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.CompareTag("Projectile"))
        {
            // Bullet will destroy sky obstacles
            if (collision.gameObject.CompareTag("SkyObstacle"))
            {
                scoreManager.UpdateScore(10);
            }
        }
        else if (gameObject.CompareTag("Signal"))
        {
            if (collision.gameObject.CompareTag("GroundObstacle"))
            {
                
            }
        }

        // Bullet is destroyed when it hits something
        Destroy(gameObject);
    }
}
