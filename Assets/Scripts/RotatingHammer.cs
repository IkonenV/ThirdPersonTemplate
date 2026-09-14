using UnityEngine;

public class RotatingHammer : MonoBehaviour
{
    GameObject player;
    public GameObject hitPoint;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 direction = player.transform.position - hitPoint.transform.position;
            player.GetComponent<ThirdPersonController>().ApplyKnockback(direction);
        }
    }
}