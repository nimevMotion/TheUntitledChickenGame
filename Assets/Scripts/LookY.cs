using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookY : MonoBehaviour
{
    /*Serialized var*/
    [SerializeField]
    private float _sensitivity = 1.0f;

    /*Private*/
    private Vector3 newRotation;
    private Vector3 newPosition;

    private void Start()
    {
        transform.localPosition = transform.localPosition 
            + new Vector3(0.0f, 1.5f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        float _mouseY = Input.GetAxis("Mouse Y");
        //newRotation = transform.localEulerAngles;
        //newRotation.x -= _mouseY * _sensitivity;

        //if (newRotation.x > 340 || newRotation.x < 20)
        //{
        //    transform.localEulerAngles = newRotation;
        //}

        newPosition = transform.localPosition;
        newPosition.y += _mouseY * _sensitivity;
        if(newPosition.y > 1.9f)
        {
            newPosition.y = 1.9f;
        }
        else if (newPosition.y < 0.1f)
        {
            newPosition.y = 0.1f;
        }

        transform.localPosition = newPosition;

        //transform.Translate(Vector3.up * _mouseY);
    }
}
