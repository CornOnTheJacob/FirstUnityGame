using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetShot : MonoBehaviour
{
    private AudioSource objectAudio;

    public ParticleSystem particle;
    public GameObject deathParticle;
    public AudioClip hitSound;

    // Start is called before the first frame update
    void Start()
    {
        objectAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            objectAudio.PlayOneShot(hitSound);
            if (gameObject.CompareTag("SkyObstacle"))
            {
                Instantiate(deathParticle, transform.position, transform.rotation);
                Destroy(gameObject);
            }

            if (gameObject.CompareTag("GroundObstacle"))
            {
                particle.Play();
            }
        }
    }
}
