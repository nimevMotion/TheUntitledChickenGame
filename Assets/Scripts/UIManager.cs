using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    
    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (_gameManager.isGamePaused)
        {
            m_PauseMenu.SetActive(true);
            m_HUD.gameObject.SetActive(false);

        }
        else
        {
            m_PauseMenu.SetActive(false);
            m_HUD.gameObject.SetActive(true);
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
    }

    public void GetMap()
    {
        m_MapMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        _gameManager.isGamePaused = false;
        //m_HUD.gameObject.SetActive(true);
        //m_PauseMenu.SetActive(false);
    }

}
