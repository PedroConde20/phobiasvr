using UnityEngine;
using UnityEngine.UI;

public class TablaDatos : MonoBehaviour
{
    public GameObject celdaPrefab;
    public Transform contenedorTabla;

    // Método para actualizar la tabla con nuevos datos
    public void ActualizarTabla(string[] datos)
    {
        // Limpiar la tabla
        LimpiarTabla();

        // Instanciar celdas para cada conjunto de datos
        foreach (string conjuntoDeDatos in datos)
        {
            // Dividir el conjunto de datos en columnas
            string[] columnas = conjuntoDeDatos.Split(',');

            // Crear una nueva instancia de la celda prefab
            GameObject nuevaCelda = Instantiate(celdaPrefab, contenedorTabla);

            // Configurar los elementos visuales dentro de la celda con los datos correspondientes
            Text textoNombreCompleto = nuevaCelda.transform.Find("NombreCompleto").GetComponent<Text>();
            Text textoFobia = nuevaCelda.transform.Find("Fobia").GetComponent<Text>();
            Text textoNivel = nuevaCelda.transform.Find("Nivel").GetComponent<Text>();
            Text textoDuracion = nuevaCelda.transform.Find("Duracion").GetComponent<Text>();

            // Asignar los datos a los elementos visuales
            textoNombreCompleto.text = "Nombre Completo: " + columnas[0];
            textoFobia.text = "Fobia: " + columnas[1];
            textoNivel.text = "Nivel: " + columnas[2];
            textoDuracion.text = "Duración: " + columnas[3];
        }
    }

    // Método para limpiar la tabla
    public void LimpiarTabla()
    {
        foreach (Transform child in contenedorTabla)
        {
            Destroy(child.gameObject);
        }
    }
}