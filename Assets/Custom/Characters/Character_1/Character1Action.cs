using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

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
    
    private InputDevice leftController;

    // Audio

    private AudioSource audioSource;
    public AudioClip line5;     // 그냥 불 켜고 찾아.
    public AudioClip line8;     // 그렇게 시끄럽게 부스럭대는 것보단, 불 켜고 빨리 찾는 게 나아.
    public AudioClip line10;    // 그러니까 빨리 찾아.
    public AudioClip line13;    // 어? 이거 아냐?
    public AudioClip line15;    // 뭐가 다른 거야…?
    public AudioClip line17;    // 저쪽은 찾아봤어?
    public AudioClip line20;    // 하, 어떡하지. 시간이 없다, 시간이.
    public AudioClip line24;    // 지금 그게 중요해? 뛰어!
    public AudioClip line26;    // 그럴 시간 없다고! 빨리 나와!

    public AudioClip warning;

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
        eventSchedule.Add(new ScheduledEvent(12f, () => SetAction(RUNNING)));
        eventSchedule.Add(new ScheduledEvent(13.7f, () => SetAction(IDLE)));
        eventSchedule.Add(new ScheduledEvent(14f, () => SetAction(CLICKING)));

        eventSchedule.Add(new ScheduledEvent(18f, () => SetAction(IDLE)));
        eventSchedule.Add(new ScheduledEvent(18.5f, () => SetRotation(90f)));
        eventSchedule.Add(new ScheduledEvent(19f, () => SetAction(TALKING)));
        eventSchedule.Add(new ScheduledEvent(19f, () => StartLine(line5)));     // 2
        eventSchedule.Add(new ScheduledEvent(21f, () => SetAction(IDLE)));

        eventSchedule.Add(new ScheduledEvent(31.5f, () => SetAction(TALKING)));
        eventSchedule.Add(new ScheduledEvent(31.5f, () => StartLine(line8)));   // 5
        eventSchedule.Add(new ScheduledEvent(36.5f, () => SetAction(IDLE)));

        eventSchedule.Add(new ScheduledEvent(40f, () => SetAction(TALKING)));
        eventSchedule.Add(new ScheduledEvent(40f, () => StartLine(line10)));    // 2
        eventSchedule.Add(new ScheduledEvent(42f, () => SetAction(IDLE)));

        eventSchedule.Add(new ScheduledEvent(60f, () => SetRotation(270f)));
        eventSchedule.Add(new ScheduledEvent(61f, () => SetAction(SEARCHING)));
        eventSchedule.Add(new ScheduledEvent(61.5f, () => StartLine(line13)));  // 2.5
        eventSchedule.Add(new ScheduledEvent(67.5f, () => SetAction(IDLE)));

        eventSchedule.Add(new ScheduledEvent(74f, () => SetAction(TALKING)));
        eventSchedule.Add(new ScheduledEvent(74f, () => StartLine(line15)));    // 2
        eventSchedule.Add(new ScheduledEvent(76f, () => SetAction(IDLE)));

        eventSchedule.Add(new ScheduledEvent(86.5f, () => SetRotation(0f)));
        eventSchedule.Add(new ScheduledEvent(86.5f, () => SetAction(TALKING)));
        eventSchedule.Add(new ScheduledEvent(86.5f, () => StartLine(line17)));  // 2
        eventSchedule.Add(new ScheduledEvent(87.5f, () => SetAction(IDLE)));

        eventSchedule.Add(new ScheduledEvent(96.5f, () => SetRotation(90f)));
        eventSchedule.Add(new ScheduledEvent(96.5f, () => SetAction(TALKING)));
        eventSchedule.Add(new ScheduledEvent(96.5f, () => StartLine(line20)));  // 4.5
        eventSchedule.Add(new ScheduledEvent(101f, () => SetAction(IDLE)));

        eventSchedule.Add(new ScheduledEvent(106f, () => StartLine(warning)));  // 4.5
        eventSchedule.Add(new ScheduledEvent(107f, () => SetRotation(270f)));

        eventSchedule.Add(new ScheduledEvent(118.5f, () => SetAction(TALKING)));
        eventSchedule.Add(new ScheduledEvent(118.5f, () => StartLine(line24))); // 3
        eventSchedule.Add(new ScheduledEvent(121.5f, () => SetAction(IDLE)));

        eventSchedule.Add(new ScheduledEvent(126f, () => SetAction(TALKING)));
        eventSchedule.Add(new ScheduledEvent(126f, () => StartLine(line26)));   // 3.5
        eventSchedule.Add(new ScheduledEvent(129.5f, () => SetAction(IDLE)));

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
