using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoFinalScript : MonoBehaviour
{
    public GameObject[] objectsToActivate; // Array de objetos a activar
    public GameObject objectToDestroy; // Objeto a destruir
    public GameObject movingObject; // Objeto hijo a mover
    public float movementSpeed = 5f; // Velocidad de movimiento

    private MeshRenderer[] meshRenderers; // Array de componentes MeshRenderer
    private SkinnedMeshRenderer skinnedMeshRenderer; // Referencia al componente SkinnedMeshRenderer
    private bool isMoving; // Indica si el objeto hijo está en movimiento
    public bool esElFinalNivel2 = false;
    private void Start()
    {
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        // Desactivar los renderers y detener el movimiento al inicio
        foreach (MeshRenderer renderer in meshRenderers)
        {
            renderer.enabled = false;
        }
        skinnedMeshRenderer.enabled = false;

        movingObject.SetActive(false);
        isMoving = false;

        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Caja"))
        {
            // Activar los renderers y los objetos cuando la caja colisiona con el objeto "ObjetosFinal"
            foreach (MeshRenderer renderer in meshRenderers)
            {
                renderer.enabled = true;
            }
            skinnedMeshRenderer.enabled = true;

            foreach (GameObject obj in objectsToActivate)
            {
                obj.SetActive(true);
            }

            // Iniciar el movimiento del objeto hijo
            movingObject.SetActive(true);
            isMoving = true;

            // Destruir el objeto especificado
            Destroy(objectToDestroy);
            esElFinalNivel2 = true;
        }
    }

    private void Update()
    {
        if (isMoving)
        {
            // Mover el objeto hijo en dirección -x
            movingObject.transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
        }
    }
} 