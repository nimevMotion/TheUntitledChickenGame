using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    public bool isGameOver;
    public bool isGamePaused;
    public bool isGameOn = true;
    public Vector3 savePoint;
    
    public List<Tuple<string, bool, bool>> doors = new List<Tuple<string, bool,bool>>();
    public List<Tuple<string, bool>> pollitos = new List<Tuple<string, bool>>();

    public int numPollitos;
    public string activeScene;

    [SerializeField]
    private GameObject[] m_Cameras;
    [SerializeField]
    private GameObject m_Doors;
    [SerializeField]
    public GameObject[] m_SavePoints;
    [SerializeField]
    private PlayableDirector m_playableDirector;

    private Player _player;
    private GameData _gameData;
    private MiniMapManager _miniMapManager;
    private UIManager _uiManager;
    private ItemManager _itemManager;
    private AsyncOperation operacion;

    private bool _isCinematic;
    private bool _loadComplete =  false;

    // Start is called before the first frame update
    void Start()
    {
        activeScene = SceneManager.GetActiveScene().name;
        if (activeScene.Contains("Cinematic"))
            _isCinematic = true;
        if (!activeScene.Equals("Scene00_Intro") && !activeScene.Equals("SceneA_LoadScene") && !_isCinematic)
        {
            isGameOn = true;
            isGameOver = false;

            m_Cameras[0].SetActive(false);
            m_Cameras[0].SetActive(true);

            _miniMapManager = GameObject.Find("MiniMapPlayer").GetComponent<MiniMapManager>();
            _player = GameObject.FindWithTag("Player").GetComponent<Player>();
            _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
            _itemManager = GameObject.Find("UIManager").GetComponent<ItemManager>();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            numPollitos = GameObject.FindGameObjectsWithTag("Pollito").Length;

            if (!SaveManager.isNewGame)
            {
                LoadGame();
            }
            else
            {
                GetDoors();
                GetPollitos();
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            isGameOn = false;
        }

    }

    private void Update()
    {
        if(isGameOn)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                isGamePaused = true;
            }

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.visible = true;
            }
        }
    }

    public void SaveGame(int index)
    {
        savePoint = m_SavePoints[index].transform.position;
        SaveManager.SaveGameData(this, _player, _itemManager, _miniMapManager.GetData());
    }

    public void LoadGame()
    {
        _gameData = SaveManager.LoadGameData();

        _player.life = _gameData.playerHealth;
        Vector3 posPlayer = new Vector3(_gameData.playerPosX, _gameData.playerPosY, _gameData.playerPosZ);
        _player.transform.position = posPlayer;
        _itemManager.UpdateItems(_gameData.items);
        numPollitos = UpdatePollitos(_gameData.pollitos);
        _miniMapManager.UpdateMapa(_gameData.map);
        UpdateDoors(_gameData.doors);

        Debug.Log("Se ha cargado  el juego");
    }

    private void GetDoors()
    {
        doors.Clear();

        foreach (Transform child in m_Doors.transform)
        {
            Door tmpDoor = child.GetComponent<Door>();
            doors.Add(new Tuple<string, bool, bool>(child.name, tmpDoor.canOpen, tmpDoor.isLock));
        }
    }

    private void UpdateDoors(List<Tuple<string, bool, bool>> newDoors)
    {
        doors.Clear();
        doors.AddRange(newDoors);
    }

    private void GetPollitos()
    {
        GameObject[] pollitosTemp = GameObject.FindGameObjectsWithTag("Pollito");
        
        pollitos.Clear();
        foreach(GameObject i in pollitosTemp)
        {
            pollitos.Add(new Tuple<string, bool>(i.name, true));
        }
    }

    private int UpdatePollitos(List<Tuple<string, bool>> newPollitos)
    {
        GameObject[] pollitosTemp = GameObject.FindGameObjectsWithTag("Pollito");
        int pollitosActive = 0;
        pollitos.Clear();
        foreach(var i in newPollitos)
        {
            foreach(var j in pollitosTemp)
            {
                if (i.Item1.Equals(j.name))
                { 
                    Debug.Log(i.Item1);
                    if(!i.Item2)
                        Destroy(j);
                    else
                        pollitosActive++;

                    break;
                }
            }
            pollitos.Add(i);
        }

        return pollitosActive;
    }

    public void UpdatePollitos(string name)
    {
        List<Tuple<string, bool>> temp = new List<Tuple<string, bool>>();
        
        temp.AddRange(pollitos);
        pollitos.Clear();

        foreach (var i in temp)
        {
            if (i.Item1.Equals(name))
                pollitos.Add(new Tuple<string, bool>(name, false));
            else
                pollitos.Add(i);
        }
    }

    public void StartCinematic(string scene)
    {
        SaveManager.isNewGame = true;
        StartCoroutine(IniciarCarga(scene));
    }

    public void EndCinematic()
    {
        if(_loadComplete)
            operacion.allowSceneActivation = true;
    }

    IEnumerator IniciarCarga(string nivel)
    {
        operacion = SceneManager.LoadSceneAsync(nivel);
        operacion.allowSceneActivation = false;

        while (!operacion.isDone)
        {
            if (operacion.progress >= 0.9f)
            {
                _loadComplete = true;
            }
            yield return null;
        }
    }

    public void StartCinematic()
    {
        _uiManager.ActivateHUD(false);
        isGameOn = false;
        m_playableDirector.Play();
    }

    public void GameOver()
    {
        isGameOver = true;
        isGameOn = false;
        _uiManager.GameOver();
    }
}
