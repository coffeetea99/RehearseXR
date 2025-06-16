using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

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
    
    private InputDevice leftController;

    // Audio

    private AudioSource audioSource;
    public AudioClip line2;     // 아니, 아직.
    public AudioClip line4;     // 어떡하지?
    public AudioClip line6;     // 미쳤어? 불을 켜면 어떡해!
    public AudioClip line9;     // 그런가...
    public AudioClip line12;    // 난 이쪽 찾아볼게.
    public AudioClip line14;    // 아니야. 우리가 찾는 건 빨간색인데 이건 붉은색이잖아.
    public AudioClip line19;    // 아무리 찾아도 나오질 않아.
    public AudioClip line21;    // 도대체 어디에 놔둔 거야.
    public AudioClip line23;    // 어쩌지, 아직 못찾았는데.

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
        eventSchedule.Add(new ScheduledEvent(4.5f, () => SetAction(TALKING)));
        eventSchedule.Add(new ScheduledEvent(4.5f, () => StartLine(line2)));    // 2
        eventSchedule.Add(new ScheduledEvent(6.5f, () => SetAction(IDLE)));

        eventSchedule.Add(new ScheduledEvent(10.5f, () => SetAction(TALKING)));
        eventSchedule.Add(new ScheduledEvent(10.5f, () => StartLine(line4)));   // 1.5
        eventSchedule.Add(new ScheduledEvent(12f, () => SetAction(IDLE)));

        eventSchedule.Add(new ScheduledEvent(22f, () => SetRotation(305f)));
        eventSchedule.Add(new ScheduledEvent(22f, () => SetAction(TALKING)));
        eventSchedule.Add(new ScheduledEvent(22f, () => StartLine(line6)));     // 3.5
        eventSchedule.Add(new ScheduledEvent(25.5f, () => SetAction(IDLE)));

        eventSchedule.Add(new ScheduledEvent(37.5f, () => SetAction(TALKING)));
        eventSchedule.Add(new ScheduledEvent(37.5f, () => StartLine(line9)));   // 1.5
        eventSchedule.Add(new ScheduledEvent(39f, () => SetAction(IDLE)));
        
        eventSchedule.Add(new ScheduledEvent(43.2f, () => SetRotation(180f)));
        eventSchedule.Add(new ScheduledEvent(43.2f, () => SetAction(WALKING)));
        eventSchedule.Add(new ScheduledEvent(44.2f, () => SetAction(IDLE)));

        eventSchedule.Add(new ScheduledEvent(52.5f, () => SetAction(CLICKING)));
        eventSchedule.Add(new ScheduledEvent(56.5f, () => SetAction(IDLE)));
        eventSchedule.Add(new ScheduledEvent(56.5f, () => SetRotation(90f)));
        eventSchedule.Add(new ScheduledEvent(57f, () => SetAction(SEARCHING)));
        eventSchedule.Add(new ScheduledEvent(58f, () => StartLine(line12)));    // 2.5

        eventSchedule.Add(new ScheduledEvent(65f, () => SetAction(IDLE)));
        eventSchedule.Add(new ScheduledEvent(65.5f, () => SetRotation(0f)));
        eventSchedule.Add(new ScheduledEvent(66f, () => SetAction(WALKING)));
        eventSchedule.Add(new ScheduledEvent(67f, () => SetAction(IDLE)));
        eventSchedule.Add(new ScheduledEvent(67.5f, () => SetRotation(270f)));
        eventSchedule.Add(new ScheduledEvent(68f, () => SetAction(TALKING)));
        eventSchedule.Add(new ScheduledEvent(68f, () => StartLine(line14)));    // 5
        eventSchedule.Add(new ScheduledEvent(73f, () => SetAction(IDLE)));

        eventSchedule.Add(new ScheduledEvent(93f, () => SetAction(TALKING)));
        eventSchedule.Add(new ScheduledEvent(93f, () => StartLine(line19)));    // 2.5
        eventSchedule.Add(new ScheduledEvent(95.5f, () => SetAction(IDLE)));

        eventSchedule.Add(new ScheduledEvent(102f, () => SetAction(TALKING)));
        eventSchedule.Add(new ScheduledEvent(102f, () => StartLine(line21)));   // 2.5
        eventSchedule.Add(new ScheduledEvent(104.5f, () => SetAction(IDLE)));

        eventSchedule.Add(new ScheduledEvent(108f, () => SetRotation(180f)));

        eventSchedule.Add(new ScheduledEvent(114.5f, () => SetAction(TALKING)));
        eventSchedule.Add(new ScheduledEvent(114.5f, () => StartLine(line23)));    // 3
        eventSchedule.Add(new ScheduledEvent(117.5f, () => SetAction(IDLE)));

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
            
            audioSource.Stop();

            // Reset scheduled events

            foreach (var e in eventSchedule)
            {
                e.done = false;
            }
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
