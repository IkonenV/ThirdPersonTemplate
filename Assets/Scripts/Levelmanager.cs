using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class Levelmanager : MonoBehaviour
{

    public GameObject currentLevel;
    public GameObject[] levels;
    public GameObject gateHitbox;
    public float gateTime;
    private float gateTimer;
    bool gateActive;
    bool canChangeLevel;
    Transform player;
    Transform knight;

    public GameObject levelSelectScreen;
    public GameObject firstButton;
    bool levelSelectActive = false;

    public PlayerInput playerInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gateHitbox.SetActive(false);
        gateActive = false;
        canChangeLevel = false;
        levelSelectScreen = GameObject.FindGameObjectWithTag("LevelSelect");
        levelSelectScreen.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        knight = GameObject.FindGameObjectWithTag("Knight").GetComponent<Transform>();
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
            ChangeLevel(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)&& gateTimer < 0 && canChangeLevel)
        {
            ChangeLevel(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && gateTimer < 0 && canChangeLevel)
        {
            ChangeLevel(3);
        }

        

    }
    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(firstButton);
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed && canChangeLevel && !levelSelectActive)
        {
            EnableLevelSelect();
        }
        else if(context.performed && levelSelectActive)
        {
            DisableLevelSelect();
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
        canChangeLevel = false;
    }
    public void CanChangeLevel()
    {
        canChangeLevel = true;
    }
    public void LevelButtonPressed(int number)
    {
        ChangeLevel(number);
        DisableLevelSelect();
    }
    public void EnableLevelSelect()
    {
        levelSelectScreen.SetActive(true);
        levelSelectActive = true;
        playerInput.SwitchCurrentActionMap("UI");
    }
    public void DisableLevelSelect()
    {
        levelSelectScreen.SetActive(false);
        levelSelectActive = false;
        playerInput.SwitchCurrentActionMap("Player");
    }
}
