using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderControl : MonoBehaviour
{
    public float speed = 5f;
    public float rotationChangeInterval = 2f; // Intervalo de tiempo para cambiar la rotación
    public string boxTag = "Caja"; // Tag de la caja

    private Quaternion targetRotation;
    private float rotationTimer = 0f;
    private bool isMoving = true; // Indica si la araña está en movimiento

    private Animation animation; // Referencia al componente Animation

    private GameObject dentroObject; // Objeto "Dentro" en la jerarquía

    void Start()
    {
        GenerateRandomRotation();
        animation = GetComponent<Animation>(); // Obtener el componente Animation

        dentroObject = GameObject.Find("Dentro"); // Encontrar el objeto "Dentro" en la jerarquía
    }

    void Update()
    {
        if (isMoving)
        {
            MoveForward();

            rotationTimer += Time.deltaTime;
            if (rotationTimer >= rotationChangeInterval)
            {
                GenerateRandomRotation();
                rotationTimer = 0f;
            }

            RotateModel();
        }
    }

    void MoveForward()
    {
        Vector3 movement = transform.forward * speed * Time.deltaTime;
        transform.Translate(movement, Space.World);
    }

    void GenerateRandomRotation()
    {
        float randomYRotation = Random.Range(-90f, 90f); // Rango más limitado de rotación
        targetRotation = Quaternion.Euler(0f, randomYRotation, 0f);
    }

    void RotateModel()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 2f); // Suavizado de rotación más lento
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(boxTag))
        {
            Debug.Log("Colisiono");
            isMoving = false; // Detener la araña cuando colisiona con la caja
            animation.Stop("run");
            animation.Play("idle"); // Activar la transición a la animación "idle"

            // Establecer la posición relativa de la araña dentro del objeto "Dentro"
            transform.SetParent(dentroObject.transform);
            transform.localPosition = Vector3.zero;
        }
    }
}