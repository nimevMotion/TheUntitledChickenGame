using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public const string DOOR_0 = "Open Door\nPress X";
    public const string DOOR_1 = "Door is lock";

    public bool canOpen = true;
    public bool isLock = false;
    public bool isPlayerDetected = false;
    public DoorState doorState;

    [SerializeField]
    private AudioClip[] m_AudioClips;
    [SerializeField]
    private bool m_isBigDoor = false;
    [SerializeField]
    private Transform m_OpenPostion;
    [SerializeField]
    private bool m_secretDoor = false;

    private Animator m_animator;
    private AudioSource m_source;

    private Vector3 _hitNormal;
    private string _doorInfo;

    private void Start()
    {
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
            switch(doorState)
            {
                case DoorState.discovered:
                    if(Input.GetKeyDown(KeyCode.X))
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
                    if (Input.GetKeyDown(KeyCode.X))
                    {
                        StartCoroutine("OpenDoor");
                    }
                    else if(m_secretDoor)
                    {
                        float distance = Vector3.Distance(transform.position, m_OpenPostion.position);

                        if (distance > 0.0f)
                            transform.position = Vector3.MoveTowards(transform.position, m_OpenPostion.position, 0.05f);
                    }
                    break;
            }
        }

        //if(m_isBigDoor)
        //{
        //    Vector3 pivote = transform.position + transform.up + transform.right * -2.5f;
        //    rayfront = new Ray(pivote, transform.forward);
        //    rayBack = new Ray(pivote, transform.forward * -1);

        //    Debug.DrawRay(pivote, rayfront.direction * 5f, Color.yellow);
        //    Debug.DrawRay(pivote, rayBack.direction * 5f, Color.blue);
        //}else if(m_secretDoor)
        //{
        //    float distance = Vector3.Distance(transform.position, m_OpenPostion.position);

        //    if(!isLock && distance > 0.0f)
        //    {
        //        transform.position = Vector3.MoveTowards(transform.position, m_OpenPostion.position, 0.05f);
        //    }
        //}
        //else
        //{
        //    Vector3 pivote = transform.position + transform.up + transform.right * -0.5f;
        //    rayfront = new Ray(pivote, transform.forward);
        //    rayBack = new Ray(pivote, transform.forward * -1);

        //    Debug.DrawRay(pivote, rayfront.direction * 0.5f, Color.yellow);
        //    Debug.DrawRay(pivote, rayBack.direction * 0.5f, Color.blue);
        //}


        //if (!_isOpen && !m_secretDoor)
        //{
        //    if (Physics.Raycast(rayfront, out hitFront, 0.5f) &&
        //        hitFront.collider.gameObject.tag.Equals("Player"))
        //    {
        //        doorState = 1;
        //        m_UIDoor.SetActive(true);
        //        _isPlayerDetected = true;
        //        //Debug.Log("hit:" + hitFront.collider.gameObject.name);
        //        if(Input.GetKeyDown(KeyCode.X))
        //        {
        //            if(canOpen)
        //            {
        //                doorState = 2;
        //                StartCoroutine("OpenDoor");
        //            }
        //            else
        //            {
        //                m_source.clip = m_AudioClips[2];
        //                m_source.Play();
        //                if (!isLock)
        //                {
        //                    doorState = 3;
        //                    isLock = true;
        //                    _doorInfo = DOOR_1;
        //                    _txtAbrir.SetActive(false);
        //                    _txtBloqueo.SetActive(true);
        //                }

        //            }
        //        }
        //    }
        //    else if (Physics.Raycast(rayBack, out hitBack, 0.5f) && 
        //            hitBack.collider.gameObject.tag.Equals("Player"))
        //    {
        //        m_UIDoor.SetActive(true);
        //       //Debug.Log("hit:" + hitBack.collider.gameObject.name);
        //        if (Input.GetKeyDown(KeyCode.X))
        //        {
        //            doorState = 1;
        //            if (canOpen)
        //            {   doorState = 2;
        //                StartCoroutine("CloseDoor");
        //            }
        //            else
        //            {
        //                m_source.clip = m_AudioClips[2];
        //                m_source.Play();

        //                if (!isLock)
        //                {
        //                    doorState = 3;
        //                    isLock = true;
        //                    _txtAbrir.SetActive(false);
        //                    _txtBloqueo.SetActive(true);
        //                }

        //            }
        //        }
        //    }
        //    else if(_isPlayerDetected)
        //    {
        //        //Debug.Log("desactivo mensaje de la puerta");
        //        m_UIDoor.SetActive(false);
        //    }
        //}

        if (Input.GetKeyDown(KeyCode.Y))
        {
            GotKey();
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

    public void GotKey()
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

    public enum DoorState
    {
        undiscovered,
        discovered,
        block, 
        unlock
    }
}
