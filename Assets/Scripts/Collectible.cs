using UnityEngine;
using UnityEngine.InputSystem;

public class Collectible : MonoBehaviour
{
    bool canPickUp = false;
    public GameObject textBox;
    public PlayerInventory playerInventory;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamepad.current.buttonWest.wasPressedThisFrame && canPickUp || Input.GetKeyDown(KeyCode.E) && canPickUp)
        {
            PickedUpLog();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        canPickUp = true;
        textBox.SetActive(true);
    }
    void OnTriggerExit(Collider other)
    {
        canPickUp = false;
        textBox.SetActive(false);
    }
    public void PickedUpLog()
    {
        playerInventory.LogCollected(1);
        Destroy(gameObject);
    }

}
