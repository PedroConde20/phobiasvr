using UnityEngine;

public class ToggleLight : MonoBehaviour
{
    public Light luzFoco; // Asigna la luz del foco en el Inspector
    public float distanciaActivacion = 5f;

    void Start()
    {
        // Asegúrate de que la luz esté encendida al inicio
        luzFoco.enabled = true;
    }

    void Update()
    {
        // No es necesario el código de actualización para cambiar el estado de la luz
    }

}