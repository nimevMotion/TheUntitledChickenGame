using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    private Transform m_player;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(m_player.position.x, transform.position.y, m_player.position.z);

    }
}
