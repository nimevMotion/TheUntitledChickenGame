using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapWall : MonoBehaviour
{
    [SerializeField]
    GameObject m_Parent;

    // Update is called once per frame
    void Update()
    {
        gameObject.layer = m_Parent.layer;
    }
}
