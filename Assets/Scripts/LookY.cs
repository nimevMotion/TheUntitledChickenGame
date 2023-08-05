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
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float _mouseY = Input.GetAxis("Mouse Y");
        newRotation = transform.localEulerAngles;
        newRotation.x -= _mouseY * _sensitivity;

        if (newRotation.x > 340 || newRotation.x < 20)
        {
            transform.localEulerAngles = newRotation;
        }
    }
}
