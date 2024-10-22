using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class DatosAdmin : MonoBehaviour
{
    public string url = "https://unityvrprojectfobia.com/SelectMenu.php";
    public Text textoPrefab; // Prefab del texto para mostrar nombres
    public Transform contenedorDeTexto; // Contenedor original para los nombres de los pacientes
    public GameObject contenedorDeDatosPacienteObj; // Contenedor para mostrar datos completos
    public Text textoDatosPaciente; // Text para mostrar datos completos del paciente
    public Transform contenedorDatosPaciente; // Contenedor para los datos del paciente
    public GameObject botonDescargar; // GameObject para el botón de descarga

    public ScrollRect scrollView;

    private Text originalText;
    private Dictionary<string, string[]> datosPacienteMap = new Dictionary<string, string[]>();

    private void Start()
    {
        // Asigna el texto original a la variable
        originalText = textoPrefab;
        // Desactiva el contenedor de datos del paciente al inicio
        contenedorDeDatosPacienteObj.SetActive(false);
        // Desactiva el botón de descargar al inicio
        botonDescargar.SetActive(false);
    }

    public void ObtenerDatosAdminDesdeBoton()
    {
        StartCoroutine(ObtenerDatosAdmin());
    }

    public IEnumerator ObtenerDatosAdmin()
    {
        string[] parts = Web.loggedInUsername.Split('.');
        string nombreUsuarioAdministrador = (parts.Length > 1) ? parts[1] : Web.loggedInUsername;

        string urlCompleta = url + "?nombreUsuarioAdministrador=" + nombreUsuarioAdministrador;

        using (UnityWebRequest www = UnityWebRequest.Get(urlCompleta))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error al obtener los datos del administrador: " + www.error);
            }
            else
            {
                string datosRecibidos = www.downloadHandler.text;

                string[] conjuntosDeDatos = datosRecibidos.Split('\n');

                // Utilizamos un HashSet para asegurar que los nombres sean únicos
                HashSet<string> nombresUnicos = new HashSet<string>();

                // Limpiar el diccionario de datos de pacientes
                datosPacienteMap.Clear();



                foreach (string conjuntoDeDatos in conjuntosDeDatos)
                {
                    string[] columnas = conjuntoDeDatos.Split(',');

                    if (columnas.Length >= 6) // Asegurarse de que hay suficientes columnas
                    {
                        string nombreCompleto = columnas[0] + " " + columnas[1] + " " + columnas[2];
                        string fobia = columnas[3];
                        string nivel = columnas[4];
                        string duracion = columnas[5];
                        string fechaFin = columnas[6];

                        // Solo agregar al HashSet si no existe ya
                        if (nombresUnicos.Add(nombreCompleto))
                        {
                            // Solo mostramos el nombre completo
                            string textoFormateado = string.Format("{0}", nombreCompleto);
                            Text nuevoTexto = Instantiate(textoPrefab, contenedorDeTexto);
                            nuevoTexto.text = textoFormateado;
                            nuevoTexto.transform.SetParent(contenedorDeTexto, false);
                            nuevoTexto.gameObject.SetActive(true); // Asegura que el nuevo texto esté activo
                            Debug.Log(nombreCompleto);

                            // Configura el botón hijo
                            Button boton = nuevoTexto.GetComponentInChildren<Button>();
                            if (boton != null)
                            {
                                // Asigna el método OnClick al botón con el nombre y los datos
                                boton.onClick.AddListener(() => OnButtonClick(nombreCompleto, datosRecibidos));
                            }

                            // Almacena los datos del paciente en el diccionario
                            datosPacienteMap[nombreCompleto] = new string[] { fobia, nivel, duracion, fechaFin };
                        }
                    }
                }


                // Destruir el texto original si es necesario
                if (originalText != null)
                {
                    originalText.gameObject.SetActive(false);
                }

                // Se ajusta el contenido del ScrollView
                Canvas.ForceUpdateCanvases();
                scrollView.normalizedPosition = new Vector2(0, 0);
            }
        }
    }

    // Método que se llama al presionar el botón
    public void OnButtonClick(string nombreUsuario, string datosRecibidos)
    {
        // Desactivar el contenedor original y activar el contenedor de datos del paciente
        contenedorDeTexto.gameObject.SetActive(false);
        contenedorDeDatosPacienteObj.SetActive(true);

        // Limpiar el contenedor de datos del paciente
        foreach (Transform child in contenedorDeDatosPacienteObj.transform)
        {
            if (child.gameObject != textoDatosPaciente.gameObject) // No destruir el texto original
            {
                Destroy(child.gameObject);
            }
        }

        // Inicializa una variable para almacenar los datos del paciente
        string datosParaGuardar = "Datos del paciente: " + nombreUsuario + "\n\n"; // Un salto de línea adicional

        // Obtener datos específicos del paciente y mostrarlos
        string[] conjuntosDeDatos = datosRecibidos.Split('\n');
        foreach (string conjuntoDeDatos in conjuntosDeDatos)
        {
            string[] columnas = conjuntoDeDatos.Split(',');

            if (columnas.Length >= 6)
            {
                string nombreCompleto = columnas[0] + " " + columnas[1] + " " + columnas[2];
                if (nombreCompleto == nombreUsuario)
                {
                    string fobia = columnas[3];
                    string nivel = columnas[4];
                    string duracion = columnas[5];
                    string fechaFin = columnas[6];

                    string textoCompleto = string.Format("Fobia: {0}\nNivel: {1}\nDuración: {2}\nFecha Fin: {3}\n\n",
                                      fobia, nivel, duracion, fechaFin); // Dos saltos de línea al final para separación

                    // Instanciar un nuevo GameObject para el texto completo
                    Text nuevoTexto = Instantiate(textoDatosPaciente, contenedorDeDatosPacienteObj.transform);
                    // Asegurarse de que el componente Text esté activo
                    nuevoTexto.gameObject.SetActive(true);
                    // Asignar el texto completo al componente
                    nuevoTexto.text = textoCompleto;
                    // Opcional: Ajusta la posición o el diseño si es necesario
                    nuevoTexto.transform.SetParent(contenedorDeDatosPacienteObj.transform, false);

                    // Agregar los datos actuales al texto que se guardará (todos los conjuntos de datos del paciente)
                    datosParaGuardar += textoCompleto; // Ya tiene saltos de línea al final
                }
            }
        }

        // Ahora, solo almacenamos los datos para el uso posterior
        // almacenando datos para usarlos en el boton descargar
        PlayerPrefs.SetString("DatosParaGuardar", datosParaGuardar); // Guardar en PlayerPrefs para acceder luego

        // Activar el botón de descarga
        botonDescargar.SetActive(true);
    }
    public void GuardarArchivoSolo(string datos, string nombreUsuario)
    {
        // Generar el nombre del archivo con el nombre del paciente y la fecha actual
        string nombreArchivo = nombreUsuario.Replace(" ", "_") + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";

        // Obtener la ruta del escritorio del usuario
        string rutaEscritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        // Combinar la ruta del escritorio con el nombre del archivo
        string rutaArchivo = Path.Combine(rutaEscritorio, nombreArchivo);

        // Escribir los datos en el archivo
        try
        {
            File.WriteAllText(rutaArchivo, datos);
            Debug.Log("Archivo guardado en: " + rutaArchivo);
        }
        catch (Exception e)
        {
            Debug.LogError("Error al guardar el archivo: " + e.Message);
        }
    }
    public void OnNuevoBotonClick()
    {
        string datosParaGuardar = PlayerPrefs.GetString("DatosParaGuardar", "");
        // Obtener el nombre del usuario del último paciente seleccionado
        string nombreUsuario = ""; // Inicializa la variable para el nombre del paciente

        // Recuperar el nombre del paciente usando el diccionario
        if (datosPacienteMap.Count > 0)
        {
            // Suponiendo que estás guardando el último paciente seleccionado en PlayerPrefs
            // o puedes guardarlo en una variable de instancia cuando haces clic en el botón
            nombreUsuario = datosPacienteMap.Keys.Last(); // Obtiene el último nombre de paciente
        }

        if (!string.IsNullOrEmpty(datosParaGuardar))
        {
            GuardarArchivoSolo(datosParaGuardar, nombreUsuario);
        }
        else
        {
            Debug.LogWarning("No hay datos para guardar.");
        }
    }

    public void RegresarASeleccionDeNombres()
    {
        // Desactivar el contenedor de datos del paciente y activar el de nombres
        contenedorDeDatosPacienteObj.SetActive(false);
        contenedorDeTexto.gameObject.SetActive(true);

        // Eliminar cualquier texto existente en el contenedor de nombres, excepto el originalText si no está nulo
        foreach (Transform child in contenedorDeTexto)
        {
            if (child.gameObject != originalText.gameObject)
            {
                Destroy(child.gameObject);
            }
        }

        // Reactivar el texto original
        if (originalText != null)
        {
            originalText.gameObject.SetActive(true);
        }

        // Vuelve a obtener los nombres de los pacientes
        ObtenerDatosAdminDesdeBoton();

        // Forzar la actualización del layout para asegurar que los elementos se posicionen correctamente
        Canvas.ForceUpdateCanvases();

        // Asegúrate de que el ScrollView se posicione en la parte superior
        scrollView.verticalNormalizedPosition = 1f;
        // Reactivar el botón de descarga si es necesario
        botonDescargar.SetActive(false);
    }


    public void LimpiarVistaSinRecargar()
    {
        // Desactivar el contenedor de datos del paciente
        contenedorDeDatosPacienteObj.SetActive(false);
        // Reactivar el botón de descarga si es necesario
        botonDescargar.SetActive(false);

        // Limpiar el contenido del contenedor de datos del paciente
        foreach (Transform child in contenedorDeDatosPacienteObj.transform)
        {
            if (child.gameObject != textoDatosPaciente.gameObject) // No destruir el texto original
            {
                Destroy(child.gameObject);
            }
        }

        // Reactivar el contenedor de selección de nombres
        contenedorDeTexto.gameObject.SetActive(true);

        // Asegurarse de que el texto original esté desactivado
        if (originalText != null)
        {
            originalText.gameObject.SetActive(false);
        }

        // Limpiar el contenedor de nombres de pacientes
        foreach (Transform child in contenedorDeTexto)
        {
            if (child.gameObject != originalText.gameObject) // No destruir el texto original
            {
                Destroy(child.gameObject);
            }
        }

        // Forzar la actualización del layout para asegurar que los elementos se posicionen correctamente
        Canvas.ForceUpdateCanvases();

        // Asegúrate de que el ScrollView se posicione correctamente
        scrollView.verticalNormalizedPosition = 1f;
    }
}
