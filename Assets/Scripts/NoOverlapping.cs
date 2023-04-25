using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoOverlapping : MonoBehaviour
{
    private Vector3 pos;

    private void OnCollisionEnter(Collision collision)
    {
        // Moves objects out of ground obstacles if the over lap
        if (collision.gameObject.CompareTag("GroundObstacle"))
        {
            pos = collision.gameObject.transform.position;
            pos.x += 2.3f;
            pos.y = gameObject.transform.position.y;
            gameObject.transform.position = pos;
        }
    }
}
