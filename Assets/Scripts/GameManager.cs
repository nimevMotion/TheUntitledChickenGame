using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isGameOver;
    public bool isGamePaused;
    public bool isGameOn = true;

    public List<Tuple<string, bool, bool>> doors = new List<Tuple<string, bool,bool>>();

    public int pollitos;
    public string activeScene;

    [SerializeField]
    private GameObject[] m_Cameras;
    [SerializeField]
    private GameObject m_Doors;
    [SerializeField]
    private ItemManager _itemManager;

    private Player _player;
    private GameData _gameData;
    private MiniMapManager _miniMapManager;

    // Start is called before the first frame update
    void Start()
    {
        m_Cameras[0].SetActive(false);
        m_Cameras[0].SetActive(true);
        activeScene = SceneManager.GetActiveScene().name;

        _miniMapManager = GameObject.Find("MiniMapPlayer").GetComponent<MiniMapManager>();
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();

        foreach(Transform child in m_Doors.transform)
        {
            Door tmpDoor =child.GetComponent<Door>();
            doors.Add(new Tuple<string, bool, bool>(child.name, tmpDoor.canOpen, tmpDoor.isLock));
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

            if (Input.GetKeyDown(KeyCode.S))
            {
                SaveGame();
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                LoadGame();
            }
        }
    }

    private void SaveGame()
    {
        SaveManager.SaveGameData(this, _player, _itemManager, _miniMapManager.GetData());
    }

    private void LoadGame()
    {
        _gameData = SaveManager.LoadGameData();
        Debug.Log("Se ha cargado  el juego");
        printData(_gameData);
    }

    private void printData(GameData gameData)
    {
        print("1" + gameData.playerHealth);
        print("2" + gameData.pollitos);
        print("3" + gameData.scene);
        print("4");
        Tuple<string, int>[] items = gameData.items;
        List<Tuple<string, bool, bool>> doorstmp = gameData.doors;
        List<Tuple<string, bool>> maptmp = gameData.map;
        for (int i = 0; i < items.Length; i++)
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
}
