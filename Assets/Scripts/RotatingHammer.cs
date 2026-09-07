using UnityEngine;

public class RotatingHammer : MonoBehaviour
{
    public float swingAngle = 60f;
    public float speed = 1f;
    public AnimationCurve swingCurve;

    void Update()
    {
        float t = (Time.time * speed) % 2f;

        float curveTime;

        if (t <= 1f)
            curveTime = t;
        else
            curveTime = 2f - t;

        float angle = swingCurve.Evaluate(curveTime) * swingAngle;

        transform.localRotation = Quaternion.Euler(0,0,angle);
    }
}