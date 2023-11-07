using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvionController : MonoBehaviour
{
    public Transform avionTransform; // La transformada del avi�n en Unity
    public float velocidadRotacion = 20.0f; // Velocidad de rotaci�n durante el despegue
    public float turbulenciaIntensidad = 0.2f; // Intensidad de la turbulencia
    public float tiempoDespegue = 10.0f; // Duraci�n del despegue

    private float tiempoTranscurrido = 0.0f;
    private Vector3 turbulencia;
    private Quaternion rotacionInicial;
    public bool estadespegando = false; // Booleano que indica si se est� despegando

    void Start()
    {
        // Almacenar la rotaci�n inicial del avi�n
        rotacionInicial = avionTransform.rotation;

        // Inicializar la turbulencia
        turbulencia = Random.insideUnitSphere * turbulenciaIntensidad;
    }

    void Update()
    {
        if (estadespegando)
        {
            tiempoTranscurrido += Time.deltaTime;
            // Durante el despegue
            if (tiempoTranscurrido < tiempoDespegue)
            {
                // Aplicar turbulencia
                avionTransform.Rotate(turbulencia * Time.deltaTime);

                // Calcular la rotaci�n deseada (apuntar hacia arriba)
                Quaternion rotacionDeseada = Quaternion.Euler(-20.0f, avionTransform.rotation.eulerAngles.y, avionTransform.rotation.eulerAngles.z);



                // Aplicar la rotaci�n suavemente
                avionTransform.rotation = Quaternion.Lerp(avionTransform.rotation, rotacionDeseada, velocidadRotacion * Time.deltaTime);
            }
            else
            {
                // Ha pasado el tiempo de despegue, restablecer la rotaci�n a la inicial
                avionTransform.rotation = Quaternion.Lerp(avionTransform.rotation, rotacionInicial, velocidadRotacion * Time.deltaTime);
            }
        }
    }
}