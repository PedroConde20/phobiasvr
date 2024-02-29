using System.Collections;
using UnityEngine;

public class TurbulenciaAvion : MonoBehaviour
{
    public bool turbulenciaActivada = false;
    public float amplitud = 1.0f;
    public float frecuencia = 1.0f;

    private Vector3 posicionInicial;
    private float tiempoInicio;

    private void Start()
    {
        posicionInicial = transform.position;
        tiempoInicio = Time.time;
    }

    private void Update()
    {
        if (turbulenciaActivada)
        {
            SimularTurbulencia();
        }
    }

    private void SimularTurbulencia()
    {
        float offsetX = Mathf.PerlinNoise(Time.time * frecuencia, tiempoInicio) * 2.0f - 1.0f;
        float offsetY = Mathf.PerlinNoise(tiempoInicio, Time.time * frecuencia) * 2.0f - 1.0f;

        Vector3 offset = new Vector3(offsetX, offsetY, 0.0f) * amplitud;

        transform.position = posicionInicial + offset;
    }
}
