using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isGameOver;
    public bool isGamePause;

    [SerializeField]
    private GameObject[] m_Cameras;

    // Start is called before the first frame update
    void Start()
    {
        m_Cameras[0].SetActive(false);
        m_Cameras[0].SetActive(true);
    }
}
