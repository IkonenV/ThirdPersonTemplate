using UnityEngine;

public class Knight : MonoBehaviour
{
    public GameObject overHeadText;
    Levelmanager levelmanager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levelmanager = GameObject.Find("LevelManager").GetComponent<Levelmanager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
       overHeadText.SetActive(true); 
       levelmanager.CanChangeLevel();
    }
    void OnTriggerExit(Collider other)
    {
        overHeadText.SetActive(false);
        levelmanager.CantChangeLevel();
    }
}
