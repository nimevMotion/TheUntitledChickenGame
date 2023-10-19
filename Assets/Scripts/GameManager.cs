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

    public int pollitos;
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
            m_Cameras[0].SetActive(false);
            m_Cameras[0].SetActive(true);

            _miniMapManager = GameObject.Find("MiniMapPlayer").GetComponent<MiniMapManager>();
            _player = GameObject.FindWithTag("Player").GetComponent<Player>();
            _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
            _itemManager = GameObject.Find("UIManager").GetComponent<ItemManager>();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (!SaveManager.isNewGame)
            {
                LoadGame();
            }
            else
            {
                GetDoors();
            }
        }
        else
            isGameOn = false;
        
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
        //printData(_gameData);
        _gameData = SaveManager.LoadGameData();

        _player.life = _gameData.playerHealth;
        Vector3 posPlayer = new Vector3(_gameData.playerPosX, _gameData.playerPosY, _gameData.playerPosZ);
        _player.transform.position = posPlayer;
        _itemManager.UpdateItems(_gameData.items);
        pollitos = _gameData.pollitos;
        _miniMapManager.UpdateMapa(_gameData.map);
        UpdateDoors(_gameData.doors);

        Debug.Log("Se ha cargado  el juego");
        printData(_gameData);
    }

    private void printData(GameData gameData)
    {
        print("1" + gameData.playerHealth);
        print("2" + gameData.pollitos);
        print("3" + gameData.scene);
        print("4");
        List<Tuple<string, int, string>> items = gameData.items;
        List<Tuple<string, bool, bool>> doorstmp = gameData.doors;
        List<Tuple<string, bool>> maptmp = gameData.map;
        for (int i = 0; i < items.Count; i++)
        {
            print(items[i].Item1 + " " + items[i].Item2);
        }
        print("5");
        foreach(var i in doorstmp) 
        {
            print(i.Item1 + " " + i.Item2 + " " + i.Item3);
        }
        print("6");
        foreach (var i in maptmp)
        {
            print(i.Item1 + " " + i.Item2);
        }
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
}
