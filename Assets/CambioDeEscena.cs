using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioDeEscena : MonoBehaviour
{
    // Nombre de la escena a la que quieres cambiar
    public string nombreDeEscenaACambiar = "AerofobiaTerminal";

    // Actualiza cada frame
    void Update()
    {
        // Verifica si la tecla "T" está siendo presionada
        if (Input.GetKeyDown(KeyCode.T))
        {
            // Llama al método para cambiar de escena
            CambiarEscena();
        }
    }

    // Método para cambiar de escena
    void CambiarEscena()
    {
        // Conservar este objeto al cambiar de escena
        DontDestroyOnLoad(gameObject);

        // Carga la escena especificada
        SceneManager.LoadScene(nombreDeEscenaACambiar);
    }
}
