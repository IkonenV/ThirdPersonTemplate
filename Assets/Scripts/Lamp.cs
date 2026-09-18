using UnityEngine;

public class Lamp : MonoBehaviour
{
    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        float swingSpeed = Random.Range(0.35f,0.45f);
        animator.SetFloat("Speed",swingSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
