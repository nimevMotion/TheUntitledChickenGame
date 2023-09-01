using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    Cinemachine.CinemachineImpulseSource m_source;

    // Start is called before the first frame update
    void Start()
    {
        m_source = GetComponent<Cinemachine.CinemachineImpulseSource>();
        m_source.GenerateImpulse(Vector3.forward * -1);

        Destroy(gameObject, 5);
    }
}
