using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvionController : MonoBehaviour
{
    public Transform avionTransform; // La transformada del avión en Unity
    public float velocidadRotacion = 20.0f; // Velocidad de rotación durante el despegue
    public float turbulenciaIntensidad = 0.2f; // Intensidad de la turbulencia
    public float tiempoDespegue = 10.0f; // Duración del despegue

    private float tiempoTranscurrido = 0.0f;
    private Vector3 turbulencia;
    private Quaternion rotacionInicial;
    public bool estadespegando = false; // Booleano que indica si se está despegando

    void Start()
    {
        // Almacenar la rotación inicial del avión
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

                // Calcular la rotación deseada (apuntar hacia arriba)
                Quaternion rotacionDeseada = Quaternion.Euler(-20.0f, avionTransform.rotation.eulerAngles.y, avionTransform.rotation.eulerAngles.z);



                // Aplicar la rotación suavemente
                avionTransform.rotation = Quaternion.Lerp(avionTransform.rotation, rotacionDeseada, velocidadRotacion * Time.deltaTime);
            }
            else
            {
                // Ha pasado el tiempo de despegue, restablecer la rotación a la inicial
                avionTransform.rotation = Quaternion.Lerp(avionTransform.rotation, rotacionInicial, velocidadRotacion * Time.deltaTime);
            }
        }
    }
}