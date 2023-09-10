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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int pollitos = GameObject.FindGameObjectsWithTag("Pollito").Length;
        m_numPollitosTxt.text = pollitos.ToString("D2");
    }
}
