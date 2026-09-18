using UnityEngine;

public class ObstacleSwitch : MonoBehaviour
{
    private bool movingToB = true;
    public float travelTime = 10;
    public float maxTravelTime;
    public float minTravelTime;
    private Vector3 upPosition;
    private Vector3 downPosition;
    private float t = 0f;
    bool movingUp;
    bool movingDown;
    bool isMovingPlatformHeightFixed;
    bool isFallingPlatformFixed;
    public MeshRenderer[] meshRenderers;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movingDown = false;
        movingUp = false;
        downPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        upPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 30, gameObject.transform.position.z);
        isMovingPlatformHeightFixed = false;
        isFallingPlatformFixed = false;
        foreach(MeshRenderer i in meshRenderers)
        {
            i.enabled = false;
        }
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
            travelTime = Random.Range(minTravelTime, maxTravelTime);
            movingUp = true;
            isMovingPlatformHeightFixed = false;
            isFallingPlatformFixed = false;
            foreach(MeshRenderer i in meshRenderers)
            {
                i.enabled = true;
            }
        }
        else if(levelActivate == false)
        {
            travelTime = Random.Range(minTravelTime, maxTravelTime);
            movingDown = true;
            isMovingPlatformHeightFixed = false;
            isFallingPlatformFixed = false;
        }
    }
    public void MoveUp()
    {
         t += Time.deltaTime / travelTime;

        float smoothT = Mathf.SmoothStep(0f,1f, t);
        if (gameObject.GetComponent<MovingPlatform>() != null && !isMovingPlatformHeightFixed)
        {
            MovingPlatform movingPlatform = GetComponent<MovingPlatform>();
            //annetaan joku arvo
            Vector3 desiredPointA = movingPlatform.pointAPosition;
            Vector3 desiredPointB = movingPlatform.pointBPosition;
            // annetaan oikea arvo
            //if (!isMovingPlatformHeightFixed)
            //{
                //desiredPointA = new Vector3(movingPlatform.pointAPosition.x,movingPlatform.pointAPosition.y + 20, movingPlatform.pointAPosition.z);
                //desiredPointB = new Vector3(movingPlatform.pointBPosition.x,movingPlatform.pointBPosition.y + 20, movingPlatform.pointBPosition.z);
                //isMovingPlatformHeightFixed = true;
                
            //}
            //movingPlatform.pointAPosition = Vector3.Lerp(movingPlatform.pointAPosition, desiredPointA, t);
            //movingPlatform.pointBPosition = Vector3.Lerp(movingPlatform.pointBPosition, desiredPointB, t);



            movingPlatform.pointAPosition = new Vector3(movingPlatform.pointAPosition.x,movingPlatform.pointAPosition.y + 30, movingPlatform.pointAPosition.z);
            movingPlatform.pointBPosition = new Vector3(movingPlatform.pointBPosition.x,movingPlatform.pointBPosition.y + 30, movingPlatform.pointBPosition.z);
            isMovingPlatformHeightFixed = true;
        }
        else
        {
            transform.position = Vector3.Lerp(downPosition, upPosition, t);
        }
        if(Vector3.Distance(transform.position, upPosition) < 0.1f)
        {
            movingUp = false;
        }
        if (gameObject.GetComponent<FallingPlatform>() != null && !isFallingPlatformFixed)
        {
            isFallingPlatformFixed = true;
            FallingPlatform fallingPlatform = GetComponent<FallingPlatform>();
            fallingPlatform.startPosition = new Vector3(fallingPlatform.startPosition.x, fallingPlatform.startPosition.y + 30, fallingPlatform.startPosition.z );
        }
    }
    public void MoveDown()
    {
        
         t += Time.deltaTime / travelTime;

        float smoothT = Mathf.SmoothStep(0f,1f, t);
        if (gameObject.GetComponent<MovingPlatform>() != null && !isMovingPlatformHeightFixed)
        {
            MovingPlatform movingPlatform = GetComponent<MovingPlatform>();
            movingPlatform.pointAPosition = new Vector3(movingPlatform.pointAPosition.x,movingPlatform.pointAPosition.y - 30, movingPlatform.pointAPosition.z);
            movingPlatform.pointBPosition = new Vector3(movingPlatform.pointBPosition.x,movingPlatform.pointBPosition.y - 30, movingPlatform.pointBPosition.z);
            isMovingPlatformHeightFixed = true;
        }
        else
        {
            transform.position = Vector3.Lerp(upPosition, downPosition, smoothT);
        }
        if(Vector3.Distance(transform.position, downPosition) < 0.1f)
        {
            movingDown = false;
            foreach(MeshRenderer i in meshRenderers)
            {
                i.enabled = false;
            }
        }
        if (gameObject.GetComponent<FallingPlatform>() != null && !isFallingPlatformFixed)
        {
            isFallingPlatformFixed = true;
            FallingPlatform fallingPlatform = GetComponent<FallingPlatform>();
            fallingPlatform.startPosition = new Vector3(fallingPlatform.startPosition.x, fallingPlatform.startPosition.y - 30, fallingPlatform.startPosition.z );
        }
        
    }

}
