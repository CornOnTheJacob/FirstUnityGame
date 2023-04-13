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
        if (transform.position.x < - 10 || transform.position.x  > 30)
        {
            Destroy(gameObject);
        }
    }
}
