using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

// Character A

public class Character2Action : MonoBehaviour
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
    
    private InputDevice leftController;

    // Audio

    private AudioSource audioSource;
    public AudioClip line1;     // 찾았어?
    public AudioClip line3;     // 큰일났네.
    public AudioClip line7;     // 맞아! 누가 보면 어떡하려고!
    public AudioClip line11;    // 여기 있나?
    public AudioClip line16;    // 어디 있는 거야 진짜.
    public AudioClip line18;    // 응. 없어.
    public AudioClip line22;    // 큰일났다!
    public AudioClip line25;    // 여기만 보고 갈게...!

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
        eventSchedule.Add(new ScheduledEvent(2f, () => SetAction(TALKING)));
        eventSchedule.Add(new ScheduledEvent(2f, () => StartLine(line1)));      // 1.5
        eventSchedule.Add(new ScheduledEvent(3.5f, () => SetAction(IDLE)));

        eventSchedule.Add(new ScheduledEvent(7.5f, () => SetAction(TALKING)));
        eventSchedule.Add(new ScheduledEvent(7.5f, () => StartLine(line3)));    // 2
        eventSchedule.Add(new ScheduledEvent(9.5f, () => SetAction(IDLE)));

        eventSchedule.Add(new ScheduledEvent(26.5f, () => SetRotation(225f)));
        eventSchedule.Add(new ScheduledEvent(26.5f, () => SetAction(TALKING)));
        eventSchedule.Add(new ScheduledEvent(26.5f, () => StartLine(line7)));   // 4
        eventSchedule.Add(new ScheduledEvent(30.5f, () => SetAction(IDLE)));

        eventSchedule.Add(new ScheduledEvent(43f, () => SetRotation(0f)));
        eventSchedule.Add(new ScheduledEvent(43f, () => SetAction(WALKING)));
        eventSchedule.Add(new ScheduledEvent(44f, () => SetAction(IDLE)));

        eventSchedule.Add(new ScheduledEvent(44.5f, () => SetAction(CLICKING)));
        eventSchedule.Add(new ScheduledEvent(48.5f, () => SetAction(IDLE)));
        eventSchedule.Add(new ScheduledEvent(48.5f, () => SetRotation(90f)));
        eventSchedule.Add(new ScheduledEvent(49f, () => SetAction(SEARCHING)));
        eventSchedule.Add(new ScheduledEvent(50f, () => StartLine(line11)));   // 1.5


        eventSchedule.Add(new ScheduledEvent(77f, () => SetAction(IDLE)));
        eventSchedule.Add(new ScheduledEvent(77.5f, () => SetRotation(0f)));
        eventSchedule.Add(new ScheduledEvent(78f, () => SetAction(CLICKING)));
        eventSchedule.Add(new ScheduledEvent(82f, () => SetAction(IDLE)));
        eventSchedule.Add(new ScheduledEvent(82.5f, () => SetRotation(180f)));
        eventSchedule.Add(new ScheduledEvent(83f, () => SetAction(WALKING)));
        eventSchedule.Add(new ScheduledEvent(83f, () => StartLine(line16)));      // 2.5
        eventSchedule.Add(new ScheduledEvent(84f, () => SetAction(IDLE)));

        eventSchedule.Add(new ScheduledEvent(89.5f, () => SetAction(TALKING)));
        eventSchedule.Add(new ScheduledEvent(89.5f, () => StartLine(line18)));    // 2.5
        eventSchedule.Add(new ScheduledEvent(92f, () => SetAction(IDLE)));

        eventSchedule.Add(new ScheduledEvent(107.5f, () => SetRotation(0f)));

        eventSchedule.Add(new ScheduledEvent(111.5f, () => SetAction(TALKING)));
        eventSchedule.Add(new ScheduledEvent(111.5f, () => StartLine(line22)));    // 2
        eventSchedule.Add(new ScheduledEvent(113.5f, () => SetAction(IDLE)));

        eventSchedule.Add(new ScheduledEvent(122.5f, () => SetAction(TALKING)));
        eventSchedule.Add(new ScheduledEvent(122.5f, () => StartLine(line25)));    // 2.5
        eventSchedule.Add(new ScheduledEvent(125f, () => SetAction(IDLE)));

        eventSchedule.Add(new ScheduledEvent(131f, () => SetRotation(161.32f)));
        eventSchedule.Add(new ScheduledEvent(131f, () => SetAction(RUNNING)));
        eventSchedule.Add(new ScheduledEvent(135f, () => SetAction(IDLE)));

        /*
        Formats
        eventSchedule.Add(new ScheduledEvent(f, () => SetAction()));
        eventSchedule.Add(new ScheduledEvent(f, () => StartLine(line)));
        eventSchedule.Add(new ScheduledEvent(f, () => SetRotation(f)));
        */
        
        // Find the left-hand controller
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, devices);

        if (devices.Count > 0)
        {
            leftController = devices[0];
            Debug.Log("Left controller found: " + leftController.name);
        }
        else
        {
            Debug.LogWarning("Left controller not found.");
        }
    }

    // Update is called once per frame
    void Update()
    {
    #if UNITY_EDITOR
        // Debug key for simulating Y button press in Editor
        if (Input.GetKeyDown(KeyCode.Y))
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
    #else
        if (!leftController.isValid)
            return;

        if (leftController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool yButtonPressed) && yButtonPressed)
        {
            Transform transform = GetComponent<Transform>();
            transform.position = initialPosition;
            transform.rotation = initialRotation;

            SetAction(IDLE);
            timer = 0f;
            beforeTime = 0f;
        }
    #endif

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
