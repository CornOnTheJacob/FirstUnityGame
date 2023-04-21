using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoOverlapping : MonoBehaviour
{
    private Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("GroundObstacle"))
        {
            pos = collision.gameObject.transform.position;
            pos.x += 2.3f;
            pos.y = gameObject.transform.position.y;
            gameObject.transform.position = pos;
        }
    }
}
