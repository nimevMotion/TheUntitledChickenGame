using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    public GameObject button;

    AsyncOperation operacion;

    private void Start()
    {
        string nivelACargar = LoadScene.siguienteNivel;
        StartCoroutine(IniciarCarga(nivelACargar));
    }

    public void ChangeScene()
    {
        operacion.allowSceneActivation = true;
    }

    IEnumerator IniciarCarga(string nivel)
    {
        operacion = SceneManager.LoadSceneAsync(nivel);
        operacion.allowSceneActivation = false;

        while(!operacion.isDone)
        {
            if (operacion.progress >= 0.9f)
            {
                //Aqui va la logica del boton
               if (!button.activeSelf)
                {
                    button.SetActive(true);
                }
            }

            yield return null;
        }
    }

}
