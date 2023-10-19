using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject panelButtons;
    public GameObject panelSettings;
    public GameObject enterButton;
    public GameObject pollito;
    public Button saveButton;
    public int index;

    [SerializeField]
    private GameObject m_ItemsMenu;
    [SerializeField]
    private GameObject m_PauseMenu;
    [SerializeField]
    private GameObject m_HUD;
    [SerializeField]
    private GameObject m_MapMenu;
    [SerializeField]
    private GameObject m_SaveMenu;

    private GameManager _gameManager;
    private GameData _gameData;
    private AudioSource _audioSource;
    
    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _audioSource = GetComponent<AudioSource>();
        
    }

    private void Update()
    {
        if(_gameManager.isGameOn)
        {
            if (_gameManager.isGamePaused)
            {
                m_PauseMenu.SetActive(true);
                m_HUD.gameObject.SetActive(false);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
                m_PauseMenu.SetActive(false);
                m_HUD.gameObject.SetActive(true);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        
    }
    private void ChangeScene(string scene)
    {
        LoadScene.NivelACargar(scene);
    }

    public void ActivateButtons()
    {
        enterButton.SetActive(false);
        pollito.SetActive(false);
        panelButtons.SetActive(true);

    }

    public void ActivateSettings()
    {
        panelButtons.SetActive(false);
        panelSettings.SetActive(true);
    }

    public void exit()
    {
        Application.Quit();
    }

    public void returnSelection()
    {
        panelSettings.SetActive(false);
        panelButtons.SetActive(true);
    }

    public void GetItems()
    {
        m_ItemsMenu.SetActive(true);
        m_MapMenu.SetActive(false);
        m_SaveMenu.SetActive(false);
    }

    public void GetMap()
    {
        m_MapMenu.SetActive(true);
        m_ItemsMenu.SetActive(false);
        m_SaveMenu.SetActive(false);
    }

    public void ResumeGame()
    {
        _gameManager.isGamePaused = false;
        m_ItemsMenu.SetActive(false);
        m_MapMenu.SetActive(false);
        m_SaveMenu.SetActive(false);
    }

    public void ExitGame()
    {
        ChangeScene("Scene00_Intro");
    }

    public void ClickSound()
    {
        _audioSource.Play();
    }

    public void SaveGame()
    {
        m_MapMenu.SetActive(false);
        m_ItemsMenu.SetActive(false);
        _gameManager.SaveGame(index);
        m_SaveMenu.SetActive(true);
    }

    public void LoadGame()
    {
        SaveManager.isNewGame = false;
        _gameData = SaveManager.LoadGameData();
        ChangeScene(_gameData.scene);
    }

    public void NewGame(string scene)
    {
        SaveManager.isNewGame = true;
        ChangeScene(scene);
    }

    public void ActivateHUD(bool active)
    {
        m_HUD.SetActive(active);
    }

}
