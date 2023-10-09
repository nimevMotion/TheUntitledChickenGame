using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_infoHUD; 
    [SerializeField]
    private TextMeshProUGUI m_numPollitosTxt;
    [SerializeField]
    private Slider m_BarraVida;
    
    private GameManager _gameManager;
    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        m_infoHUD.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        m_numPollitosTxt.text = _gameManager.pollitos.ToString("D2");
        m_BarraVida.value = _player.life;
        
    }

    public void UpdateInfoHUD(string txt)
    {
        m_infoHUD.SetActive(true);
        TMP_Text txtInfo = m_infoHUD.GetComponentInChildren<TextMeshProUGUI>();
        txtInfo.text = txt;
    }

    public void DeactivateInfoHUD()
    {
        TMP_Text txtInfo = m_infoHUD.GetComponentInChildren<TextMeshProUGUI>();
        txtInfo.text = "";
        m_infoHUD.SetActive(false);
    }

}
