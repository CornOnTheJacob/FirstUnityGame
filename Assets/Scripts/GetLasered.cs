using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetLasered : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private ScoreManager scoreManager;
    private MeshRenderer rend;
    private BoxCollider boxCollider;
    private AudioSource audioSource;
    private bool isAlive = true;

    public ParticleSystem particle1;
    public ParticleSystem particle2;
    public Material lightRed;
    public Material invisible;
    public GameObject body;
    public AudioClip audioClip1;
    public AudioClip audioClip2;
    public AudioClip audioClip3;
    public AudioClip audioClip4;

    // Start is called before the first frame update
    void Start()
    {
        // Initializing components
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        boxCollider = gameObject.GetComponent<BoxCollider>();
        audioSource = gameObject.GetComponent<AudioSource>();
        rend = body.GetComponent<MeshRenderer>();
        scoreManager = GameObject.Find("Main Camera").GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // On collision with signal spawns laser
        if (collision.gameObject.CompareTag("Signal") && isAlive)
        {
            isAlive = false;
            lineRenderer.material = lightRed;
            audioSource.pitch = 1.5f;
            audioSource.PlayOneShot(audioClip2);
            particle2.Play();
            Invoke("LaserObject", 0.5f);
        }
    }

    // Effect of a laser beam
    private void LaserObject()
    {
        particle1.Play();
        scoreManager.UpdateScore(20);
        rend.material = invisible;
        lineRenderer.material = invisible;
        boxCollider.isTrigger = true;

        // Plays sound effects
        audioSource.pitch = 1f;
        audioSource.PlayOneShot(audioClip1, 0.75f);
        audioSource.PlayOneShot(audioClip2);
        audioSource.PlayOneShot(audioClip3);

        Invoke("DestroyObject", 0.5f);
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}