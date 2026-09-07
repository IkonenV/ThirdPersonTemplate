using Unity.VisualScripting;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float fallDelay = 0.5f;
    private float fallDelayTimer;
    bool canFall;
    Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canFall = false;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        fallDelayTimer -= Time.deltaTime;
        if(canFall && fallDelayTimer < 0)
        {
            rb.isKinematic = false;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        canFall = true;
        fallDelayTimer = fallDelay;
    }
}
