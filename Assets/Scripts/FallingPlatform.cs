using Unity.VisualScripting;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float fallDelay = 0.5f;
    private float fallDelayTimer;
    bool canFall;
    Rigidbody rb;
    private Vector3 lastPosition;
    public Vector3 platformMovement;
    public Vector3 startPosition;
    public float positionResetTime;
    public float resetTimer;
    public bool alreadyFell;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
        canFall = false;
        rb = GetComponent<Rigidbody>();
        lastPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        fallDelayTimer -= Time.deltaTime;
        resetTimer -= Time.deltaTime;
        if(canFall && fallDelayTimer < 0 && !alreadyFell)
        {
            rb.isKinematic = false;
            resetTimer = positionResetTime;
            alreadyFell = true;
        }
        platformMovement = transform.position - lastPosition;
        lastPosition = transform.position;
        if(resetTimer < 0 && alreadyFell)
        {
            rb.isKinematic = true;
            transform.position = startPosition;
            alreadyFell = false;
            canFall = false;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
        canFall = true;
        fallDelayTimer = fallDelay;  
        }
    }
}
