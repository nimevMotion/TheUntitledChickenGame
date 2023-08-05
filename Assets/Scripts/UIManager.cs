using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject panelButtons;
    public GameObject panelSettings;
    public GameObject enterButton;
    public GameObject pollito;

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

}
