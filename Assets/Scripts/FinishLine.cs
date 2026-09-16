using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public Transform respawnPoint;
    private Transform startSpawnPoint;
    private GameObject player;
    CharacterController characterController;
    Void voidScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        voidScript =  GameObject.FindGameObjectWithTag("Void").GetComponent<Void>();
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
            voidScript.respawnPoint = startSpawnPoint.position;
            player.transform.position = voidScript.respawnPoint;
            characterController.enabled = true;
        }
    }
    public void LevelFinished()
    {
        
    }
}
