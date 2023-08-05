using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverAlrededor : MonoBehaviour
{
    private Vector3 centro;
    private Vector3 iniPos;
    private float radio;

    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        iniPos = transform.position;
        centro = player.transform.position;
        radio = Vector3.Distance(iniPos, player.position);
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 newPos;
        //newPos.x = transform.position.x + 0.01f;
        //newPos.y = transform.position.y;
        //newPos.z = centro.z + Mathf.Sqrt((radio * radio) - Mathf.Pow(newPos.x - centro.x, 2.0f));
        //transform.position = newPos;

        transform.RotateAround(player.position, Vector3.up, 20.0f * Time.deltaTime);
        //Debug.Log(transform.localPosition);
        

    }
}
