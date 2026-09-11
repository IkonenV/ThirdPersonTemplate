using UnityEngine;

public class Levelmanager : MonoBehaviour
{

    public GameObject currentLevel;
    public GameObject[] levels;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Vaihdetaan leveliin 1");
            ChangeLevel(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Vaihdetaan leveliin 2");
            ChangeLevel(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("Vaihdetaan leveliin 3");
            ChangeLevel(3);
        }
    }
    public void ChangeLevel(int desiredLevel)
    {
        if(currentLevel != null)
        {
            foreach(Transform child in currentLevel.transform)
        {
            ObstacleSwitch obstacleSwitch = child.GetComponent<ObstacleSwitch>();
            obstacleSwitch.ChangeLevel(false);
        }
        }
        currentLevel = levels[desiredLevel - 1];
        foreach(Transform child in currentLevel.transform)
        {
            ObstacleSwitch obstacleSwitch = child.GetComponent<ObstacleSwitch>();
            obstacleSwitch.ChangeLevel(true);
        }
    }
}
