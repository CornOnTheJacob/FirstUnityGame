using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticleEffect : MonoBehaviour
{
    public ParticleSystem particle;

    // Start is called before the first frame update
    void Start()
    {
        // Plays particles
        particle.Play();
    }
}
