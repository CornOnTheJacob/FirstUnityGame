using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobObject : MonoBehaviour
{
    private Vector3 previousPos;
    private Vector3 bobPosition;
    private float bobStrength = 0.05f;
    private PlayerController controller;
    private MeshRenderer rend;
    private float fadeSpeed = 0.05f;

    public Material opaque;
    public Material transparent;
    public float bobSpeed = 16.5f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("Player").GetComponent<PlayerController>();
        rend = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isAlive && (controller.onGround || gameObject.CompareTag("GroundObstacle") || gameObject.CompareTag("Titan") || gameObject.CompareTag("Power Up")))
        {
            // Bobs object up and down based on the ingame time
            previousPos = transform.position;
            bobPosition = Vector3.up * Mathf.Cos(Time.time * bobSpeed) * bobStrength;
            bobPosition.x += previousPos.x;

            if (gameObject.CompareTag("Power Up"))
            {
                bobPosition.y += previousPos.y;
            }
            transform.position = bobPosition;
        }
    }

    // Rapid faded effect used when character is hit
    public void ChangeOpacity()
    {
        float interval = 0;
        for (int i = 1; i < 7; i++)
        {
            if (i % 2 == 1)
            {
                Invoke("MakeTransparent", interval);
            }
            else
            {
                Invoke("MakeOpaque", interval);
            }
            interval += fadeSpeed;
        }
    }

    private void MakeOpaque()
    {
        rend.material = opaque;
    }

    private void MakeTransparent()
    {
        rend.material = transparent;
    }
}
