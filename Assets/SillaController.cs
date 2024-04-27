using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SillaController : MonoBehaviour
{
    public float distanciaSilla = 1.5f;
    public AudioSource audioSource1; // AudioSource para el primer sonido.
    public AudioSource audioSource2; // AudioSource para el segundo sonido.
    private bool playerCerca = false;
    private Transform playerTransform;
    private bool sentado = false;
    private CharacterController playerController; // Referencia al CharacterController del jugador.
    public AvionController avionController; // Referencia al AvionController.
    public DespegueAvion despegueavion;
    public CameraController cameraController;

    public string mensaje = "Apreta E para sentarte";
    public Text textoCanvas;

    public GameObject referenciaPosicion; // GameObject que servirá como referencia para la posición del jugador

    private void Update()
    {
        if (playerCerca)
        {
            if (!sentado)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    if (audioSource1 != null)
                    {
                        textoCanvas.text = "El avion esta apunto de despegar! manten la calma mientras que empiece";
                        audioSource1.Play();
                        StartCoroutine(EsperarYReproducirSegundoSonido());
                    }

                    SitOnChair();
                }
            }
            else
            {
                if (Input.GetButtonDown("Fire3"))
                {
                    StandUp();
                }
            }
        }
    }

    private IEnumerator EsperarYReproducirSegundoSonido()
    {
        textoCanvas.text = "Manten la calma, comenzará la turbulencia";
        yield return new WaitForSeconds(audioSource1.clip.length + 10f); // Espera por la duración del primer sonido más 10 segundos.
        if (audioSource2 != null)
        {
            despegueavion.despegando = true;
            audioSource2.Play();
            if (avionController != null)
            {
                avionController.estadespegando = true;
                Debug.Log("Esta despegando esta en True");
                textoCanvas.text = ". . . ";
                StartCoroutine(cameraController.Shake());
            }
        }

        // Desactivar el controlador de personaje para que no se pueda mover
        playerController.enabled = false;
    }

    private void SitOnChair()
    {
        playerController.enabled = false;
        Vector3 newPosition = referenciaPosicion.transform.position;
        newPosition.y -= 0.6f; // Reducir la altura en 0.5 unidades
        newPosition.z -= 0.07f;
        newPosition.x += 0.3f;
        playerTransform.position = newPosition;
        playerTransform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        sentado = true;
    }
    private void StandUp()
    {
        playerController.enabled = true;
        playerTransform.position = new Vector3(playerTransform.position.x, 0.0f, playerTransform.position.z);
        sentado = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCerca = true;
            playerTransform = other.transform;
            playerController = other.GetComponent<CharacterController>();
            textoCanvas.text = mensaje;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCerca = false;
        }
    }
}