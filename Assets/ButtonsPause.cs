using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsPause : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public DatosAntesDeSalir datosAntesDeSalir;
    public void GoToMenu()
    {
        // Resumir el tiempo
        Time.timeScale = 1f;
        // Aquí cargamos la escena del menú principal
        datosAntesDeSalir.OnApplicationQuit();
        SceneManager.LoadScene("MenuV");

    }

    public void ResumeGame()
    {
        // Ocultar el menú de pausa si existe
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }

        // Resumir el tiempo
        Time.timeScale = 1f;
    }
}

