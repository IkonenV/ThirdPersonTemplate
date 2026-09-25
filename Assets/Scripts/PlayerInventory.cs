using TMPro;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int logsInInventory = 0;
    private TMP_Text logText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logText = GameObject.FindGameObjectWithTag("LogText").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LogCollected(int amount)
    {
        logsInInventory += 1;
        logText.text = logsInInventory.ToString();
        Debug.Log(logsInInventory);
    }
    public void LogUsed(int amount)
    {
        logsInInventory -= amount;
        logText.text = logsInInventory.ToString();
    }
}
