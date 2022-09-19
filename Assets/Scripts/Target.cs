using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody target;
    private GameManager gameManager;
    private AudioSource blast;

    private float minSpeed = 12.0f;
    private float maxSpeed = 16.0f;
    private float torqueValue = 10.0f;
    private float xRange = 4.0f;
    private float ySpawnPos = -2.0f;

    public ParticleSystem explosion;
    public AudioClip clip;

    public int pointValue;

    // Start is called before the first frame update
    void Start()
    {
        target = GetComponent<Rigidbody>();
        blast = GetComponent<AudioSource>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        target.AddForce(RandomForce(), ForceMode.Impulse);
        target.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        transform.position = RandomSpawnpos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        // no longer needed
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        
        if (!gameObject.CompareTag("Bad"))
            gameManager.Life();
    }

    public void DestroyTarget()
    {
        if (gameManager.isGameActive)
        {
            blast.PlayOneShot(clip);
            Destroy(gameObject);
            Instantiate(explosion, transform.position, explosion.transform.rotation);
            gameManager.UpdateScore(pointValue);
        }
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }
    float RandomTorque()
    {
        return Random.Range(-torqueValue, torqueValue);
    }
    Vector3 RandomSpawnpos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
}
