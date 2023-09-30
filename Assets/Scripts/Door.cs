using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool canOpen = true;
    public bool isLock = false;
    public int doorState = 0;

    [SerializeField]
    private AudioClip[] m_AudioClips;
    
    [SerializeField]
    private GameObject m_UIDoor;
    [SerializeField]
    private bool m_isBigDoor = false;
    [SerializeField]
    private Transform m_OpenPostion;
    [SerializeField]
    private bool m_secretDoor = false;

    private Animator m_animator;
    private AudioSource m_source;

    private GameObject _txtAbrir;
    private GameObject _txtBloqueo;

    private bool _isOpen = false;
    private bool _isPlayerDetected = false;

    private Ray rayfront;
    private Ray rayBack;
    private RaycastHit hitFront;
    private RaycastHit hitBack;

    private void Start()
    {
        if(!m_secretDoor)
            m_animator = GetComponent<Animator>();
        m_source = GetComponent<AudioSource>();
        
        foreach (Transform child in m_UIDoor.transform)
        {
            if (child.name.Equals("Txt_Abrir"))
                _txtAbrir = child.gameObject;
            else if (child.name.Equals("Txt_Bloqueo"))
                _txtBloqueo = child.gameObject;
        }

        _txtAbrir.SetActive(true);
        _txtBloqueo.SetActive(false);

    }
    void Update()
    {
        if(m_isBigDoor)
        {
            Vector3 pivote = transform.position + transform.up + transform.right * -2.5f;
            rayfront = new Ray(pivote, transform.forward);
            rayBack = new Ray(pivote, transform.forward * -1);

            Debug.DrawRay(pivote, rayfront.direction * 5f, Color.yellow);
            Debug.DrawRay(pivote, rayBack.direction * 5f, Color.blue);
        }else if(m_secretDoor)
        {
            float distance = Vector3.Distance(transform.position, m_OpenPostion.position);
            
            if(!isLock && distance > 0.0f)
            {
                transform.position = Vector3.MoveTowards(transform.position, m_OpenPostion.position, 0.05f);
            }
        }
        else
        {
            Vector3 pivote = transform.position + transform.up + transform.right * -0.5f;
            rayfront = new Ray(pivote, transform.forward);
            rayBack = new Ray(pivote, transform.forward * -1);

            Debug.DrawRay(pivote, rayfront.direction * 0.5f, Color.yellow);
            Debug.DrawRay(pivote, rayBack.direction * 0.5f, Color.blue);
        }
        
        
        if (!_isOpen && !m_secretDoor)
        {
            if (Physics.Raycast(rayfront, out hitFront, 0.5f) &&
                hitFront.collider.gameObject.tag.Equals("Player"))
            {
                doorState = 1;
                m_UIDoor.SetActive(true);
                _isPlayerDetected = true;
                //Debug.Log("hit:" + hitFront.collider.gameObject.name);
                if(Input.GetKeyDown(KeyCode.X))
                {
                    if(canOpen)
                    {
                        doorState = 2;
                        StartCoroutine("OpenDoor");
                    }
                    else
                    {
                        m_source.clip = m_AudioClips[2];
                        m_source.Play();
                        if (!isLock)
                        {
                            doorState = 3;
                            isLock = true;
                            _txtAbrir.SetActive(false);
                            _txtBloqueo.SetActive(true);
                        }
                        
                    }
                }
            }
            else if (Physics.Raycast(rayBack, out hitBack, 0.5f) && 
                    hitBack.collider.gameObject.tag.Equals("Player"))
            {
                m_UIDoor.SetActive(true);
               //Debug.Log("hit:" + hitBack.collider.gameObject.name);
                if (Input.GetKeyDown(KeyCode.X))
                {
                    doorState = 1;
                    if (canOpen)
                    {   doorState = 2;
                        StartCoroutine("CloseDoor");
                    }
                    else
                    {
                        m_source.clip = m_AudioClips[2];
                        m_source.Play();
                        
                        if (!isLock)
                        {
                            doorState = 3;
                            isLock = true;
                            _txtAbrir.SetActive(false);
                            _txtBloqueo.SetActive(true);
                        }

                    }
                }
            }
            else if(_isPlayerDetected)
            {
                //Debug.Log("desactivo mensaje de la puerta");
                m_UIDoor.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            GotKey();
        }

    }

    IEnumerator OpenDoor()
    {
        _isOpen = true;
        m_UIDoor.SetActive(false);
        m_animator.SetTrigger("open");
        m_source.clip = m_AudioClips[0];
        m_source.Play();
        yield return new WaitForSeconds(5);
        
        m_animator.SetTrigger("close");
        m_source.clip = m_AudioClips[1];
        m_source.Play();
        _isOpen = false;
    }

    IEnumerator CloseDoor()
    {
        _isOpen = true;
        m_UIDoor.SetActive(false);
        m_animator.SetTrigger("close");
        m_source.clip = m_AudioClips[0];
        m_source.Play();
        yield return new WaitForSeconds(5);

        m_animator.SetTrigger("open");
        m_source.clip = m_AudioClips[1];
        m_source.Play();
        _isOpen = false;
    }

    public void GotKey()
    {
        isLock = false;
        //Debug.Log("Se ha encontrado la llave");
        canOpen = true;
        _txtAbrir.SetActive(true);
        _txtBloqueo.SetActive(false);
    }
}
