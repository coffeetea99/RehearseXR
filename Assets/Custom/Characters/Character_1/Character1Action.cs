using UnityEngine;
public class Character1Action : MonoBehaviour
{
    private const int IDLE = 0;
    private const int TALKING = 1;
    private const int WALKING = 2;
    private const int RUNNING = 3;
    private const int SEARCHING = 4;
    private const int CLICKING = 5;

    public Animator animator;

    private float timer = 0f;
    private float beforeTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        beforeTime = timer;
        timer += Time.deltaTime;

        // TODO: fix
        if (DidTimePass(7))
        {
            SetAction(TALKING);
        }
        else if (DidTimePass(10))
        {
            SetAction(IDLE);
        }
    }

    bool DidTimePass(float referenceTime)
    {
        return beforeTime < referenceTime && referenceTime <= timer;
    }

    void SetAction(int action)
    {
        animator.SetInteger("Action", action);
    }
}
