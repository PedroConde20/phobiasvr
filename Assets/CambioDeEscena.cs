using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioDeEscena : MonoBehaviour
{
    // Nombre de la escena a la que quieres cambiar
    public string nombreDeEscenaACambiar = "AerofobiaTerminal";

    // Actualiza cada frame
    void Update()
    {
        // Verifica si la tecla "T" est� siendo presionada
        if (Input.GetKeyDown(KeyCode.T))
        {
            // Llama al m�todo para cambiar de escena
            CambiarEscena();
        }
    }

    // M�todo para cambiar de escena
    void CambiarEscena()
    {
        // Conservar este objeto al cambiar de escena
        DontDestroyOnLoad(gameObject);

        // Carga la escena especificada
        SceneManager.LoadScene(nombreDeEscenaACambiar);
    }
}
