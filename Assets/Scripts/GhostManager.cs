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
    [SerializeField]
    private float m_health = 100.0f;

    private Animator _animator;
    private NavMeshAgent _agent;
    private Renderer _renderer;
    private Material _matBody;
    private ParticleSystem _particleSystem;
    private GameObject _player;
    private Vector3[] _position;

    private FieldOfView _fov;
    private bool _randomOn = false;
    private bool _binState = false;
    private bool _isDying = false;
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
            _position = new Vector3[m_Path.Length]; // crea el arreglo el mismo tamaño que el path
            _position[0] = m_Path[0].transform.position; //asigna la primera posicion

            for (int i = 1; i < m_Path.Length; i++)
            {
                _position[i] = m_Path[i].transform.position;
            }

            _animator = GetComponentInChildren<Animator>();
            _agent = GetComponent<NavMeshAgent>();
            _fov = GetComponent<FieldOfView>();
            _renderer = GetComponentInChildren<Renderer>();
            _particleSystem = GetComponentInChildren<ParticleSystem>();
            _player = GameObject.FindWithTag("Player");
            _state = GhostState.idle;

            foreach(var i in _renderer.materials)
            {
                if(i.name.Contains("Body"))
                {
                    _matBody = i;
                    break;
                }
            }

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
            case GhostState.dying:
                if(!_isDying)
                {
                    StartCoroutine("Die");
                }
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

        float distance = Vector3.Distance(transform.position, _position[_index]);
        if(distance <= 0)
        {
            
            Debug.Log("Cambia index " + _index);
            if (_index == m_Path.Length - 1)
            {
                _index = 0;
            }
            else
            {
                _index++;
            }
            StartCoroutine("Disappear");
            transform.LookAt(_position[_index], Vector3.up);
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
        //print(seconds);
    }

    IEnumerator Disappear()
    {
        float opacity = 1.0f;
        Debug.Log(_matBody.GetFloat("_Opacity"));
        while(opacity > 0)
        {
            opacity = opacity - 0.1f;

            yield return new WaitForSeconds(0.1f);
            _matBody.SetFloat("_Opacity", opacity);
        }
        while (opacity < 1)
        {
            yield return new WaitForSeconds(0.1f);
            opacity = opacity + 0.1f;
            _matBody.SetFloat("_Opacity", opacity);
        }
        _matBody.SetFloat("_Opacity", 1.0f);
    }

    public void Damage(float damage)
    {
        m_health -= damage;
        if(m_health <= 0.0f)
        {
            _state = GhostState.dying;
        }

    }
    IEnumerator Die()
    {
        Debug.Log("He muerto");
        _isDying = true;
        _animator.SetTrigger("dying");
        _particleSystem.Play();
        float opacity = 1.0f;
        while (opacity > 0)
        {
            opacity = opacity - 0.1f;

            yield return new WaitForSeconds(0.3f);
            _matBody.SetFloat("_Opacity", opacity);
        }
        Destroy(gameObject);
    }
    private enum GhostState
    {
        idle,
        attack,
        move,
        dying
    }

}
