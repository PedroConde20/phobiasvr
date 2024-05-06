using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersonWakeUp : MonoBehaviour
{
    public bool estaactualizado = true;
    public float distanciaSilla = 1.5f;
    public bool playerCerca = false;
    public Transform playerTransform;
    public bool sentado = false;
    public CharacterController playerController; // Referencia al CharacterController del jugador.
    public CambioTextoCanvas cambioTextoCanvas;
    private bool primeraColision = true; // Variable para controlar la primera colisión.
    private int controlasiento = 0;
    public DatosAntesDeSalir datosAntesDeSalir;
    private void Update()
    {
        if (playerCerca)
        {
            if (!sentado || sentado && controlasiento == 2)
            {
                // La primera vez que colisiona, se sienta automáticamente.
                if (primeraColision)
                {
                    Sentarse();
                    primeraColision = false;
                    controlasiento = 1;
                }
                else if (Input.GetButtonDown("Fire2"))
                {
                    Debug.Log("ESTA SENTADO?" + "" + sentado); //FALSE
                    // A partir de la segunda vez, el jugador puede levantarse o sentarse al presionar la tecla F.
                    if (sentado==false && cambioTextoCanvas.final== false)
                    {
                        Debug.Log("SE ESTA SENTANDO Y ESTA EN FALSE EL FINAL");
                        Sentarse();
                    }
                    else if (sentado == true && cambioTextoCanvas.final == false)
                    {
                        Debug.Log("SE ESTA PARANDO Y ESTA EN FALSE EL FINAL");
                        Pararse();
                    }
                    else if (sentado == false && cambioTextoCanvas.final == true)
                    {
                        Debug.Log("SE ESTA SENTADO Y ESTA EN TRUE EL FINAL");
                        Sentarse();

                        //ESTA ES LA BUENA
                        // Llama al método para cambiar de escena después de 5 segundos
                        Invoke("CambiarEscenaMenu", 5f);
                    }
                    else if (sentado && cambioTextoCanvas.final == true)
                    {
                        Debug.Log("Este es el final y no te podras parar");
                    }
                }
            }
            else if (sentado && controlasiento <=1)
            {
                Debug.Log("ESTA ENTRANDO A DONDE NO DEBIA");
                if (Input.GetButtonDown("Fire2"))
                {
                    controlasiento++;
                    Debug.Log("SIGUE METIENDOSE DONDE NO ES");
                    Debug.Log(controlasiento);
                    Pararse();
                }
            }
        }
    }

    public void CambiarEscenaMenu()
    {

        // Aquí cargamos la escena del menú principal
        datosAntesDeSalir.OnApplicationQuit();
        SceneManager.LoadScene("MenuV");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCerca = true;
            playerTransform = other.transform;
            playerController = other.GetComponent<CharacterController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCerca = false;
        }
    }

    private void Sentarse()
    {
        playerController.enabled = false;
        playerTransform.position = transform.position;
        playerTransform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        sentado = true;
    }

    private void Pararse()
    {
        playerController.enabled = true;
        playerTransform.position = new Vector3(playerTransform.position.x, 0.0f, playerTransform.position.z);
        sentado = false;
    }
}
