using UnityEngine;

public class CambiarGameObject : MonoBehaviour
{
    public GameObject objetoADeshabilitar;
    public GameObject objetoAhabilitar;

    public void Cambiar()
    {
        // Deshabilitar el GameObject especificado
        if (objetoADeshabilitar != null)
        {
            objetoADeshabilitar.SetActive(false);
        }

        // Habilitar el GameObject especificado
        if (objetoAhabilitar != null)
        {
            objetoAhabilitar.SetActive(true);
        }
    }
}
