using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    public float timeBetweenRotations = 2f;
    public float rotationDuration = 0.5f;

    private float timer = 0f;
    private float rotationTimer = 0f;
    private bool rotating = false;

    private Quaternion startRotation;
    private Quaternion targetRotation;
    Void voidObject;
    void Start()
    {
        voidObject = GameObject.FindGameObjectWithTag("Void").GetComponent<Void>();
    }

    void Update()
    {
        if (!rotating)
        {
            timer += Time.deltaTime;

            if (timer >= timeBetweenRotations)
            {
                timer = 0f;
                rotating = true;
                rotationTimer = 0f;

                startRotation = transform.rotation;
                targetRotation = startRotation * Quaternion.Euler(180f, 0f, 0f);
            }
        }
        else
        {
            rotationTimer += Time.deltaTime;

            float t = rotationTimer / rotationDuration;

            // SmoothStep tekee liikkeestä pehmeän
            t = Mathf.SmoothStep(0f, 1f, t);

            transform.rotation = Quaternion.Slerp(
                startRotation,
                targetRotation,
                t
            );

            if (rotationTimer >= rotationDuration)
            {
                transform.rotation = targetRotation;
                rotating = false;
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            voidObject.Death();
        }
    }
}
