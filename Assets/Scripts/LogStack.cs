using UnityEngine;
using UnityEngine.InputSystem;

public class LogStack : MonoBehaviour
{
    bool canInteract = false;
    public GameObject textBox;
    public GameObject[] logs;
    int logIndex = 0;
    PlayerInventory playerInventory;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamepad.current.buttonWest.wasPressedThisFrame && canInteract && playerInventory.logsInInventory > 0 || Input.GetKeyDown(KeyCode.E) && canInteract && playerInventory.logsInInventory > 0)
        {
            PlaceLog();
            playerInventory.LogUsed(1);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        canInteract = true;
        textBox.SetActive(true);
    }
    void OnTriggerExit(Collider other)
    {
        canInteract = false;
        textBox.SetActive(false);
    }
    public void PlaceLog()
    {
        GameObject currentLog = logs[logIndex];
        currentLog.SetActive(true);
        logIndex += 1;
    }
}
