using UnityEngine;

public class Void : MonoBehaviour
{
    public Transform respawnPoint;
    private Transform startSpawnPoint;
    private GameObject player;
    CharacterController characterController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        startSpawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").GetComponent<Transform>();
        respawnPoint = startSpawnPoint;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            characterController.enabled = false;
            player.transform.position = respawnPoint.position;
            characterController.enabled = true;
        }
    }
}
