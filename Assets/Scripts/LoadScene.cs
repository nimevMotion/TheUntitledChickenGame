using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene 
{
    public static string siguienteNivel;
    public static void NivelACargar(string scene)
    {   
        siguienteNivel = scene;
        SceneManager.LoadScene("SceneA_LoadScene");
    }
}
