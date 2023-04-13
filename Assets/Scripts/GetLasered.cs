using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetLasered : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private ScoreManager scoreManager;
    private MeshRenderer rend;

    public ParticleSystem particle;
    public Material lightRed;
    public Material invisible;
    public GameObject body;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        rend = body.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreManager = GameObject.Find("Main Camera").GetComponent<ScoreManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Signal"))
        {
            lineRenderer.material = lightRed;
            Invoke("KillObject", 0.2f);
        }
    }

    private void KillObject()
    {
        particle.Play();
        scoreManager.UpdateScore(20);
        rend.material = invisible;
        Invoke("DestroyObject", 0.5f);
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
