using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform spawnPoint;
    Void voidObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        voidObject = GameObject.FindGameObjectWithTag("Void").GetComponent<Void>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckpointActivated();
        }
    }
    public void CheckpointActivated()
    {
        voidObject.respawnPoint.position = spawnPoint.position;
    }
}
