using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanLaser : MonoBehaviour
{
    private LineRenderer lineRend;
    private ScoreManager scoreManager;
    private PlayerController playerController;
    private Vector3 eyePos = new Vector3(2.15f, 7.197f, -0.434f);

    public GameObject deathEffect;

    // Start is called before the first frame update
    void Start()
    {
        scoreManager = GameObject.Find("Main Camera").GetComponent<ScoreManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        lineRend = GetComponent<LineRenderer>();
        lineRend.SetPosition(0, eyePos);
    }

    // Update is called once per frame
    void Update()
    {
        lineRend.SetPosition(1, gameObject.transform.position);

        if (!playerController.isTitan)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("GroundObstacle") || collision.gameObject.CompareTag("SkyObstacle"))
        {
            scoreManager.UpdateScore(10);
            Instantiate(deathEffect, collision.transform.position, collision.transform.rotation);
            Destroy(collision.gameObject);
        }
    }
}
