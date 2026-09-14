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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canFall = false;
        rb = GetComponent<Rigidbody>();
        lastPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        fallDelayTimer -= Time.deltaTime;
        if(canFall && fallDelayTimer < 0)
        {
            rb.isKinematic = false;
        }
        platformMovement = transform.position - lastPosition;
        lastPosition = transform.position;
    }
    void OnTriggerEnter(Collider other)
    {
        canFall = true;
        fallDelayTimer = fallDelay;
    }
}
