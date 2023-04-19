using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanLaser : MonoBehaviour
{
    private Vector3 eyePos = new Vector3(2.15f, 7.197f, -0.434f);
    private LineRenderer lineRend;

    // Start is called before the first frame update
    void Start()
    {
        lineRend = GetComponent<LineRenderer>();
        lineRend.SetPosition(0, eyePos);
    }

    // Update is called once per frame
    void Update()
    {
        lineRend.SetPosition(1, gameObject.transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("GroundObstacle") || collision.gameObject.CompareTag("SkyObstacle"))
        {
            Destroy(collision.gameObject);
        }
    }
}
