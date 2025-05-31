using System;
using System.Collections.Generic;
using UnityEngine;

// Character B

public class Character3Action : MonoBehaviour
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
    public AudioClip line2;
    public AudioClip line4;
    public AudioClip line6;
    public AudioClip line9;
    public AudioClip line12;
    public AudioClip line14;
    public AudioClip line19;
    public AudioClip line21;
    public AudioClip line23;

    private List<ScheduledEvent> eventSchedule = new();

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

        // TODO: fix
        eventSchedule.Add(new ScheduledEvent(3f, () => SetAction(TALKING)));
        eventSchedule.Add(new ScheduledEvent(3f, () => StartLine(line2)));
        eventSchedule.Add(new ScheduledEvent(3f, () => SetRotation(90f)));

        /*
        Formats
        eventSchedule.Add(new ScheduledEvent(f, () => SetAction()));
        eventSchedule.Add(new ScheduledEvent(f, () => StartLine(line)));
        eventSchedule.Add(new ScheduledEvent(f, () => SetRotation(f)));
        */
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

            // Reset scheduled events

            foreach (var e in eventSchedule)
            {
                e.done = false;
            }
        }

        beforeTime = timer;
        timer += Time.deltaTime;

        foreach (var e in eventSchedule)
        {
            if (!e.done && DidTimePass(e.time))
            {
                e.Invoke();
            }
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

    private class ScheduledEvent
    {
        public float time;
        public Action action;
        public bool done = false;

        public ScheduledEvent(float time, Action action)
        {
            this.time = time;
            this.action = action;
        }

        public void Invoke()
        {
            done = true;
            action?.Invoke();
        }
    }
}
