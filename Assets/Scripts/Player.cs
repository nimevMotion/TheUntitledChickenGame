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

    /*Serialized var*/
    [SerializeField]
    private Camera m_Camera;
    [SerializeField]
    private GameObject m_Bullet;

    /*Private*/
    private Animator m_Animator;
    private AnimatorClipInfo[] m_CurrentClipInfo;
    private Rigidbody m_RBBullet;
    private Transform mAim;
    private Transform mplayerMesh;

    private LookX _lookX;

    private string clipName;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 newRotation;
    private Vector3 iniCamPos;
    private Vector3 iniCamDir;

    private bool m_IsPlaying = false;
    private bool isRunning = false;
    private bool isAimming = true;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.name.Equals("Player"))
            {
                mplayerMesh = child;
            }
            if (child.name.Equals("Aim"))
                mAim = child;
        }

        m_Animator = GetComponentInChildren<Animator>();      
        _lookX = GetComponentInChildren<LookX>();

        iniCamPos = m_Camera.transform.localPosition;
        iniCamDir = m_Camera.transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        m_CurrentClipInfo = m_Animator.GetCurrentAnimatorClipInfo(0);
        clipName = m_CurrentClipInfo[0].clip.name;
        
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        //transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput, Space.World);
        //transform.Rotate(Vector3.up * horizontalInput);
        newRotation = transform.localEulerAngles;
        newRotation.y = mplayerMesh.transform.localEulerAngles.y;

        //transform.localEulerAngles = newRotation;
        transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);

        if (verticalInput > 0.0f)
            if (isRunning)
                if (isAimming)
                    m_Animator.SetInteger("State", 6);
                else
                    m_Animator.SetInteger("State", 2);
            else if (isAimming)
                m_Animator.SetInteger("State", 5);
            else
                m_Animator.SetInteger("State", 1);
        else if (verticalInput < 0.0f)
            if (isRunning)
                m_Animator.SetInteger("State", -2);
            else
                m_Animator.SetInteger("State", -1);

        if (horizontalInput > 0.0f)
            m_Animator.SetInteger("State", 3);
        else if (horizontalInput < 0.0f)
            m_Animator.SetInteger("State", -3);

        if (horizontalInput == 0.0f && verticalInput == 0.0f && !m_IsPlaying)
        { 
            m_Animator.SetInteger("State", 0);
            mplayerMesh.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }

        if(horizontalInput != 0.0f || verticalInput != 0.0f)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                isRunning = true;
                speed = 5.0f;
                //m_Animator.speed = 5.0f;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                Debug.Log("Stop running..........................");
                //m_Animator.SetInteger("State", 0);
                //m_Animator.speed = 1.0f;
                isRunning = false;
                speed = 1.0f;
            }
        }

        if (isRotating)
        {
            RotatePlayer();
        }
        if(Input.GetKeyDown(KeyCode.Z))
        {
            mplayerMesh.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
        
        if (clipName.Equals("LeftTurn") || clipName.Equals("RightTurn"))
            m_IsPlaying = true;
        else if ((!clipName.Equals("LeftTurn") || !clipName.Equals("RightTurn"))
            && m_IsPlaying)
        {
            ResetCam();
        }

        if(Input.GetMouseButtonDown(1))
        {
            m_RBBullet = Instantiate(m_Bullet, mAim.position, Quaternion.identity)
                .GetComponent<Rigidbody>();
            m_RBBullet.AddForce(Vector3.forward * 5.0f, ForceMode.Impulse);
        }
    }

    private void RotatePlayer()
    {
        Debug.Log("Estoy rotando");
        isRotating = false;
        m_Animator.SetTrigger(turn);
    }

    private void ResetCam()
    {
        m_IsPlaying = false;
        float angle = 0.0f;
        int twist = turn.Equals("TurnLeft") ? 1 : -1;
        float pivote = transform.localEulerAngles.y;
        Vector3 rotCam = m_Camera.transform.localEulerAngles; 

        do
        {
            angle += Time.deltaTime * 0.1f;
            transform.Rotate(Vector3.up, -angle * twist, Space.Self);
            transform.rotation = Quaternion.Euler(0.0f, pivote - angle * twist, 0.0f);
            mplayerMesh.transform.rotation = Quaternion.Euler(0.0f, angle * twist, 0.0f);
        } while (angle < 60.0f);

        mplayerMesh.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        m_Camera.transform.localPosition = iniCamPos;
        m_Camera.transform.localEulerAngles = new Vector3(rotCam.x, iniCamDir.y, 0.0f);
        _lookX.rotate = false;
    }
}
