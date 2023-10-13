using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookY : MonoBehaviour
{
    /*Serialized var*/
    [SerializeField]
    private float _sensitivity = 1.0f;

    /*Private*/
    private Vector3 newPosition;
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        transform.localPosition = transform.localPosition 
            + new Vector3(0.0f, 1.5f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(!_gameManager.isGamePaused)
        {
            float _mouseY = Input.GetAxis("Mouse Y");

            newPosition = transform.localPosition;
            newPosition.y += _mouseY * _sensitivity;
            if (newPosition.y > 1.9f)
            {
                newPosition.y = 1.9f;
            }
            else if (newPosition.y < 0.1f)
            {
                newPosition.y = 0.1f;
            }

            transform.localPosition = newPosition;
        }
        
    }
}
