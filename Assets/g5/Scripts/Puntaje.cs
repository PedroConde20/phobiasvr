using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puntaje : MonoBehaviour
{
    public Text puntaje;
    public int puntosTotales = 0;
    void Start()
    {
        MostrarPuntos(puntosTotales);
    }
    public void MostrarPuntos(int totalPuntos)
    {
        puntaje.text = "Puntuacion: " + totalPuntos.ToString();
    }

    public void SumarPuntos(int puntosSumar)
    {
        puntosTotales = puntosTotales + puntosSumar;
        MostrarPuntos(puntosTotales);
    }
}
