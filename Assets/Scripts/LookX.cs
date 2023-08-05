using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookX : MonoBehaviour
{
    /*Public*/
    public bool rotate =  false;

    /*Serialized var*/
    [SerializeField]
    private Transform m_player;
    [SerializeField]
    private float m_sensitivity = 1.0f;

    /*Private*/
    private Player player;
    private Vector3 newRot;
    private Vector3 iniPos;
    private Quaternion iniRot;

    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        iniPos = transform.localPosition;
        iniRot = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        float _mouseX = Input.GetAxis("Mouse X");
        if(!rotate)
            transform.RotateAround(m_player.position, Vector3.up, _mouseX * m_sensitivity);

        if (CameraAngle() > 17.0f && !rotate)
        {
            if (transform.localPosition.x > 0)
            {
                rotate = true;
                Debug.Log("Limite de vista izquierdo");
                player.turn = "TurnLeft";
                player.isRotating = true;

            }
            else if (transform.localPosition.x < 0)
            {
                rotate = true;
                Debug.Log("Limite de vista derecha");
                player.turn = "TurnRight";
                player.isRotating = true;
            }         
        }
    }

    private float CameraAngle()
    {
        Vector3 vecCamera = transform.worldToLocalMatrix.MultiplyVector(transform.forward);
        Vector3 vecPlayer = m_player.worldToLocalMatrix.MultiplyVector(transform.forward);
        return Vector3.Angle(vecPlayer + vecCamera, vecCamera);
    }

}
