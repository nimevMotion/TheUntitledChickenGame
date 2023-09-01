using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_Path;
    [SerializeField]
    private float m_speed = 1.0f;
    [SerializeField]
    private float m_speedRot = 1.0f;

    private Animator _animator;
    private NavMeshAgent _agent;
    private GameObject _player;
    private Vector3[] _position;

    private FieldOfView _fov;
    private bool _randomOn = false;
    private bool _binState = false;
    private int _index = 0;

    private GhostState _state;

    // Start is called before the first frame update
    void Start()
    {
        if(m_Path.Length <= 1)
        {
            Debug.LogError("El path no puede ser menor o igual a 1");
        }else
        {
            _position = new Vector3[m_Path.Length];
            _position[0] = m_Path[0].transform.position;

            for (int i = 1; i < m_Path.Length; i++)
            {
                _position[i] = m_Path[i].transform.position;
            }

            _animator = GetComponentInChildren<Animator>();
            _agent = GetComponent<NavMeshAgent>();
            _fov = GetComponent<FieldOfView>();
            _player = GameObject.FindWithTag("Player");
            _state = GhostState.idle;

        }
        

    }

    // Update is called once per frame
    void Update()
    {
        switch (_state)
        {
            case GhostState.idle:
                _animator.SetBool("walk", false);
                break;
            case GhostState.move:
                
                Move();
                break;
            case GhostState.attack:
                Attack();
                break;
        }

        if (!_randomOn)
        {
            StartCoroutine(GetCuckoo(Random.Range(3.0f, 8.0f)));
        }

        if (_fov.canSeePlayer)
        {
            _state = GhostState.attack;
        }
        
        
    }

    private void Move()
    {
        _animator.SetBool("walk", true);
        var step = m_speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _position[_index], step);

        float singleStep = m_speedRot * Time.deltaTime;
        Vector3 targetDirection = _position[_index] - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);

        if (transform.position == _position[_index])
        {
            if (_index == m_Path.Length - 1)
            {
                _index = 0;
            }
            else
            {
                _index++;
            }
        }

        
    }

    private void Attack()
    {
        _animator.SetBool("attack", true);
        _animator.SetBool("walk", false);
        _agent.enabled = true;
        _agent.destination = _player.transform.position;
    }

    IEnumerator GetCuckoo(float seconds)
    {
        _randomOn = true;
        _binState = !_binState;
        if (_binState)
            _state = GhostState.idle;
        else 
            _state = GhostState.move;

        yield return new WaitForSeconds(seconds);
        _randomOn = false;
        print(seconds);
    }
    private enum GhostState
    {
        idle,
        attack,
        move,
        dying
    }

}
