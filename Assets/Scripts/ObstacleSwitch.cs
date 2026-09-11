using UnityEngine;

public class ObstacleSwitch : MonoBehaviour
{
    private bool movingToB = true;
    public float travelTime = 10;
    private Vector3 upPosition;
    private Vector3 downPosition;
    private float t = 0f;
    bool movingUp;
    bool movingDown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movingDown = false;
        movingUp = false;
        downPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        upPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 20, gameObject.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (movingDown)
        {
            MoveDown();
        }
        else if (movingUp)
        {
            MoveUp();
        }
    }
    public void ChangeLevel(bool levelActivate)
    {
        t = 0;
        if(levelActivate == true)
        {
            movingUp = true;
        }
        else if(levelActivate == false)
        {
            movingDown = true;
        }
    }
    public void MoveUp()
    {
         t += Time.deltaTime / travelTime;

        float smoothT = Mathf.SmoothStep(0f,1f, t);
        transform.position = Vector3.Lerp(downPosition, upPosition, t);
        if(Vector3.Distance(transform.position, upPosition) < 0.1f)
        {
            movingUp = false;
        }
    }
    public void MoveDown()
    {
        
         t += Time.deltaTime / travelTime;

        float smoothT = Mathf.SmoothStep(0f,1f, t);
        transform.position = Vector3.Lerp(upPosition, downPosition, smoothT);
        if(Vector3.Distance(transform.position, downPosition) < 0.1f)
        {
            // make invisible
            movingDown = false;
        }
    }

}
