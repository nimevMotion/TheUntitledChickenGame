using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pollito : MonoBehaviour
{ 
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.tag.Equals("Player"))
        {
            _gameManager.pollitos -= 1;
            Destroy(gameObject);
        }
    }
}
