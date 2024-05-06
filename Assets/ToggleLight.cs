using UnityEngine;

public class ToggleLight : MonoBehaviour
{
    public Light luzFoco; // Asigna la luz del foco en el Inspector
    public float distanciaActivacion = 5f;

    void Start()
    {
        // Aseg�rate de que la luz est� encendida al inicio
        luzFoco.enabled = true;
    }

    void Update()
    {
        // No es necesario el c�digo de actualizaci�n para cambiar el estado de la luz
    }

}