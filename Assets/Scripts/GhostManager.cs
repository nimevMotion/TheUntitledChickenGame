using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostManager : MonoBehaviour
{
    public GhostState state;

    [SerializeField]
    private GameObject[] m_Path;
    [SerializeField]
    private float m_speed = 1.0f;
    [SerializeField]
    private float m_health = 100.0f;
    [SerializeField]
    private GameObject m_item;
    [SerializeField]
    private string m_ItemDescripcion;
    [SerializeField]
    private bool m_isSpooky = false;
    [SerializeField]
    private GameObject m_SpookyGhost;
    [SerializeField]
    private AudioClip m_DyingAudio;
    [SerializeField]
    private AudioClip m_SpookyAudio;
    [SerializeField]
    private Material m_BodyMaterial;

    private Animator _animator;
    private NavMeshAgent _agent;
    private Renderer[] _renderer;
    [SerializeField]
    private Material _matBody;
    private ParticleSystem _particleSystem;
    private AudioSource _audioSource;
    private GameObject _player;
    private Vector3[] _position;

    private FieldOfView _fov;
    private GameManager _gameManager;
    private bool _randomOn = false;
    private bool _binState = false;
    private bool _isDying = false;
    private bool _hadItem = false;
    private int _index = 0;

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

            _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            _animator = GetComponentInChildren<Animator>();
            _agent = GetComponent<NavMeshAgent>();
            _fov = GetComponent<FieldOfView>();
            _renderer = GetComponentsInChildren<Renderer>();
            _particleSystem = GetComponentInChildren<ParticleSystem>();
            _audioSource = GetComponent<AudioSource>();
            _player = GameObject.FindWithTag("Player");
            
            if (m_isSpooky)
                state = GhostState.none;
            else
                state = GhostState.move;
            
            if(m_item!=null)
                _hadItem = true;

            foreach(var i in _renderer)
            {
                foreach(var j in i.materials)
                {
                    if (j.name.Contains("Body") || j.name.Contains("Cuerpo"))
                    {
                        _matBody = j;
                        break;
                    }
                }
                if (_matBody != null)
                    break;
            }

            if(_matBody == null)
            {
                Debug.Log("No hay material");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameManager.isGameOn && !_gameManager.isGamePaused)
        {
            GhostStateFunction();
        }
        else if (_gameManager.isGameOver)
        {
            _animator.SetBool("walk", false);
            _animator.SetBool("attack", false);
        }
            

    }

    private void GhostStateFunction()
    {
        switch (state)
        {
            case GhostState.none:
                _matBody.SetFloat("_Opacity", 0.0f);
                break;
            case GhostState.spooky:
                if (m_isSpooky)
                    StartCoroutine("Spooky");
                break;
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
                if (!_isDying)
                {
                    StartCoroutine("Die");
                }
                break;
        }

        if (!_randomOn && !m_isSpooky)
        {
            StartCoroutine(GetCuckoo(Random.Range(3.0f, 8.0f)));
        }

        if (_fov.canSeePlayer && !m_isSpooky)
        {
            state = GhostState.attack;
        }
    }

    private void Move()
    {
        _animator.SetBool("walk", true);
        var step = m_speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _position[_index], step);

        float distance = Vector3.Distance(transform.position, _position[_index]);
        if(distance < 1.0f)
        {
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
        gameObject.GetComponent<Collider>().isTrigger = false;
        _agent.enabled = true;
        _agent.destination = _player.transform.position;
    }

    IEnumerator GetCuckoo(float seconds)
    {
        _randomOn = true;
        _binState = !_binState;
        if (_binState)
            state = GhostState.idle;
        else 
            state = GhostState.move;

        yield return new WaitForSeconds(seconds);
        _randomOn = false;
    }

    IEnumerator Disappear()
    {
        float opacity = 1.0f;
        while(opacity > 0)
        {
            opacity = opacity - 0.1f;

            yield return new WaitForSeconds(0.1f);
            _matBody.SetFloat("_Opacity", opacity);
            //m_BodyMaterial.SetFloat("_Opacity", opacity);
        }
        while (opacity < 1)
        {
            yield return new WaitForSeconds(0.1f);
            opacity = opacity + 0.1f;
            _matBody.SetFloat("_Opacity", opacity);
            //m_BodyMaterial.SetFloat("_Opacity", opacity);
        }
        _matBody.SetFloat("_Opacity", 1.0f);
        //m_BodyMaterial.SetFloat("_Opacity", 1.0f);
    }

    public IEnumerator Damage(float damage)
    {
        yield return new WaitForSeconds(1.0f);
        m_health -= damage;
        if(m_health <= 0.0f)
        {
            state = GhostState.dying;
        }
    }
    IEnumerator Die()
    {
        _isDying = true;
        _agent.isStopped = true;
        _randomOn = true;
        _animator.SetTrigger("dying");
        _audioSource.Stop();
        _particleSystem.Play();

        float opacity = 1.0f;
        while (opacity > 0)
        {
            opacity = opacity - 0.1f;

            yield return new WaitForSeconds(0.3f);
            _matBody.SetFloat("_Opacity", opacity);
        }
        _audioSource.PlayOneShot(m_DyingAudio);
        if (_hadItem)
        { 
            GameObject item = Instantiate(m_item, transform.position + new Vector3(0.0f, 0.1f, 0.0f), Quaternion.Euler(90, 0, 90));
            GameItem key = item.GetComponent<GameItem>();
            key.desc = m_ItemDescripcion;
        }
        Destroy(gameObject);
    }
    IEnumerator Spooky()
    {
        float opacity = 0.0f;
        m_isSpooky = false;
        state = GhostState.none;
        while (opacity < 1)
        {
            yield return new WaitForSeconds(0.1f);
            opacity = opacity + 0.1f;
            _matBody.SetFloat("_Opacity", opacity);
        }
        _audioSource.PlayOneShot(m_SpookyAudio);
        transform.position = transform.position + (transform.forward * 1f);
        while (opacity > 0)
        {
            opacity = opacity - 0.1f;
            yield return new WaitForSeconds(0.3f);
            _matBody.SetFloat("_Opacity", opacity);
        }
        _matBody.SetFloat("_Opacity", 0.0f);
        m_SpookyGhost.SetActive(true);
        Destroy(gameObject);
    }
    public enum GhostState
    {
        none,
        idle,
        attack,
        move,
        dying,
        spooky
    }

}
