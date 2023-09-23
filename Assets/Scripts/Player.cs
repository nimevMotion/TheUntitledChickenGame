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

    private Vector3 newRotation;

   // private bool m_IsPlaying = false;
    private bool isRunning = false;
    private bool isAimming = false;
    private bool haveGun = true;
    private bool isMoving = false;

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
            if(isRotating)
            {
                horizontalInput = Input.GetAxis("Mouse X") * 0.5f;
            }

            Move();

        }
        //m_CurrentClipInfo = m_Animator.GetCurrentAnimatorClipInfo(0);
        //clipName = m_CurrentClipInfo[0].clip.name;

        //horizontalInput = Input.GetAxis("Horizontal");
        //verticalInput = Input.GetAxis("Vertical");

        //if((verticalInput != 0 || horizontalInput != 0) && !isMoving)
        //{
        //    m_audioPlayer.Play();
        //}

        //if(verticalInput != 0 || horizontalInput != 0)
        //    isMoving = true;

        //newRotation = transform.localEulerAngles;
        //newRotation.y = mplayerMesh.transform.localEulerAngles.y;

        //transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
        //transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);

        ////Inicia Controles y movimiento
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

        //if (horizontalInput > 0.0f)
        //    m_Animator.SetInteger("State", 3);
        //else if (horizontalInput < 0.0f)
        //    m_Animator.SetInteger("State", -3);

        //if (horizontalInput == 0.0f && verticalInput == 0.0f)
        //{
        //    m_Animator.SetInteger("State", 0);
        //    m_audioPlayer.Stop();
        //    isMoving = false;
        //}

        //if (horizontalInput != 0.0f || verticalInput != 0.0f)
        //{
        //    if (Input.GetKeyDown(KeyCode.LeftShift))
        //    {
        //        isRunning = true;
        //        speed = 5.0f;
        //    }
        //    else if (Input.GetKeyUp(KeyCode.LeftShift))
        //    {
        //        Debug.Log("Stop running..........................");
        //        isRunning = false;
        //        speed = 1.0f;
        //    }
        //}

        //if (isRotating)
        //{
        //    RotatePlayer();
        //}

        ////Shoot
        //if (Input.GetMouseButtonDown(0))
        //{
        //    m_Animator.SetTrigger("shoot");
        //    float x = Screen.width / 2;
        //    float y = Screen.height / 2;

        //    //Ray ray = m_Camera.ScreenPointToRay(new Vector3(x,y,0));

        //    //Debug.DrawRay(mAim.position, ray.direction * 10, Color.yellow);
        //    m_RBBullet = Instantiate(m_Bullet, m_AimOrigin.transform.position, Quaternion.identity)
        //        .GetComponent<Rigidbody>();
        //    //m_audioPlayer.clip = m_SoundsPlayer[0];
        //    //m_audioPlayer.Play();
        //    m_RBBullet.AddForce((mAim.position - m_AimOrigin.transform.position).normalized * m_shootForce,
        //        ForceMode.Impulse);
        //    }

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
    }

    private void Move()
    {
        //transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput, Space.World);
        transform.Rotate(Vector3.up * Time.deltaTime * speed * 50 * horizontalInput, Space.Self);
        transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);


        //Inicia Controles y movimiento
        if (verticalInput > 0.0f)
        {
            if ((isRunning || haveGun) && !isAimming)
                m_Animator.SetInteger("State", 2);
            else
                m_Animator.SetInteger("State", 1);
        }
        else if (verticalInput < 0.0f)
        {
            if (isRunning || haveGun)
                m_Animator.SetInteger("State", -2);
            else
                m_Animator.SetInteger("State", -1);
        }

        if (horizontalInput > 0.0f)
            m_Animator.SetInteger("State", 3);
        else if (horizontalInput < 0.0f)
            m_Animator.SetInteger("State", -3);

        if (horizontalInput > 0.0f)
            m_Animator.SetInteger("State", 3);
        else if (horizontalInput < 0.0f)
            m_Animator.SetInteger("State", -3);

        if (horizontalInput == 0.0f && verticalInput == 0.0f)
        {
            m_Animator.SetInteger("State", 0);
            //m_audioPlayer.Stop();
            //isMoving = false;
        }
    }

    public void RecoverHealth(int cure)
    {
        life = life + cure; 
    }
    private void RotatePlayer()
    {
        Debug.Log("Estoy rotando");
        isRotating = false;
        m_Animator.SetTrigger(turn);
        int twist = turn.Equals("TurnLeft") ? 1 : -1;
        Vector3 rotTarget = transform.rotation.eulerAngles + new Vector3(0.0f, -angleRot * twist, 0.0f);
        var step = speedRot * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(rotTarget), step);
        _lookX.rotate = false;
    }

}
