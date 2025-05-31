using UnityEngine;

// Character C

public class Character1Action : MonoBehaviour
{
    private const int IDLE = 0;
    private const int TALKING = 1;
    private const int WALKING = 2;
    private const int RUNNING = 3;
    private const int SEARCHING = 4;
    private const int CLICKING = 5;

    // Animation

    public Animator animator;

    private float timer = 0f;
    private float beforeTime = 0f;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    // Audio

    private AudioSource audioSource;
    public AudioClip line5;
    public AudioClip line8;
    public AudioClip line10;
    public AudioClip line13;
    public AudioClip line15;
    public AudioClip line17;
    public AudioClip line20;
    public AudioClip line24;
    public AudioClip line26;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        Transform transform = GetComponent<Transform>();
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Transform transform = GetComponent<Transform>();
            transform.position = initialPosition;
            transform.rotation = initialRotation;

            SetAction(IDLE);
            timer = 0f;
            beforeTime = 0f;
            
            audioSource.Stop();
        }

        beforeTime = timer;
        timer += Time.deltaTime;

        // TODO: fix
        if (DidTimePass(1))
        {
            SetAction(WALKING);
            StartLine(line5);
            SetRotation(270f);
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

    void StartLine(AudioClip line)
    {
        audioSource.clip = line;
        audioSource.Play();
    }

    void SetRotation(float rotationY)
    // Heading left = 0, back = 90, right = 180, front = 270
    {
        transform.rotation = Quaternion.Euler(0f, rotationY, 0f);
    }
}
