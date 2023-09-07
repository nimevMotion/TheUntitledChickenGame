using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] m_AudioClips;
    [SerializeField]
    private bool m_canOpen = true;
    [SerializeField]
    private bool _isLock = false;
    [SerializeField]
    private GameObject m_UIDoor;

    private Animator m_animator;
    private AudioSource m_source;

    private GameObject _txtAbrir;
    private GameObject _txtBloqueo;

    private bool _isOpen = false;    
    private bool _playerFounded = false;

    private Ray rayfront;
    private Ray rayBack;
    private RaycastHit hitFront;
    private RaycastHit hitBack;

    private void Start()
    {
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
        Vector3 pivote = transform.position + transform.up + transform.right * -0.5f;
        rayfront = new Ray(pivote, transform.forward);
        rayBack = new Ray(pivote, transform.forward * -1);

        Debug.DrawRay(pivote, rayfront.direction * 0.5f, Color.yellow);
        Debug.DrawRay(pivote, rayBack.direction * 0.5f, Color.blue);
        
        

        if (!_isOpen)
        {
            if (Physics.Raycast(rayfront, out hitFront, 0.5f) &&
                hitFront.collider.gameObject.tag.Equals("Player"))
            {
                Debug.Log("hit:" + hitFront.collider.gameObject.name);
                if(Input.GetKeyDown(KeyCode.X))
                {
                    _playerFounded = true;
                    if(m_canOpen)
                    {
                        StartCoroutine("OpenDoor");
                    }
                    else
                    {
                        m_source.clip = m_AudioClips[2];
                        m_source.Play();
                        if (!_isLock)
                        {
                            _isLock = true;
                            _txtAbrir.SetActive(false);
                            _txtBloqueo.SetActive(true);
                        }
                        
                    }
                }
                
                
                m_UIDoor.SetActive(true);
            }
            else
            {
                m_UIDoor.SetActive(false);
        }

        if (Physics.Raycast(rayBack, out hitBack, 0.5f) && 
                hitBack.collider.gameObject.tag.Equals("Player"))
            {
                Debug.Log("hit:" + hitBack.collider.gameObject.name);
                if (Input.GetKeyDown(KeyCode.X))
                {
                    _playerFounded = true;
                    if (m_canOpen)
                    {
                        StartCoroutine("CloseDoor");
                    }
                    else
                    {
                        m_source.clip = m_AudioClips[2];
                        m_source.Play();
                        if (!_isLock)
                        {
                            _isLock = true;
                            _txtAbrir.SetActive(false);
                            _txtBloqueo.SetActive(true);
                        }

                    }
                }
                m_UIDoor.SetActive(true);
            }
            else
            {
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
        _isLock = false;
        Debug.Log("Se ha encontrado la llave");
        m_canOpen = true;
        _txtAbrir.SetActive(true);
        _txtBloqueo.SetActive(false);
    }
}
