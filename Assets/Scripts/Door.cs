using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public const string DOOR_0 = "Open Door\nPress Q";
    public const string DOOR_1 = "Door is lock";
    public const string DOOR_2 = "Press Q to use key";
    public const string DOOR_3 = "Can't open door\nFind all <b>chicks</b>";

    public bool canOpen = true;
    public bool isLock = false;
    public bool isPlayerDetected = false;
    public bool isOpen = false;
    public DoorState doorState;

    [SerializeField]
    private AudioClip[] m_AudioClips;
    [SerializeField]
    private bool m_isBigDoor = false;
    [SerializeField]
    private Transform m_OpenPostion;
    [SerializeField]
    private bool m_secretDoor = false;

    private GameManager _gameManager;
    private Animator m_animator;
    private AudioSource m_source;
    private Vector3 _hitNormal;
    
    private bool _isFinal = false;
    private string _doorInfo;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if(!m_secretDoor)
            m_animator = GetComponent<Animator>();
        m_source = GetComponent<AudioSource>();

        _doorInfo = DOOR_0;
        doorState = DoorState.undiscovered;
    }
    void Update()
    {
        if(isPlayerDetected)
        {
            DoorStateFunction();
        }

    }

    private void DoorStateFunction()
    {
        switch (doorState)
        {
            case DoorState.discovered:
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    if (canOpen)
                    {
                        doorState = DoorState.unlock;
                        StartCoroutine("OpenDoor");
                    }
                    else
                    {
                        m_source.clip = m_AudioClips[2];
                        m_source.Play();
                        doorState = DoorState.block;
                        _doorInfo = DOOR_1;
                    }


                }
                break;
            case DoorState.unlock:
                if (Input.GetKeyDown(KeyCode.Q) && !m_secretDoor)
                {
                    StartCoroutine("OpenDoor");
                }
                else if (m_secretDoor)
                {
                    float distance = Vector3.Distance(transform.position, m_OpenPostion.position);

                    if (distance > 0.0f)
                        transform.position = Vector3.MoveTowards(transform.position, m_OpenPostion.position, 0.05f);
                    if (isOpen)
                    {
                        isOpen = false;
                        m_source.Play();
                    }
                }
                break;

            case DoorState._lock:
                _doorInfo = DOOR_2;
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    GotKey();
                }
                break;
        }
    }

    IEnumerator OpenDoor()
    {
        bool open = false;
        if (Vector3.Angle(_hitNormal,transform.forward) < 90.0f)
            open = true;

        if(m_isBigDoor)
        { 
            m_animator.SetBool("inFront", open);
            m_animator.SetTrigger("open");
            m_source.clip = m_AudioClips[0];
            m_source.Play();
            yield return new WaitForSeconds(5);

            m_animator.SetTrigger("close");
            m_source.clip = m_AudioClips[1];
            m_source.Play();
        }else if(_isFinal)
        {
            m_animator.SetTrigger(open ? "open" : "close");
            m_source.clip = m_AudioClips[0];
            m_source.Play();
            _gameManager.StartCinematic();
        }
        else
        {
            m_animator.SetTrigger(open ? "open": "close");
            m_source.clip = m_AudioClips[0];
            m_source.Play();
            yield return new WaitForSeconds(5);

            m_animator.SetTrigger(open ? "close" : "open");
            m_source.clip = m_AudioClips[1];
            m_source.Play();
        }

    }

    private void GotKey()
    {
        isLock = false;
        canOpen = true;
        _doorInfo = DOOR_0;
        doorState = DoorState.unlock;
    }

    public string GetDoorInfo(Vector3 hitNormal)
    {
        _hitNormal = hitNormal;
        if (doorState == DoorState.undiscovered)
            doorState = DoorState.discovered;
        return _doorInfo;
    }

    public void OpenSecretDoor()
    {
        m_secretDoor = true;
        doorState = DoorState.unlock;
    }

    public void CanOpenFinalDoor()
    {
        if(_gameManager.numPollitos == 0)
        {
           GotKey();
            _isFinal = true;
        }
        
    }

    public enum DoorState
    {
        undiscovered,
        discovered,
        block, 
        unlock,
        _lock
    }
}
