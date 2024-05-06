using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextAracnofobiaController : MonoBehaviour
{
    public Text ayudaText;
    public BotonNivel2 botonNivel2;
    public botonnivel3 botonNivel3;

    private bool mostrandoTexto = false; // Variable para controlar si se est� mostrando un texto

    public SpiderControl spiderControl;

    public ObjetoFinalScript objetoFinalScript;

    public GameObject boton2;
    public GameObject boton3;


    public GameObject[] objetosAEscuchar; // Lista de GameObjects a ser verificados
    public bool todosDestruidos = false; // Variable que se volver� true cuando todos los GameObjects sean destruidos

    public DatosAntesDeSalir datosAntesDeSalir;
    public void Start()
    {
        // Inicialmente, mostramos el primer texto
        MostrarTextoInicial();
    }

    // Funci�n para mostrar el primer texto
    void MostrarTextoInicial()
    {
        // Limpiamos el texto anterior
        ayudaText.text = "";

        // Mostramos el nuevo texto
        ayudaText.text = "Est�s en el nivel 1. Puedes programar los controles del Simulador.";
    }



    // Funci�n para mostrar el texto del nivel 2
    void MostrarTextoNivel2()
    {
        // Limpiamos el texto anterior
        ayudaText.text = "";

        // Mostramos el nuevo texto
        ayudaText.text = "En este Nivel tendr�s que atrapar a la ara�a que est� rondando la casa";

    }
    void MostrarTextoNivel2Parte2()
    {
        // Limpiamos el texto anterior
        ayudaText.text = "";

        // Mostramos el nuevo texto
        ayudaText.text = "Bien hecho , ahora tirala al pasto del patio de afuera!";

    }
    void MostrarTextoNivel2Parte3()
    {
        // Limpiamos el texto anterior
        ayudaText.text = "";

        // Mostramos el nuevo texto
        ayudaText.text = "Felicidades, Superaste el Nivel 2! , Sigue con el Nivel 3!";

    }
    // Funci�n para mostrar el texto del nivel 3
    void MostrarTextoNivel3()
    {
        // Limpiamos el texto anterior
        ayudaText.text = "";

        // Mostramos el nuevo texto
        ayudaText.text = "En esta prueba tendr�s que eliminar a las tar�ntulas venenosas con A enciendes el Gas";
    }
    void MostrarTextoNivel3Parte2()
    {
        // Limpiamos el texto anterior
        ayudaText.text = "";

        // Mostramos el nuevo texto
        ayudaText.text = "Felicidades Completaste la fase 3! y acabaste Aracnofobia";
    }

    // Verificamos si se ha presionado alg�n bot�n y mostramos el texto correspondiente
    public void Update()
    {
        // Verificamos si se est� mostrando un texto
        if (!mostrandoTexto)
        {
            if (botonNivel2.activartextosNivel2)
            {
                MostrarTextoNivel2();
                if (spiderControl.ara�aencaja == true)
                {
                    MostrarTextoNivel2Parte2();

                    if (objetoFinalScript.esElFinalNivel2 == true)
                    {
                        MostrarTextoNivel2Parte3();
                        boton2.SetActive(false);
                        boton3.SetActive(true);
                        botonNivel2.activartextosNivel2 = false;
                    }
                }
            }
            else if (botonNivel3.Activartextosnivel3)
            {
                MostrarTextoNivel3();
                if (!todosDestruidos)
                {
                    // Iterar sobre cada GameObject en la lista
                    foreach (GameObject objeto in objetosAEscuchar)
                    {
                        // Si alg�n GameObject no existe en la escena, salimos del m�todo
                        if (objeto == null)
                        {
                            Debug.Log("Todos los objetos estan destruidos en el VerificarDestruccion");
                            todosDestruidos = true; // Marcar que todos los GameObjects han sido destruidos
                            break; // Salir del bucle foreach
                        }
                    }
                }
                if (todosDestruidos)
                {
                    Debug.Log("Se termino el juego");
                    MostrarTextoNivel3Parte2();


                    // Llama al m�todo para cambiar de escena despu�s de 5 segundos
                    Invoke("CambiarEscenaMenu", 5f);
                }
            }
        }
    }
    public void CambiarEscenaMenu()
    {

        // Aqu� cargamos la escena del men� principal
        datosAntesDeSalir.OnApplicationQuit();
        SceneManager.LoadScene("MenuV");
    }
}
