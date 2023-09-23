using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [SerializeField]
    private int m_TotalPollitos;
    [SerializeField]
    private TextMeshProUGUI m_numPollitosTxt;
    [SerializeField]
    private GameObject m_PauseMenu;
    [SerializeField]
    private GameObject m_HUD;
    [SerializeField]
    private GameObject m_ItemsMenu;
   
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        int pollitos = GameObject.FindGameObjectsWithTag("Pollito").Length;
        m_numPollitosTxt.text = pollitos.ToString("D2");

        if(_gameManager.isGamePaused)
        {
            m_PauseMenu.SetActive(true);

        }
        else
        {
            m_PauseMenu.SetActive(false);
        }
    }

    public void GetItems()
    {
        m_ItemsMenu.SetActive(true);
    }
}
