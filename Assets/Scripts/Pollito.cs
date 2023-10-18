using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pollito : MonoBehaviour
{ 
    private GameManager _gameManager;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _animator = GetComponent<Animator>();
        StartCoroutine("RandomAnim");
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.tag.Equals("Player"))
        {
            _gameManager.pollitos -= 1;
            Player player = collision.gameObject.GetComponent<Player>();
            player.FindChicken();
            Destroy(gameObject);
        }
    }

    IEnumerator RandomAnim()
    {
        WaitForSeconds waitTime = new WaitForSeconds(Random.Range(3.0f, 8.0f));
        while (true)
        {
            switch(Random.Range(0,3))
            {
                case 0:
                    _animator.SetBool("Eat", true);
                    _animator.SetBool("Turn Head", false);
                    break;
                case 1:
                    _animator.SetBool("Eat", false);
                    _animator.SetBool("Turn Head", true);
                    break;
                case 2:
                    _animator.SetBool("Eat", false);
                    _animator.SetBool("Turn Head", false);
                    break;
            }
            yield return waitTime;
        }
    }

    public void GetChicken()
    {
        StopCoroutine("RandomAnim");
        _animator.SetTrigger("Fall");
    }
}
