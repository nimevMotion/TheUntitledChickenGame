using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;

    private Vector3 iniPos;
    // Start is called before the first frame update
    void Start()
    {
        //iniPos = player.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = player.transform.position - iniPos;
    }
}
