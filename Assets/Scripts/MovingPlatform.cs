using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public Vector3 pointAPosition;
    public Vector3 pointBPosition;
    private Vector3 lastPosition;
    private float t = 0f;
    private bool movingToB = true;
    public float travelTime;
    public Vector3 platformMovement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pointAPosition = pointA.position;
        pointBPosition = pointB.position;
        lastPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime / travelTime;

        float smoothT = Mathf.SmoothStep(0f,1f, t);

        if (movingToB)
        {
            transform.position = Vector3.Lerp(pointAPosition, pointBPosition, smoothT);
        }
        else
        {
            transform.position = Vector3.Lerp(pointBPosition, pointAPosition, smoothT);
        }
        if(t >= 1f)
        {
            t=0f;
            movingToB = !movingToB;
        }
        platformMovement = transform.position - lastPosition;
        lastPosition = transform.position;

    }
}
