using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public CronometroController cronometroController;

    private bool isPaused = false;

    void Start()
    {
        // Asegurarse de que el men� de pausa est� oculto al inicio
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
    }

    void Update()
    {
        // Verificar si se presion� la tecla de escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Alternar entre pausado y no pausado
            isPaused = !isPaused;

            // Manejar la pausa
            if (isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    void PauseGame()
    {
        // Mostrar el men� de pausa
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(true);
        }

        // Pausar el tiempo
        Time.timeScale = 0f;

        // Pausar el cron�metro
        cronometroController.ToggleCronometro(false);
    }

    void ResumeGame()
    {
        // Ocultar el men� de pausa
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }

        // Resumir el tiempo
        Time.timeScale = 1f;

        // Reanudar el cron�metro
        cronometroController.ToggleCronometro(true);
    }
}
