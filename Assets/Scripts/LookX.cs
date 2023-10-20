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
    private GameManager _gameManager;
    private Vector3 newPosition;

    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!_gameManager.isGamePaused && !_gameManager.isGameOver)
        {
            float _mouseX = Input.GetAxis("Mouse X");

            newPosition = transform.localPosition;
            newPosition.x += _mouseX * m_sensitivity;
            if (newPosition.x > 0.5f)
            {
                newPosition.x = 0.5f;
                player.isRotating = true;
            }
            else if (newPosition.x < -0.3f)
            {
                newPosition.x = -0.3f;
                player.isRotating = true;
            }
            else
            {
                player.isRotating = false;
            }

            transform.localPosition = newPosition;
        }
        

    }

}
