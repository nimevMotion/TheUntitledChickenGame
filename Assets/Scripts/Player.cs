using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /*Public*/
    public float speed;
    public float angleRot = 30.0f;
    public bool isRotating = false;
    public string turn = "";
    public int chocolateBar;
    public int life = 50;

    /*Serialized var*/
    [SerializeField]
    private GameObject m_AimCamera;
    [SerializeField]
    private GameObject m_MainCamera;
    [SerializeField]
    private GameObject m_Bullet;
    [SerializeField]
    private GameObject m_AimOrigin;
    [SerializeField]
    private float m_shootForce;
    [SerializeField]
    private AudioClip[] m_SoundsPlayer;
    [SerializeField]
    private float speedRot;
    [SerializeField]
    private ParticleSystem m_MuzzleFlash;
    [SerializeField]
    private GameObject m_ImpactEffect;
    /*Private*/
    private Animator m_Animator;
    private AnimatorClipInfo[] m_CurrentClipInfo;
    private AudioSource m_audioPlayer;
    private Rigidbody m_RBBullet;
    private Transform mAim;
    private Transform mplayerMesh;

    private GameManager _gameManager;
    private LookX _lookX;

    private string clipName;

    private float horizontalInput;
    private float verticalInput;
    private float range = 100.0f;

    private Vector3 newRotation;

   // private bool m_IsPlaying = false;
    private bool isRunning = false;
    private bool isAimming = false;
    private bool haveGun = true;
    private bool isMoving = false;
    private bool isSideWalk = false;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.name.Equals("Player"))
                mplayerMesh = child;
            else if (child.name.Equals("Aim"))
                mAim = child;
        }

        m_Animator = GetComponentInChildren<Animator>();
        _lookX = GetComponentInChildren<LookX>();
        m_audioPlayer = GetComponent<AudioSource>();
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(_gameManager.isGameOn)
        if (true)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
            if (isRotating)
            {
                horizontalInput = Input.GetAxis("Mouse X") * 0.5f;
            }

            Move();

            //Aimming
            if (Input.GetMouseButtonDown(1))
            {
                m_Animator.SetBool("isAimming", true);
                isAimming = true;
            }
            else if (Input.GetMouseButtonUp(1))
            {
                m_Animator.SetBool("isAimming", false);
                isAimming = false;
            }

            if (isAimming)
            {
                m_MainCamera.SetActive(false);
                m_AimCamera.SetActive(true);
            }
            else
            {
                m_MainCamera.SetActive(true);
                m_AimCamera.SetActive(false);
            }
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("Shoot");
                StartCoroutine("Shoot");
            }

            if (Input.GetKeyDown(KeyCode.LeftControl))
                isRunning = true;
            else if(Input.GetKeyUp(KeyCode.LeftControl))
                isRunning = false;

            if (Input.GetKeyDown(KeyCode.LeftShift))
                isSideWalk = true;
            else if (Input.GetKeyUp(KeyCode.LeftShift))
                isSideWalk = false;

        }

    }

    private void Move()
    {
        //transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput, Space.World);
        if(isSideWalk)
            transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
        else
            transform.Rotate(Vector3.up * Time.deltaTime * speed * 50 * horizontalInput, Space.Self);

        transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);


        //Inicia Controles y movimiento
        //if (verticalInput > 0.0f)
        //{
        //    if ((isRunning || haveGun) && !isAimming)
        //        m_Animator.SetInteger("State", 2);
        //    else
        //        m_Animator.SetInteger("State", 1);
        //}
        //else if (verticalInput < 0.0f)
        //{
        //    if (isRunning || haveGun)
        //        m_Animator.SetInteger("State", -2);
        //    else
        //        m_Animator.SetInteger("State", -1);
        //}

        if (verticalInput > 0.0f)
        {
            if (isRunning)
                m_Animator.SetInteger("State", 2);
            else
                m_Animator.SetInteger("State", 1);
        }
        else if (verticalInput < 0.0f)
        {
            m_Animator.SetInteger("State", -1);
        }

        if (horizontalInput != 0.0f)
        { 
            if (isSideWalk)
            {
                if (horizontalInput < 0.0f)
                    m_Animator.SetInteger("State", -3);
                else if (horizontalInput > 0.0f)
                    m_Animator.SetInteger("State", 3);
            }
            else
                m_Animator.SetInteger("State", 1);
        }

        if (horizontalInput == 0.0f && verticalInput == 0.0f)
        {
            m_Animator.SetInteger("State", 0);
        }
    }

    public void RecoverHealth(int cure)
    {
        life = life + cure; 
    }

    IEnumerator Shoot()
    {
        m_MuzzleFlash.Play();
        
        m_Animator.SetTrigger("shoot");
        RaycastHit hit;

        if(Physics.Raycast(m_MainCamera.transform.position, m_MainCamera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            GhostManager ghost = hit.transform.GetComponent<GhostManager>();
            if(ghost != null)
            {
                ghost.Damage(50.0f);
            }
            Debug.Log("va a disparar");
            yield return new WaitForSeconds(1.0f);
            GameObject impactEffect = Instantiate(m_ImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactEffect,1.0f);
            Debug.Log("disparo");
        }
    }

}
