using UnityEngine;

public class VerificarDestruccion : MonoBehaviour
{
    public GameObject[] objetosAEscuchar; // Lista de GameObjects a ser verificados
    public bool todosDestruidos = false; // Variable que se volverá true cuando todos los GameObjects sean destruidos

    public void Update()
    {
        // Verificar si todos los GameObjects han sido destruidos
        if (!todosDestruidos)
        {
            // Iterar sobre cada GameObject en la lista
            foreach (GameObject objeto in objetosAEscuchar)
            {
                // Si algún GameObject no existe en la escena, salimos del método
                if (objeto == null)
                {
                    Debug.Log("Todos los objetos estan destruidos en el VerificarDestruccion");
                    todosDestruidos = true; // Marcar que todos los GameObjects han sido destruidos
                    break; // Salir del bucle foreach
                }
            }
        }
    }
}
