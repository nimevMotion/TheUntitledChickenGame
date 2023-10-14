using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public GameObject panelButtons;
    public GameObject panelSettings;
    public GameObject enterButton;
    public GameObject pollito;

    [SerializeField]
    private GameObject m_ItemsMenu;
    [SerializeField]
    private GameObject m_PauseMenu;
    [SerializeField]
    private GameObject m_HUD;
    [SerializeField]
    private GameObject m_MapMenu;

    private GameManager _gameManager;
    private ItemManager _itemManager;
    
    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _itemManager = GetComponent<ItemManager>();
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
    public void ChangeScene(string scene)
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
    }

    public void GetMap()
    {
        m_MapMenu.SetActive(true);
        m_ItemsMenu.SetActive(false);
    }

    public void ResumeGame()
    {
        _gameManager.isGamePaused = false;
        m_ItemsMenu.SetActive(false);
        m_MapMenu.SetActive(false);
    }

    public void ExitGame()
    {
        ChangeScene("Scene00_Intro");
    }

}
