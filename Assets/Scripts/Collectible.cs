using UnityEngine;

public class Collectible : MonoBehaviour
{
    PlayerInventory playerInventory;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").AddComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        playerInventory.LogCollected(1);
        Destroy(gameObject);
    }
}
