using UnityEngine;

public class Levelmanager : MonoBehaviour
{

    public GameObject currentLevel;
    public GameObject[] levels;
    public GameObject gateHitbox;
    public float gateTime;
    private float gateTimer;
    bool gateActive;
    bool canChangeLevel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gateHitbox.SetActive(false);
        gateActive = false;
        canChangeLevel = true;
    }

    // Update is called once per frame
    void Update()
    {
        gateTimer -= Time.deltaTime;
        if(gateActive && gateTimer < 0)
        {
            gateActive = false;
            gateHitbox.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)&& gateTimer < 0 && canChangeLevel)
        {
            Debug.Log("Vaihdetaan leveliin 1");
            ChangeLevel(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)&& gateTimer < 0 && canChangeLevel)
        {
            Debug.Log("Vaihdetaan leveliin 2");
            ChangeLevel(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && gateTimer < 0 && canChangeLevel)
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

        gateHitbox.SetActive(true);
        gateActive = true;
        gateTimer = gateTime;

    }
    public void CantChangeLevel()
    {
        canChangeLevel = !canChangeLevel;
    }
    public void CanChangeLevel()
    {
        canChangeLevel = true;
    }
}
