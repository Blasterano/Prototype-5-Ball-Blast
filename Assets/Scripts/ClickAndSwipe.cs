using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer), typeof(BoxCollider))]
public class ClickAndSwipe : MonoBehaviour
{
    private GameManager gameManager;
    private Camera cam;
    private BoxCollider col;
    private TrailRenderer trail;
    private Vector3 mousePos;

    private bool swiping = false;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        
        cam = Camera.main;
        col = GetComponent<BoxCollider>();
        trail = GetComponent<TrailRenderer>();

        col.enabled = false;
        trail.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.isGameActive)
        {
            if(Input.GetMouseButton(0))
            {
                swiping = true;
                UpdateComponent();
            }
            else
            {
                swiping = false;
                UpdateComponent();
            }

            if (swiping)
                UpdateMousePosition();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Target>())
        {
            collision.gameObject.GetComponent<Target>().DestroyTarget();
        }
    }

    void UpdateMousePosition()
    {
        mousePos=cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
        transform.position = mousePos;
    }

    void UpdateComponent()
    {
        col.enabled = swiping;
        trail.enabled = swiping;
    }
}
