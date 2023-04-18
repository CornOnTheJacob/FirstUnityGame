using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private PlayerController playerController;
    private Vector3 previousPos;
    private Vector3 bobPosition;
    private LineRenderer lineRenderer;

    public float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        // Sets up the player controller class as a variable
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

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
        if (gameObject.CompareTag("Cloud") || gameObject.CompareTag("Tree"))
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        else if (gameObject.CompareTag("Titan"))
        {
            transform.Translate(Vector3.back * Time.deltaTime * (speed / 5));
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
}
