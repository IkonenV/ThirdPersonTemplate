using UnityEngine;

public class Levelmanager : MonoBehaviour
{
    public GameObject currentLevel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ChangeLevel(3);
    }

    // Update is called once per frame
    void Update()
    {
        //ChangeLevel(3);
    }
    public void ChangeLevel(int desiredLevel)
    {
        foreach(Transform child in currentLevel.transform)
        {
            //Vector3 desiredHeight = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 50, gameObject.transform.position.z);
            //float speed = Random.Range(5, 10);
            //transform.position = Vector3.Lerp(child.position,desiredHeight,speed);
            Animator animator = child.GetComponent<Animator>();
            animator.SetTrigger("Descend");
        }
    }
}
