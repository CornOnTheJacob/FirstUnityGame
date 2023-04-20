using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InBounds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Destroys object when out of the game view
        if (gameObject.CompareTag("Laser End Point") && transform.position.x > 25)
        {
            Destroy(gameObject);
        }
        else if (transform.position.x < - 50 || transform.position.x  > 65)
        {
            Destroy(gameObject);
        }
    }
}
